using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager_API.Models.DTO
{
    public class ProjectManagerContext:DbContext
    {
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options):base (options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
