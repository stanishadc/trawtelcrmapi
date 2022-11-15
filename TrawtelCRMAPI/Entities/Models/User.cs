using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Password { get; set; }
        public Guid AgentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Status { get; set; }
    }
}
