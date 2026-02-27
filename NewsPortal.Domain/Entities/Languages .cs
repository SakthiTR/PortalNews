using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }             // Unique ID
        public string Code { get; set; }        // e.g., "en", "ta", "hi"
        public string Name { get; set; }        // e.g., "English", "Tamil"
        public bool IsDefault { get; set; }     // true if default language
        public bool IsActive { get; set; }      // true if enabled in the system
        public int DisplayOrder { get; set; }   // optional: sort in dropdown
        public string? CountryCode { get; set; } = string.Empty;
    }
}
