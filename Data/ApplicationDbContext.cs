using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Models;
using PetCommunity.Models.Dto;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PetCommunity.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        // done
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PetsType> PetsTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<PetSeller> PetSellers { get; set; }

        public DbSet<DocktorCreationRequest> DocktorCreationRequests { get; set; }
        public DbSet<Connect> Connects { get; set; }

        public DbSet<Comment> comments { get; set; }

        // Q



        public DbSet<Learn> Learns { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ServicePlanned> ServicePlanneds { get; set; }
        public DbSet<ServiceProvided> ServiceProvideds { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PUnit> Units { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        


    }
}