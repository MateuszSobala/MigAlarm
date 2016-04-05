using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public sealed class RoleType
    {
        public RoleType()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoleTypeId { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}