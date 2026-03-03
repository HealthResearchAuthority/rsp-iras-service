using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

public class DuplicateModificationTests : TestServiceBase<ProjectModificationService>
{
    private readonly Mock<IProjectModificationRepository> _projectModificationRepository = new();
    private readonly Mock<IProjectPersonnelRepository> _projectPersonnelRepository = new();
    private readonly Mock<IBlobService> _blobService = new();

    [Fact]
    public async Task Returns_Null_When_No_Modification()
    {
        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync((ProjectModification?)null);

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = Guid.NewGuid(),
            ProjectRecordId = "12345678",
            UpdatedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        });

        response.ShouldBeNull();

        _projectModificationRepository.Verify(x => x.CreateModification(It.IsAny<ProjectModification>()), Times.Never);
        _blobService.Verify(x => x.CopyBlobWithinContainerAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Duplicates_Modification_When_Found_And_No_Changes_Answers_Or_Documents()
    {
        var existing = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "219159/9",
            Status = "Draft",
            Category = "Cat",
            ModificationType = "Type",
            ReviewType = "Review",
            RevisionDescription = "Desc",
            ReviewerId = "",
            ReviewerEmail = "r@example.com",
            ReviewerName = "Reviewer",
            ReviewerComments = "comments",
            ReasonNotApproved = "reason",
            ProvisionalReviewOutcome = "outcome",
            SentToSponsorDate = DateTime.UtcNow.AddDays(-2),
            SentToRegulatorDate = DateTime.UtcNow.AddDays(-1),
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-1),
            IsDuplicate = false
        };

        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(existing);

        // return the passed-in entity (typical repo stub)
        _projectModificationRepository
            .Setup(x => x.CreateModification(It.IsAny<ProjectModification>()))
            .ReturnsAsync((ProjectModification m) => m);

        _projectModificationRepository
            .Setup(x => x.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(new List<ProjectModificationChange>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationAnswer>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentSpecification>()))
            .ReturnsAsync(new List<ModificationDocument>());

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = existing.Id,
            ProjectRecordId = existing.ProjectRecordId,
        });

        response.ShouldNotBeNull();

        // verify the NEW mod properties without callbacks
        _projectModificationRepository.Verify(x => x.CreateModification(It.Is<ProjectModification>(m =>
            m.ProjectRecordId == existing.ProjectRecordId
            && m.ModificationIdentifier == "219159/"
            && m.IsDuplicate
            && m.DuplicatedFromModificationIdentifier == existing.ModificationIdentifier
            && m.Status == ModificationStatus.InDraft
            && m.Category == existing.Category
            && m.ModificationType == existing.ModificationType
            && m.ReviewType == existing.ReviewType
            && m.RevisionDescription == existing.RevisionDescription
            && m.ReviewerId == null
            && m.ReviewerEmail == null
            && m.ReviewerName == null
            && m.ReviewerComments == null
            && m.ReasonNotApproved == null
            && m.ProvisionalReviewOutcome == null
            && m.SentToSponsorDate == null
            && m.SentToRegulatorDate == null
            && m.CreatedBy == existing.CreatedBy
            && m.UpdatedBy == existing.UpdatedBy
        )), Times.Once);

        _projectModificationRepository.Verify(x => x.CreateModificationChange(It.IsAny<ProjectModificationChange>()), Times.Never);

        _projectPersonnelRepository.Verify(x => x.SaveModificationChangeResponses(
            It.IsAny<GetModificationChangeAnswersSpecification>(),
            It.IsAny<List<ProjectModificationChangeAnswer>>()), Times.Never);

        _projectPersonnelRepository.Verify(x => x.SaveModificationResponses(
            It.IsAny<GetModificationAnswersSpecification>(),
            It.IsAny<List<ProjectModificationAnswer>>()), Times.Never);

        _projectPersonnelRepository.Verify(x => x.SaveModificationDocumentResponses(
            It.IsAny<SaveModificationDocumentsSpecification>(),
            It.IsAny<List<ModificationDocument>>()), Times.Never);

        _projectPersonnelRepository.Verify(x => x.SaveModificationDocumentAnswerResponses(
            It.IsAny<SaveModificationDocumentAnswerSpecification>(),
            It.IsAny<List<ModificationDocumentAnswer>>()), Times.Never);

        _blobService.Verify(x => x.CopyBlobWithinContainerAsync(
            It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Duplicates_Modification_When_Identifier_Is_Null_Uses_EmptyPrefix()
    {
        var existing = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = null,
            Category = "Cat",
            ModificationType = "Type",
            ReviewType = "Review",
            RevisionDescription = "Desc",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-1),
        };

        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(existing);

        _projectModificationRepository
            .Setup(x => x.CreateModification(It.IsAny<ProjectModification>()))
            .ReturnsAsync((ProjectModification m) => m);

        _projectModificationRepository
            .Setup(x => x.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(new List<ProjectModificationChange>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationAnswer>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentSpecification>()))
            .ReturnsAsync(new List<ModificationDocument>());

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = existing.Id,
            ProjectRecordId = existing.ProjectRecordId,
        });

        response.ShouldNotBeNull();

        _projectModificationRepository.Verify(x => x.CreateModification(It.Is<ProjectModification>(m =>
            m.ModificationIdentifier == "/"
            && m.IsDuplicate
            && m.DuplicatedFromModificationIdentifier == null
        )), Times.Once);
    }

    [Fact]
    public async Task Duplicates_Changes_And_Saves_ChangeAnswers_When_They_Exist()
    {
        var existing = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "219159/1",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-1),
        };

        var existingChangeId = Guid.NewGuid();

        var existingChanges = new List<ProjectModificationChange>
        {
            new()
            {
                Id = existingChangeId,
                ProjectModificationId = existing.Id,
                ModificationType = "Type",
                Category = "Cat",
                ReviewType = "Review",
                AreaOfChange = "Area",
                SpecificAreaOfChange = "Specific",
                Status = "Draft",
                CreatedBy = "u",
                UpdatedBy = "u",
                CreatedDate = DateTime.UtcNow.AddDays(-9),
                UpdatedDate = DateTime.UtcNow.AddDays(-8),
            }
        };

        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(existing);

        _projectModificationRepository
            .Setup(x => x.CreateModification(It.IsAny<ProjectModification>()))
            .ReturnsAsync((ProjectModification m) => m);

        _projectModificationRepository
            .Setup(x => x.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(existingChanges);

        _projectModificationRepository
            .Setup(x => x.CreateModificationChange(It.IsAny<ProjectModificationChange>()))
            .ReturnsAsync(new ProjectModificationChange());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationChangeAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationChangeAnswer>
            {
                new() { ProjectModificationChangeId = existingChangeId, IsDuplicate = false }
            });

        _projectPersonnelRepository
            .Setup(x => x.SaveModificationChangeResponses(It.IsAny<GetModificationChangeAnswersSpecification>(),
                It.IsAny<List<ProjectModificationChangeAnswer>>()))
            .Returns(Task.CompletedTask);

        // no mod answers/docs
        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationAnswer>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentSpecification>()))
            .ReturnsAsync(new List<ModificationDocument>());

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = existing.Id,
            ProjectRecordId = existing.ProjectRecordId
        });

        response.ShouldNotBeNull();

        _projectModificationRepository.Verify(x => x.CreateModificationChange(It.Is<ProjectModificationChange>(c =>
            c.IsDuplicate
            && c.Status == ModificationStatus.InDraft
            && c.ModificationType == existingChanges[0].ModificationType
            && c.Category == existingChanges[0].Category
            && c.ReviewType == existingChanges[0].ReviewType
            && c.AreaOfChange == existingChanges[0].AreaOfChange
            && c.SpecificAreaOfChange == existingChanges[0].SpecificAreaOfChange
        )), Times.Once);

        _projectPersonnelRepository.Verify(x => x.SaveModificationChangeResponses(
            It.IsAny<GetModificationChangeAnswersSpecification>(),
            It.Is<List<ProjectModificationChangeAnswer>>(list =>
                list.Count == 1
                && list[0].IsDuplicate
            )), Times.Once);
    }

    [Fact]
    public async Task Duplicates_ModificationAnswers_And_Saves_When_They_Exist()
    {
        var existing = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "219159/1",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-1),
        };

        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(existing);

        _projectModificationRepository
            .Setup(x => x.CreateModification(It.IsAny<ProjectModification>()))
            .ReturnsAsync((ProjectModification m) => m);

        _projectModificationRepository
            .Setup(x => x.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(new List<ProjectModificationChange>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationAnswer>
            {
                new() { ProjectModificationId = existing.Id, IsDuplicate = false }
            });

        _projectPersonnelRepository
            .Setup(x => x.SaveModificationResponses(It.IsAny<GetModificationAnswersSpecification>(), It.IsAny<List<ProjectModificationAnswer>>()))
            .Returns(Task.CompletedTask);

        // no docs
        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentSpecification>()))
            .ReturnsAsync(new List<ModificationDocument>());

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = existing.Id,
            ProjectRecordId = existing.ProjectRecordId
        });

        response.ShouldNotBeNull();

        _projectPersonnelRepository.Verify(x => x.SaveModificationResponses(
            It.IsAny<GetModificationAnswersSpecification>(),
            It.Is<List<ProjectModificationAnswer>>(list =>
                list.Count == 1
                && list[0].IsDuplicate
            )), Times.Once);
    }

    [Fact]
    public async Task Duplicates_Documents_And_DocumentAnswers_And_Copies_Blob()
    {
        var existing = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "219159/1",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-1),
        };

        var oldPath = $"323477/{existing.Id}/file.pdf";

        _projectModificationRepository
            .Setup(x => x.GetModification(It.IsAny<GetModificationSpecification>()))
            .ReturnsAsync(existing);

        _projectModificationRepository
            .Setup(x => x.CreateModification(It.IsAny<ProjectModification>()))
            .ReturnsAsync((ProjectModification m) => m);

        _projectModificationRepository
            .Setup(x => x.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(new List<ProjectModificationChange>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationAnswersSpecification>()))
            .ReturnsAsync(new List<ProjectModificationAnswer>());

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentSpecification>()))
            .ReturnsAsync(new List<ModificationDocument>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ProjectModificationId = existing.Id,
                    DocumentStoragePath = oldPath,
                    Status = "Complete",
                    IsDuplicate = false
                }
            });

        _projectPersonnelRepository
            .Setup(x => x.GetResponses(It.IsAny<GetModificationDocumentAnswersSpecification>()))
            .ReturnsAsync(new List<ModificationDocumentAnswer>
            {
                new() { Id = Guid.NewGuid(), IsDuplicate = false }
            });

        _projectPersonnelRepository
            .Setup(x => x.SaveModificationDocumentResponses(It.IsAny<SaveModificationDocumentsSpecification>(), It.IsAny<List<ModificationDocument>>()))
            .Returns(Task.CompletedTask);

        _projectPersonnelRepository
            .Setup(x => x.SaveModificationDocumentAnswerResponses(It.IsAny<SaveModificationDocumentAnswerSpecification>(), It.IsAny<List<ModificationDocumentAnswer>>()))
            .Returns(Task.CompletedTask);

        _blobService
            .Setup(x => x.CopyBlobWithinContainerAsync(It.IsAny<string>(), It.IsAny<string>(), false, false, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        Mocker.Use(_projectModificationRepository.Object);
        Mocker.Use(_projectPersonnelRepository.Object);
        Mocker.Use(_blobService.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var response = await Sut.DuplicateModification(new DuplicateModificationRequest
        {
            ExistingModificationId = existing.Id,
            ProjectRecordId = existing.ProjectRecordId
        });

        response.ShouldNotBeNull();

        _blobService.Verify(x => x.CopyBlobWithinContainerAsync(
            It.Is<string>(s => s == oldPath),
            It.Is<string>(d => d.Contains(existing.Id.ToString()) == false && d.Contains("file.pdf")),
            false, false, It.IsAny<CancellationToken>()), Times.Once);

        _projectPersonnelRepository.Verify(x => x.SaveModificationDocumentResponses(
            It.IsAny<SaveModificationDocumentsSpecification>(),
            It.Is<List<ModificationDocument>>(docs =>
                docs.Count == 1
                && docs[0].IsDuplicate
                && docs[0].Status == "Complete"
                && docs[0].DocumentStoragePath.Contains(existing.Id.ToString()) == false
            )), Times.Once);

        _projectPersonnelRepository.Verify(x => x.SaveModificationDocumentAnswerResponses(
            It.IsAny<SaveModificationDocumentAnswerSpecification>(),
            It.Is<List<ModificationDocumentAnswer>>(answers => answers.Count == 1 && answers[0].IsDuplicate)
        ), Times.Once);
    }
}