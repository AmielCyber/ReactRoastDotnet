// My import.
import ProblemDetails from "../problem-details/ProblemDetails.ts";

type Result<T, P = ProblemDetails> =
    | { ok: true; value: T }
    | { ok: false; problemDetails: P };

export default Result;

