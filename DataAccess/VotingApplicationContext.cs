using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VotingApplicationContext : DbContext
    {
        public DbSet<Choice>? Choices { get; set; }
        public DbSet<Poll>? Polls { get; set; }
        public VotingApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Poll>()
                .HasIndex(p => p.PollName)
                .IsUnique();

            modelBuilder.Entity<Choice>()
                .HasIndex(c => new { c.PollId, c.ChoiceText })
                .IsUnique();

            modelBuilder.Entity<Choice>()
                .Property(c => c.ChoiceText)
                .HasDefaultValue(0);
        }
    }
}
