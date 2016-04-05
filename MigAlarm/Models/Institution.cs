using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class Institution
    {
        public Institution()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int Id { get; set; }       

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public virtual Coordinate Coordinate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}