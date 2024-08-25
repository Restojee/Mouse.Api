using Mouse.NET.Data.Models;

namespace Mouse.NET.Data;
using Microsoft.EntityFrameworkCore;

public class MouseDbContext : DbContext
{
    public MouseDbContext(DbContextOptions<MouseDbContext> options) : base(options) {}
    
    public DbSet<LevelEntity> Levels { get; set; }
    
    public DbSet<LevelFavoriteEntity> LevelFavorites { get; set; }
    
    public DbSet<LevelCompletedEntity> LevelCompleted { get; set; }
    
    public DbSet<LevelCommentEntity> LevelComments { get; set; }
    
    public DbSet<TagEntity> Tags { get; set; }
    
    public DbSet<TipEntity> Tips { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<LevelTagRelation> LevelTagRelations { get; set; }
    
    public DbSet<LevelVisitEntity> LevelVisits { get; set; }
    
    public DbSet<MessageEntity> Messages { get; set; }
    
    public DbSet<LevelNoteEntity> LevelNotes { get; set; }
    
    public DbSet<InviteEntity> Invites { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<LevelEntity>()
            .HasMany(l => l.Tags)
            .WithMany(t => t.Levels)
            .UsingEntity<LevelTagRelation>(
                l => l
                    .HasOne(x => x.Tag)
                    .WithMany()
                    .HasForeignKey(l => l.TagId),
                t => t
                    .HasOne(x => x.Level)
                    .WithMany()
                    .HasForeignKey(t => t.LevelId)
            );
    }
}