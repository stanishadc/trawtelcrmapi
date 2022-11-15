using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("UserKeys")]
    public class UserKey
    {
        [Key]
        public Guid UserKeyId { get; set; }
        [Required(ErrorMessage = "apikey is required")]
        public string? SecretKey { get; set; }
        public string? IPAddress { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid AgentId { get; set; }
    }
}
