using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public sealed class Role
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
        public Institution Institution { get; set; }

        public ICollection<User> Users { get; set; }
    }
}