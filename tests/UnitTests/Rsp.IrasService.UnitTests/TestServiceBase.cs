using Rsp.IrasService.Application.Mappping;

namespace Rsp.IrasService.UnitTests;

public class TestServiceBase
{
    public TestServiceBase()
    {
        Mocker = new AutoMocker();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MappingRegister).Assembly);
    }

    public AutoMocker Mocker { get; }
}