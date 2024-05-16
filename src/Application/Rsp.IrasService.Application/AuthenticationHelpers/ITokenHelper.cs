using Microsoft.Extensions.Primitives;

namespace Rsp.IrasService.Application.AuthenticationHelpers;

public interface ITokenHelper
{
    string DeBearerizeAuthToken(StringValues authToken);

    string BearerizeAuthToken(StringValues authToken);
}