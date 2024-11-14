namespace Goal.Infra.Crosscutting.Errors;

public enum ErrorType
{
    InvalidInput,
    ResourceNotFound,
    BusinessRule,
    ServiceUnavailable,
    UnexpectedError
}

public record AppError(ErrorType Type, string Detail, string Code);

