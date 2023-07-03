using ErrorOr;

namespace ReactRoastDotnet.Data.Common.Errors;

public partial class Errors
{
    public static class Cart
    {
        public static Error NotFound(string description) => Error.NotFound(
            code: "Product.NotFound",
            description: description
        );
        public static Error FailedToSaveChanges(string description) => Error.Failure(
            code: "Product.FailedToSaveChanges",
            description: description
        );
    }
    
    
}