using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos.Contacts;

namespace Contactsmanagment.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactResponseDto> Create(CreateContactDto contact);
        Task<IEnumerable<ContactResponseDto>> GetAll();
        Task<ContactResponseDto?> GetById(Guid id);
        Task Update(Guid id, UpdateContactDto dto);
        Task Delete(Guid id);
    }
}
