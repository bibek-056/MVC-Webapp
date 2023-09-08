using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Models;

namespace MVC_Webapp.Data
{
    public class MVC_WebappContext : DbContext
    {
        public MVC_WebappContext (DbContextOptions<MVC_WebappContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_Webapp.Models.Information> Information { get; set; } = default!;

        public DbSet<MVC_Webapp.Models.Educations>? Educations { get; set; }

        public DbSet<MVC_Webapp.Models.Certifications>? Certifications { get; set; }

        public DbSet<MVC_Webapp.Models.Experiences>? Experiences { get; set; }

        public DbSet<MVC_Webapp.Models.Projects>? Projects { get; set; }

        public DbSet<MVC_Webapp.Models.Reference>? Reference { get; set; }

        public DbSet<MVC_Webapp.Models.Scholarships>? Scholarships { get; set; }

        public DbSet<MVC_Webapp.Models.Skills>? Skills { get; set; }
    }
}
