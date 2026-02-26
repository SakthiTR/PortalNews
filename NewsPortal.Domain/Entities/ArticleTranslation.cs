using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Domain.Entities
{
    public class ArticleTranslation
    {
        [Key]
        public int TranslationId { get; set; }

        [ForeignKey("Article")]
        public int ArticleId  { get; set; }

        [Required]
        [MaxLength(5)]
        public string LanguageCode  { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content  { get; set; }

        public Article Article { get; set; }
    }
}
