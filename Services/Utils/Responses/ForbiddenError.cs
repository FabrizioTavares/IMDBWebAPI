using FluentResults;

namespace Service.Utils.Responses;

public class ForbiddenError : Error
{
	public ForbiddenError(string errorMessage) : base(errorMessage) { }
}