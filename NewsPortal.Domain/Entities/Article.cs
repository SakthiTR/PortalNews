using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewsPortal.Domain.Entities
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int Status { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
        public ICollection<ArticleTranslation> ArticleTranslations { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
        public virtual ICollection<Comment> Comments { get; set; }
        public ICollection<Media> Medias { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        //public ICollection<Notification> Notifications { get; set; }
    }
}
