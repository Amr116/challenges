using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterTime.Entities
{
    public partial class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate {get; set;}

        public ICollection<WorkFlow> WorkFlows {get; set;}        
    }
}
