using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; } 
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}
