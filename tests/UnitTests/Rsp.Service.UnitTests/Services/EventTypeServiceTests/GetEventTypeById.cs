using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.EventTypeServiceTests
{
    public class GetEventTypeById : TestServiceBase<EventTypeService>
    {
        private readonly EventTypeRepository _templateRepository;
        private readonly RspContext _context;

        public GetEventTypeById()
        {
            var options = new DbContextOptionsBuilder<RspContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

            _context = new RspContext(options);
            _templateRepository = new EventTypeRepository(_context);
        }

        [Theory]
        [InlineAutoData(5)]
        public async Task Returns_Event_Type_By_Id(int records, Generator<EventType> generator)
        {
            // Arrange
            Mocker.Use<IEventTypeRepository>(_templateRepository);

            Sut = Mocker.CreateInstance<EventTypeService>();

            // seed data using number of records to seed
            var templates = await TestData.SeedData(_context, generator, records);

            // get the random application id between 0 and 4
            var eventTypeId = templates[Random.Shared.Next(0, 4)].Id;

            // Act
            var irasApplication = await Sut.GetById(eventTypeId);

            // Assert
            irasApplication.ShouldNotBeNull();
            irasApplication.ShouldBeOfType<EventType>();
            irasApplication.Id.ShouldBe(eventTypeId);
        }
    }
}