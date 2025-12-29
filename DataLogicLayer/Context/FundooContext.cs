using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace DataLogicLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Notes> Notes { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<NoteLabel> NoteLabels { get; set; }

        public DbSet<Collaborator> Collaborators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Notes>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<NoteLabel>()
                .HasKey(nl => new { nl.NoteId, nl.LabelId });

            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Notes)
                .WithMany(n => n.NoteLabels)
                .HasForeignKey(nl => nl.NoteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Label)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Collaborator>()
                .HasOne(c => c.Notes)
                .WithMany(n => n.Collaborators)
                .HasForeignKey(c => c.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Collaborator>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collaborators)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}

