using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Domain.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}

        public User User { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
