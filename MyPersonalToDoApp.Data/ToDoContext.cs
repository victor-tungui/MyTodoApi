using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data
{
    public class ToDoContext : IdentityDbContext<ApplicationUser>
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options)
        {
        }

        public DbSet<Todo> ToDos { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.ModelTodoEntity(modelBuilder);
            this.ModelActivityEntity(modelBuilder);
            this.ModelCustomerEntity(modelBuilder);
        }

        private void ModelCustomerEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(c => {
                c.Property(o => o.UserId)
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");                    

                c.Property(n => n.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");

                c.Property(n => n.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");
                c.ToTable("Customers");
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(c => c.Customer)
                .WithOne(usr => usr.User)
                .HasForeignKey<Customer>(au => au.UserId);
        }

        private void ModelActivityEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(a =>
            {   
                a.ToTable("Activities");
            });

            modelBuilder.Entity<Activity>()
               .Property(a => a.Name)
               .HasMaxLength(60)
               .IsRequired()
               .HasColumnType("VARCHAR");

            modelBuilder.Entity<Activity>()
               .Property(a => a.Description)
               .HasMaxLength(500)
               .HasColumnType("VARCHAR");

            modelBuilder.Entity<Activity>()
                .Property(a => a.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.CustomerId);
        }

        private void ModelTodoEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todos");

            modelBuilder.Entity<Todo>()
               .Property(t => t.Name)
               .HasMaxLength(60)
               .IsRequired()
               .HasColumnType("VARCHAR");

            modelBuilder.Entity<Todo>()
               .Property(t => t.Description)
               .HasMaxLength(500)
               .HasColumnType("VARCHAR");

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Todo>()
               .Property(t => t.Status)
               .IsRequired();

            modelBuilder.Entity<Todo>()
                .Property(t => t.ActivityId)
                .IsRequired();

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Activity)
                .WithMany(a => a.Todos)
                .HasForeignKey(t => t.ActivityId);
        }
    }
}
