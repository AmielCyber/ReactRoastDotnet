using ErrorOr;

namespace ReactRoastDotnet.Data.Common.Errors;

public static partial class Errors
{
    public class Order
    {
        public static Error NotFound(string description) => Error.NotFound(
            code: "Order.NotFound",
            description: description
        );
        public static Error FailedToSaveChanges(string description) => Error.Failure(
            code: "Order.FailedToSaveChanges",
            description: description
        );
        public static Error Forbidden(string description) => Error.Custom(
            type: MyErrorTypes.Forbidden,
            code: "Order.Forbidden",
            description: description
        );
    }
    
}