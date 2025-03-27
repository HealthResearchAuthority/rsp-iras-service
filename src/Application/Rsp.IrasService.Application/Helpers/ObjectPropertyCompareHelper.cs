using KellermanSoftware.CompareNetObjects;

namespace Rsp.IrasService.Application.Helpers;

public class ObjectPropertyCompareHelper
{
    public ComparisonResult Compare<T>(T object1, T object2, CompareLogic? compareLogic = null)
    {
        var config = compareLogic == null ? new CompareLogic() : compareLogic;

        var result = config.Compare(object1, object2);

        return result;
    }
}