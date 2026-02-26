using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Domain.Entities
{
    public class ErrorLogs
    {
        [Key]
        public int Id { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
        public string StackTrace {  get; set; } = string.Empty;
        public DateTime ErrorTime { get; set; } = DateTime.UtcNow;
        public string Source { get; set; } = string.Empty;
    }
}
