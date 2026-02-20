using Contactsmanagment.Data;
using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos;
using Contactsmanagment.Repositories;
using Contactsmanagment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contactsmanagment.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ApplicationDbContext _context;
        public ContactService(IContactRepository repository, ApplicationDbContext context)
        {
            _contactRepository = repository;
            _context = context;
        }

        public async Task<Contact> Create(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact?> GetById(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task Update(Guid id, UpdateContactDto dto)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact is null)
                throw new Exception("Contact not found");

            // Verificar email duplicado (se mudou)
            if (contact.Email != dto.Email)
            {
                var emailExists = await _contactRepository.ExistsByEmailAsync(dto.Email);

                if (emailExists)
                    throw new Exception("Email already exists");
            }

            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == dto.RegionId); 
            
            if (region is null)
                throw new Exception("Region not found");

            if (!region.IsActive)
                throw new Exception("Region is inactive");

            // Atualizar dados
            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.Phone = dto.Phone;
            contact.RegionId = dto.RegionId;

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task Delete(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact != null)
                await _contactRepository.DeleteAsync(contact);
        }
    }
}
