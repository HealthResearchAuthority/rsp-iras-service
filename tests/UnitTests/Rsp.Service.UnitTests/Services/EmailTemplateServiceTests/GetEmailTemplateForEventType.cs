using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.EmailTemplateServiceTests
{
    public class GetEmailTemplateForEventType : TestServiceBase<EmailTemplateService>
    {
        private readonly EmailTemplateRepository _templateRepository;
        private readonly RspContext _context;

        public GetEmailTemplateForEventType()
        {
            var options = new DbContextOptionsBuilder<RspContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

            _context = new RspContext(options);
            _templateRepository = new EmailTemplateRepository(_context);
        }

        [Theory]
        [InlineAutoData(5)]
        public async Task Returns_Template_ForEventType(int records, Generator<EmailTemplate> generator)
        {
            // Arrange
            Mocker.Use<IEmailTemplateRepository>(_templateRepository);

            Sut = Mocker.CreateInstance<EmailTemplateService>();

            // seed data using number of records to seed
            var templates = await TestData.SeedData(_context, generator, records);

            // get the random application id between 0 and 4
            var randomTemplate = templates[Random.Shared.Next(0, 4)];
            var eventTypeId = randomTemplate.EventTypeId;
            var templateId = randomTemplate.TemplateId;

            // Act
            var irasApplication = await Sut.GetEmailTemplateForEventType(eventTypeId);

            // Assert
            irasApplication.ShouldNotBeNull();
            irasApplication.ShouldBeOfType<EmailTemplate>();
            irasApplication.TemplateId.ShouldBe(templateId);
        }
    }
}