namespace Rsp.IrasService.UnitTests.Fixtures;

public class NoRecursionAutoDataAttribute : AutoDataAttribute
{
    public NoRecursionAutoDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        })
    {
    }
}