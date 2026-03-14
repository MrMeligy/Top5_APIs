using Microsoft.EntityFrameworkCore;
using Top5.Domain.Entities;
using Top5.Domain.Enums;
using Top5.Domain.Models;

namespace Top5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SysSetting> SysSettings { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<TeamPlayers> TeamPlayers { get; set; }
        public DbSet<MatchPlayers> MatchPlayers { get; set; }
        public DbSet<Token> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Token>()
            .Property(x => x.id)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<SysSetting>(entity =>
            {
                entity.HasNoKey();
            });
            // Configure Player
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.phone).IsRequired();
                entity.HasIndex(e => new {e.username,e.phone }).IsUnique();
                entity.Property(e => e.password).IsRequired();
            });

            // Configure Team
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired();
                entity.HasOne(e => e.captin)
                    .WithMany()
                    .HasForeignKey(e => e.captinId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Match
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasOne(e => e.homeTeam)
                    .WithMany()
                    .HasForeignKey(e => e.homeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.awayTeam)
                    .WithMany()
                    .HasForeignKey(e => e.awayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(x => x.statues)
              .HasConversion<int>();

            });

            // Configure TeamPlayers
            modelBuilder.Entity<TeamPlayers>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasOne(e => e.player)
                    .WithMany()
                    .HasForeignKey(e => e.playerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.team)
                    .WithMany()
                    .HasForeignKey(e => e.teamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure MatchPlayers
            modelBuilder.Entity<MatchPlayers>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasOne(e => e.match)
                    .WithMany()
                    .HasForeignKey(e => e.matchId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.player)
                    .WithMany()
                    .HasForeignKey(e => e.playerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.team)
                    .WithMany()
                    .HasForeignKey(e => e.teamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

