using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Commands;
using Rsp.IrasService.Application.CQRS.Handlers.CommandHandlers;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.CommandHandlers
{
    public class CreateReviewBodyHandlerTests
    {
        private readonly Mock<IReviewBodyService> _reviewBodyServiceMock;
        private readonly CreateReviewBodyHandler _handler;

        public CreateReviewBodyHandlerTests()
        {
            _reviewBodyServiceMock = new Mock<IReviewBodyService>();
            _handler = new CreateReviewBodyHandler(_reviewBodyServiceMock.Object);
        }

        [Fact]
        public async Task Handle_GetApplicationsQuery_ShouldReturnListOfApplications()
        {
            // Arrange
            var request = new ReviewBodyDto()
            {
                OrganisationName = "App-123", Description = "Approved"
            };

            var expectedResponse = new ReviewBodyDto()
            {
                OrganisationName = "App-123",
                Description = "Approved"
            };

            var query = new CreateReviewBodyCommand(request);

            _reviewBodyServiceMock
                .Setup(service => service.CreateReviewBody(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();

            _reviewBodyServiceMock.Verify(service => service.CreateReviewBody(It.IsAny<ReviewBodyDto>()), Times.Once);
        }
    }
}