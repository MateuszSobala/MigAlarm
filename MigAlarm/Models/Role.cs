using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public sealed class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public int RoleTypeId { get; set; }

        public ICollection<User> Users { get; set; }
        [Required]
        public Institution Institution { get; set; }
        [Required]
        public RoleType RoleType { get; set; }
    }
}