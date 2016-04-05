using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class Institution
    {
        public int InstitutionId { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        public int AddressId { get; set; }
        public int CoordinateId { get; set; }
        public int RoleId { get; set; }

        [Required]
        public virtual Coordinate Coordinate { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        public virtual Role Role { get; set; }
    }
}