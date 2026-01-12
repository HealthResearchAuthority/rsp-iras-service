namespace Rsp.Service.UnitTests.Fixtures;

public class NoRecursionInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public NoRecursionInlineAutoDataAttribute(params object[] values)
        : base(new NoRecursionAutoDataAttribute(), values)
    {
    }
}