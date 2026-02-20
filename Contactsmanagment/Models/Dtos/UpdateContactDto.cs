namespace Contactsmanagment.Models.Dtos
{
    public class UpdateContactDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid RegionId { get; set; }

    }
}
