using Microsoft.EntityFrameworkCore;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options)
        {
        }

        public DbSet<Todo> ToDos { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ModelTodoEntity(modelBuilder);
            this.ModelActivityEntity(modelBuilder);
        }

        private void ModelActivityEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().ToTable("Activities");

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
