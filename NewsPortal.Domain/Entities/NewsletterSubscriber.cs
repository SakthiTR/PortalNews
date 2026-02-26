using System.ComponentModel.DataAnnotations;


namespace NewsPortal.Domain.Entities
{
    public class NewsletterSubscriber
    {
        [Key]
        public int SubscriberId { get; set; }
        public string Email { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}
