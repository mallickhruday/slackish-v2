﻿using MediatR;
using Slackish.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using Slackish.Services;

namespace Slackish.Featrues.Messages
{
    public class MessagesQuery: IAsyncRequest<List<MessageApiModel>>
    {
        public MessagesQuery() { }        
    }

    public class MessagesQueryHandler: IAsyncRequestHandler<MessagesQuery, List<MessageApiModel>>
    {
        public MessagesQueryHandler(DataContext dataContext, ICache cache)
        {
            _cache = cache;
            _dataContext = dataContext;
        }

        public async Task<List<MessageApiModel>> Handle(MessagesQuery query)
        {
            return await _dataContext.Messages
                .Select(x => MessageApiModel.FromMessage(x))
                .ToListAsync();
        }

        private DataContext _dataContext;
        private ICache _cache;
    }
}