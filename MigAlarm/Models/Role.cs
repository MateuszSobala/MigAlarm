using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int InstitutionId { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        [Required]
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}