using System;
using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Data
{
    public class LanguageClassesContext : DbContext
    {
        public LanguageClassesContext()
        {
        }

        public LanguageClassesContext(DbContextOptions<LanguageClassesContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeesCourse> EmployeesCourses { get; set; } = null!;
        public DbSet<Listener> Listeners { get; set; } = null!;
        public DbSet<ListenersCourse> ListenersCourses { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Purpose> Purposes { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=languageClasses2;Integrated Security=True";
        //        _ = optionsBuilder
        //            .UseSqlServer(connectionString)
        //            //.UseSqlite(connectionString)
        //            .Options;
        //        optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        //    }
        //}
    }
}
