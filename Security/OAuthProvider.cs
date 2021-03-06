using MediatR;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Slackish.Security
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public OAuthProvider(Lazy<IAuthConfiguration> lazyAuthConfiguration, IMediator mediator)
        {
            _authConfiguration = lazyAuthConfiguration.Value;
            _mediator = mediator;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(_authConfiguration.AuthType);
            var username = context.OwinContext.Get<string>($"{_authConfiguration.AuthType}:username");
            var response = await _mediator.Send(new GetClaimsForUserQuery.Request() { Username = username });

            foreach (var claim in response.Claims)
            {
                identity.AddClaim(claim);
            }
            context.Validated(identity);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];
                var teamName = context.Request.Headers.Get("Team");
                var response = await _mediator.Send(new IsAuthenticUserQuery.Request() { Username = username, Password = password, TeamName = teamName });
                if (response.IsAuthenticated)
                {
                    context.OwinContext.Set($"{_authConfiguration.AuthType}:username", username);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch (Exception exception)
            {
                context.SetError(exception.Message);
                context.Rejected();
            }            
        }

        private readonly IMediator _mediator;
        private readonly IAuthConfiguration _authConfiguration;
        private readonly Lazy<IAuthConfiguration> _lazyAuthConfiguration;
    }
}
