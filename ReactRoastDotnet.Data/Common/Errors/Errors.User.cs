using ErrorOr;

namespace ReactRoastDotnet.Data.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error Unauthorized(string description) => Error.Custom(
            type: MyErrorTypes.Unauthorized,
            code: "User.Unauthorized",
            description: description
        );

        public static Error NotFound(string description) => Error.NotFound(
            code: "User.NotFound",
            description: description
        );

        public static Error ValidationProblem(string code, string description) => Error.Validation(
            code: code,
            description: description
        );
    }
}