using Moq.AutoMock;

namespace Rsp.IrasService.UnitTests;

public class TestServiceBase
{
    public AutoMocker Mocker { get; }

    public TestServiceBase()
    {
        Mocker = new AutoMocker();
    }
}