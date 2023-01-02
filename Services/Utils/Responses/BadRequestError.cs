using FluentResults;

namespace Service.Utils.Responses;

public class BadRequestError : Error
{
    public BadRequestError(string errorMessage) : base(errorMessage) { }
}
