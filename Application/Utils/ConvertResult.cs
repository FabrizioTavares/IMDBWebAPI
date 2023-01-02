using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Service.Utils.Responses;

namespace Application.Utils
{
    public class ConvertResult
    {
        /*
        public static IResult Convert(Result result)
        {
            if (result.IsSuccess)
                return Results.NoContent();

            if (result.Errors.First() is ForbiddenError forbiddenError)
                return Results.Json(forbiddenError.Message, statusCode: 403);

            if (result.Errors.First() is NotFoundError notFoundError)
                return Results.Json(notFoundError.Message, statusCode: 404);

            if (result.Errors.First() is BadRequestError badRequestError)
                return Results.Json(badRequestError.Message, statusCode: 400);

            return Results.Json(result.Errors.FirstOrDefault(), statusCode: 500);
        }
        */
        public static IActionResult Convert(Result result)
        {
            if (result.IsSuccess)
                return new NoContentResult();

            if (result.Errors.First() is ForbiddenError forbiddenError)
                return new ObjectResult(forbiddenError.Message) { StatusCode = 403 };

            if (result.Errors.First() is NotFoundError notFoundError)
                return new ObjectResult(notFoundError.Message) { StatusCode = 404 };

            if (result.Errors.First() is BadRequestError badRequestError)
                return new ObjectResult(badRequestError.Message) { StatusCode = 400 };

            return new ObjectResult(result.Errors.FirstOrDefault()) { StatusCode = 500 };
        }

        public static IActionResult Convert<T>(Result<T> result)
        {
            if (result.IsSuccess)
                if (result.Value != null)
                    return new OkObjectResult(result.Value);
                else
                    return new NoContentResult();

            return Convert(result.ToResult());
        }
    }
}
