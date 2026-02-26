namespace Contactsmanagment.Models.Dtos.Contacts
{
    public class ContactResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int Ddd { get; set; }
        public string RegionName{ get; set; } = null!;
    }
}
