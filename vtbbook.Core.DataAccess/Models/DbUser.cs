#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vtbbook.Core.DataAccess.Models
{
    public class DbUser : BaseEntity, IEquatable<Guid>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; } = string.Empty;

        public bool Equals(Guid other)
        {
            return Id == other;
        }
    }
}
