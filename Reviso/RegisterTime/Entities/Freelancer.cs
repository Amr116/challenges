using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RegisterTime.Entities
{
    public partial class Freelancer
    {
        public Guid Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<WorkFlow> WorkFlows {get; set;}
    }
}
