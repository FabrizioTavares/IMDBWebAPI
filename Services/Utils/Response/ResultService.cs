using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Response
{
    public class ResultService : IResultService
    {
        public Result<T> CreateResult<T>(T? data, bool success, int statusCode, string? errorMessage = default)
        {
            return new Result<T>(data, success, statusCode, errorMessage);
        }

        public Result CreateResult(bool success, int statusCode, string? errorMessage = default)
        {
            return new Result(success, statusCode, errorMessage);
        }
    }
}
