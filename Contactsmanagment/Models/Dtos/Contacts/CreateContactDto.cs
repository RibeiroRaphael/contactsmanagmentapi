using System.ComponentModel.DataAnnotations;

namespace Contactsmanagment.Models.Dtos.Contacts
{
    public class CreateContactDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\d{8,11}$")]
        public string Phone { get; set; } = null!;
        [Required]
        [Range(11, 99)]
        public int Ddd { get; set; }
    }
}
