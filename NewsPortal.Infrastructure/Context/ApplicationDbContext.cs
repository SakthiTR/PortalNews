using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;


namespace NewsPortal.Infrastructure.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTranslation> ArticleTranslations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<NewsletterSubscriber> newsletterSubscribers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ErrorLogs> ErrorLog { get; set; }
        public DbSet<Language> Language { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configuration (optional, EF uses conventions)
            modelBuilder.Entity<Author>()
                .HasOne(a => a.User) // Author has one User
                .WithMany(u => u.Authors) // User has many Authors
                .HasForeignKey(a => a.UserId) // ForeignKey is UserId
                .OnDelete(DeleteBehavior.Cascade); // Optional: Set cascade delete

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(au => au.Articles)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticleTranslation>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTranslations)
                .HasForeignKey(at => at.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleId, at.TagId }); // Composite key

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .IsRequired(false); // Mark as optional for guest comments

            modelBuilder.Entity<Media>()
               .HasOne(at => at.Article)
               .WithMany(t => t.Medias)
               .HasForeignKey(at => at.ArticleId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reaction>()
                .HasOne(c => c.User)
                .WithMany(x => x.Reactions)
                .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reaction>()
                .HasOne(c => c.Article)
                .WithMany(x => x.Reactions)
                .HasForeignKey(t => t.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Notification>()
            //    .HasOne(n => n.Article)
            //    .WithMany(a => a.Notifications)
            //    .HasForeignKey(n => n.ArticleId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
