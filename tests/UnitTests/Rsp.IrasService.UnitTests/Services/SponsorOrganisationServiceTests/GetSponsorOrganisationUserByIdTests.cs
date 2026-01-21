using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.SponsorOrganisationServiceTests;

public class GetSponsorOrganisationUserByIdTests : TestServiceBase<SponsorOrganisationsService>
{
    private readonly IrasContext _context;
    private readonly SponsorOrganisationRepository _sponsorOrganisationRepository;

    public GetSponsorOrganisationUserByIdTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _sponsorOrganisationRepository = new SponsorOrganisationRepository(_context);
    }

    [Theory, AutoData]
    public async Task GetSponsorOrganisationUserById_ShouldReturnUser
    (
        Guid id,
        string userEmail,
        SponsorOrganisationUser response
    )
    {
        // Arrange
        var mockHttpContextAccessor = new HttpContextAccessor()
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new(ClaimTypes.Email, userEmail)
                ]))
            }
        };
        response.Email = userEmail;

        var sponsorOrganisationRepositoryMock = new Mock<ISponsorOrganisationsRepository>();
        sponsorOrganisationRepositoryMock
            .Setup(x => x.GetSponsorOrganisationUserById(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(response);

        Mocker.Use(sponsorOrganisationRepositoryMock.Object);
        Mocker.Use<IHttpContextAccessor>(mockHttpContextAccessor);

        Sut = Mocker.CreateInstance<SponsorOrganisationsService>();

        // Act
        var result = await Sut.GetSponsorOrganisationUserById(id);

        // Assert
        result.ShouldNotBeNull();
    }
}