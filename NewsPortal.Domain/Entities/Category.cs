using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }    
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}

        public ICollection<Article> Articles { get; set; }

    }
}
