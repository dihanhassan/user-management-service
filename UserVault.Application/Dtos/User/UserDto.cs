using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Application.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
