using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public sealed class Institution
    {
        public Institution()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int Id { get; set; }       

        [DisplayName("Nazwa"), Required]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}