using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewsPortal.Domain.Entities
{
    public class Reaction
    {
        [Key]
        public int ReactionId { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public byte ReactionType { get; set; }
        public DateTime CreatedAt { get; set;}

        public Article Article { get; set; }
        public User User { get; set; }
    }
}
