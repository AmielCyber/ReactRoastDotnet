// My import.
import type {ProblemDetails} from "get-problem-details";

type Result<T, P = ProblemDetails> =
    | { ok: true; value: T }
    | { ok: false; problemDetails: P };

export default Result;

