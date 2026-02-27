using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Domain.Entities
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

        public Article Article { get; set; }
    }
}
