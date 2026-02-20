using Contactsmanagment.Models;

namespace Contactsmanagment.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Contact contact);

        Task<Contact?> GetByIdAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<bool> ExistsByEmailAsync(string email);
    }
}
