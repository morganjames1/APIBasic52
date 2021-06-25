using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;


namespace API.Context
{

    public class MyContext :DbContext

    {
        public MyContext(DbContextOptions<MyContext> options) : base (options)
        {

        }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many
            modelBuilder.Entity<Employee>()
               .HasOne(e => e.Account)
               .WithOne(a => a.Employee)
               .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
               .HasOne(a => a.Profiling)
               .WithOne(p => p.Account)
               .HasForeignKey<Profiling>(p => p.NIK);

            // One to One
            modelBuilder.Entity<Education>()
                .HasMany(p => p.Profilings)
                .WithOne(edc => edc.Education);

            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University);

            //Many to Many
            modelBuilder.Entity<AccountRole>()
            .HasKey(ar => new { ar.NIK, ar.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(ar => ar.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }

}
