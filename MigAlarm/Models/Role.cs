using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public int RoleTypeId { get; set; }

        public virtual ICollection<User> Users { get; set; }
        [Required]
        public virtual Institution Institution { get; set; }
        [Required]
        public virtual RoleType RoleType { get; set; }
    }
}