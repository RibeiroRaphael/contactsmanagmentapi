using Contactsmanagment.Data;
using Contactsmanagment.Models;
using Contactsmanagment.Models.Dtos.Contacts;
using Contactsmanagment.Repositories;
using Contactsmanagment.Repositories.Interfaces;
using Contactsmanagment.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactsmanagement.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _repositoryMock;
        private readonly ApplicationDbContext _context;
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _repositoryMock = new Mock<IContactRepository>();
            _context = new ApplicationDbContext(options);
            _service = new ContactService(_repositoryMock.Object, _context);

        }

        [Fact]
        public async Task CreateAsync_ShouldException_WhenEmailAlreadyExists()
        {
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()));

            var dto = new CreateContactDto
            {
                Name = "Raphael",
                Email = "raphaelribeiro331@gmail.com",
                Phone = "+5535991494007",
                Ddd = 35
            };

            var action = async () => await _service.Create(dto);

            await action.Should().ThrowAsync<Exception>();
        }


        [Fact]
        public async Task CreateAsync_ShouldCreateContact_WhenDataValid()
        {
            var region = new Region
            {
                Id = Guid.NewGuid(),
                Ddd = 35,
                Name = "Minas Gerais",
                IsActive = true
            };

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            _repositoryMock
                .Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var dto = new CreateContactDto
            {
                Name = "Raphael",
                Email = "raphael@email.com",
                Phone = "35991494007",
                Ddd = region.Ddd
            };

            var result = await _service.Create(dto);

            result.Should().NotBeNull();
            result.Name.Should().Be("Raphael");
        }






    }
}
