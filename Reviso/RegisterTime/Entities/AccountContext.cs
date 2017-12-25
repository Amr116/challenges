using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using RegisterTime.Models;

namespace RegisterTime.Entities
{
    public class AccountContext : IdentityDbContext
    {
        //public virtual Freelancer Freelancer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=RegisterTime;uid=;pwd=;");
    }
}
