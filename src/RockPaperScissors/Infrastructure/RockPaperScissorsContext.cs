using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure
{
    public class RockPaperScissorsContext: DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Error> Errors { get; set; }

        public RockPaperScissorsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            // Player
            modelBuilder.Entity<Player>().Property(p => p.Name).HasMaxLength(150);

            // Game
            modelBuilder.Entity<Game>().Property(g => g.Player1Id).IsRequired();
            modelBuilder.Entity<Game>().Property(g => g.Player2Id).IsRequired();
            modelBuilder.Entity<Game>().HasOne(g=> g.Player1).WithMany(p => p.Games).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasOne(g => g.Player2).WithMany(p => p.Games).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasOne(g => g.Winner).WithMany(p => p.Games).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasMany(g => g.Rounds).WithOne(r => r.Game).OnDelete(DeleteBehavior.Restrict); ;

            // Round
            modelBuilder.Entity<Round>().Property(r => r.Move1).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Round>().Property(r => r.Move2).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Round>().Property(r => r.GameId).IsRequired();
            modelBuilder.Entity<Round>().HasOne(r => r.Winner).WithMany(p => p.Rounds).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
