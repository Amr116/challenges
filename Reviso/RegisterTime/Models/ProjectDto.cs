using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterTime.Models
{
    public class ProjectDto
    {
        public Guid ProjetId { get; set; }
        public string Title { get; set; }
        public DateTime startDate{get; set;}
    }
}