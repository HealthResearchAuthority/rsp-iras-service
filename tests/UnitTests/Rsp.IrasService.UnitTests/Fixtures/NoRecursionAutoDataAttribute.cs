using Rsp.IrasService.UnitTests.Customisation;

namespace Rsp.IrasService.UnitTests.Fixtures;

public class NoRecursionAutoDataAttribute : AutoDataAttribute
{
    public NoRecursionAutoDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture();

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customize(new RemoveNavigationPropertiesCustomization());

            return fixture;
        })
    { }
}