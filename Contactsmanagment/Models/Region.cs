using System.ComponentModel.DataAnnotations;

namespace Contactsmanagment.Models
{
    public class Region
    {
        public Guid Id { get; set; }
        public int Ddd { get;set; }
        public required string Name { get; set; }    
        public bool IsActive { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
