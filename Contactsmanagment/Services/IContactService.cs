using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos;

namespace Contactsmanagment.Services
{
    public interface IContactService
    {
        Task<Contact> Create(Contact contact);
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact?> GetById(Guid id);
        Task Update(Guid id, UpdateContactDto dto);
        Task Delete(Guid id);
    }
}
