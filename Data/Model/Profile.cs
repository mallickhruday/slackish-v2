using Slackish.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slackish.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Profile:ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        [Index("ProfileNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string Name { get; set; }
        public User User { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Conversation> Conversations { get; set; } = new HashSet<Conversation>();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
