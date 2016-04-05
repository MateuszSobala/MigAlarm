﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class EventNode
    {
        public int EventNodeId { get; set; }

        public int? AncestorId { get; set; }
        [ForeignKey("AncestorId")]
        public virtual Event Ancestor { get; set; }

        public int? OffspringId { get; set; }
        [ForeignKey("OffspringId")]
        public virtual Event Offspring { get; set; }
    }
}