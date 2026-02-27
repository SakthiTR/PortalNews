using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FName { get; set; }

        public string LName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Mobile number must be between 10 and 15 digits and may include a '+' prefix.")]
        public string MobileNo { get; set; }

        [Required] 
        [MinLength(6)] 
        [MaxLength(100)] 
        public string Password { get; set; }

        public byte  UserType { get; set; }
        public string IPAddress { get; set; } 
        public string Device { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<Author> Authors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
