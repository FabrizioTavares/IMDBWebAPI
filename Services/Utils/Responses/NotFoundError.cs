using FluentResults;

namespace Service.Utils.Responses;

public class NotFoundError : Error
{
    public NotFoundError(string errorMessage) : base(errorMessage) { }
}