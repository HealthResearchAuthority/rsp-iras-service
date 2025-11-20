using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Customisation;

public class RemoveNavigationPropertiesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<ProjectRecordAuditTrail>(c =>
            c.Without(x => x.ProjectRecord));

        fixture.Customize<ProjectRecord>(c =>
            c.Without(x => x.ProjectRecordAnswers)
             .Without(x => x.ProjectModifications));

        fixture.Customize<ProjectRecordAnswer>(c =>
            c.Without(x => x.ProjectRecord)
             .Without(x => x.ProjectPersonnel));
    }
}