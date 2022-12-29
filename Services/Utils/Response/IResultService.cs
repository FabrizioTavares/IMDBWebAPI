using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Response
{
    public interface IResultService
    {
        Result<T> CreateResult<T>(T? data, bool success, int statusCode, string? errorMessage = default);
        Result CreateResult(bool success, int statusCode, string? errorMessage = default);
    }
}
