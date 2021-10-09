#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vtbbook.Core.DataAccess.Models
{
    public class DbCoupon : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DbProduct? Product { get; set; }

        [Required]
        public string? Data { get; set; } = string.Empty;

        [Required]
        public DateTime Expire { get; set; }
    }
}
