using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RegisterTime.Entities
{
    public partial class WorkFlow
    {
        public int Id {get; set;}
        public Guid FreelancerId {get; set;}
        public Guid ProjectId {get; set;}
        public float WorkHours{get; set;}
        //public DateTime TimeStamp{get; set;}
        public virtual Freelancer freelancer {get; set;}
        public virtual Project project {get; set;}
    }
}