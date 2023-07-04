using ErrorOr;

namespace ReactRoastDotnet.Data.Common.Errors;

public static partial class Errors
{
    public static class Cart
    {
        public static Error NotFound(string description) => Error.NotFound(
            code: "Cart.NotFound",
            description: description
        );
        public static Error FailedToSaveChanges(string description) => Error.Failure(
            code: "Cart.FailedToSaveChanges",
            description: description
        );
        public static Error BadRequest(string description) => Error.Custom(
            type: MyErrorTypes.BadRequest,
            code: "Cart.BadRequest",
            description: description
        );
    }
    
    
}