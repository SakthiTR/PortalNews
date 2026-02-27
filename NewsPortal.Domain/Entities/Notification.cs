using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace NewsPortal.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; } // Primary key

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to the User table

        //[ForeignKey("Article")]
        public int? ArticleId { get; set; } // Optional foreign key to the Article table

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } // Notification title

        [Required]
        public string Message { get; set; } // Notification message

        public bool IsRead { get; set; } = false; // Indicates if the notification has been read

        [Required]
        public DateTime CreatedAt { get; set; } // Timestamp when the notification was created

        [Required]
        public DateTime ExpiryDate { get; set; } // Optional expiry date for the notification

        // Navigation properties
        public User User { get; set; } // User who received the notification
       // public Article Article { get; set; } // Optional linked article
    }

}
