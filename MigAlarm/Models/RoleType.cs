using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class RoleType
    {
        public RoleType()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoleTypeId { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}