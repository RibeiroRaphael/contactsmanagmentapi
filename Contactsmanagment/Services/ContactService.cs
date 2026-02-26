using Contactsmanagment.Data;
using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos.Contacts;
using Contactsmanagment.Repositories;
using Contactsmanagment.Repositories.Interfaces;
using Contactsmanagment.Services.Interfaces;
using Microsoft.AspNetCore.Components;
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

        private static ContactResponseDto Map(Contact contact)
        {
            return new ContactResponseDto()
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Ddd = contact.Region.Ddd,
                RegionName = contact.Region.Name
            };
        }

        public async Task<ContactResponseDto> Create(CreateContactDto contactDto)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == contactDto.RegionId);

            if(region is null)
            {
                throw new Exception("Region não encontrada");
            }

            if(!region.IsActive)
            {
                throw new Exception("Região inativa");
            }
            
            var existsEmail = await _contactRepository.ExistsByEmailAsync(contactDto.Email);
            if(existsEmail)
            {
                throw new Exception("Email já existe");
            }

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = contactDto.Name,
                Email = contactDto.Email,
                Phone = contactDto.Phone,
                RegionId = contactDto.RegionId
            
            };

            await _contactRepository.AddAsync(contact);
            contact.Region = region;

            return Map(contact);
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAll()
        {
            var contacts =  await _contactRepository.GetAllAsync();
            return contacts.Select(Map);
        }

        public async Task<ContactResponseDto?> GetById(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            return Map(contact);
        }

        public async Task Update(Guid id, UpdateContactDto dto)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact is null)
                throw new Exception("Contact not found");

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
