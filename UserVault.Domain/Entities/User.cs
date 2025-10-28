using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Domain.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public  string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public  string LastName { get; set; } = String.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
