using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public sealed class AdditionalDataType
    {
        public AdditionalDataType()
        {
            AdditionalData = new HashSet<AdditionalData>();
        }
        public int AdditionalDataTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<AdditionalData> AdditionalData { get; set; }
    }
}