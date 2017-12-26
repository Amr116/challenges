using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterTime.Models
{
    public class WorkFlowDto
    {
        public Guid ProjectId { get; set; }
        public float workHours {get;set;}
    }
}