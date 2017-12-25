using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterTime.Models
{
    public class CreateProjectDto
    {
        [Required]
        public string Title { get; set; }
    }
}