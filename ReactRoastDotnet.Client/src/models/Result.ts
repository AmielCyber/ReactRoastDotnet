// My import.
import type {ProblemDetails} from "problem-details-mapper";

type Result<T, P = ProblemDetails> =
    | { ok: true; value: T }
    | { ok: false; problemDetails: P };

export default Result;

