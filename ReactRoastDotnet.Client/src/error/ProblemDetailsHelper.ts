// My import.
import type ProblemDetails from "../models/ProblemDetails.ts";

function getTitle(result: unknown, errorMessage?: string) {
    if (typeof result === "object" && result !== null && "title" in result && typeof result.title === "string") {
        return result.title;
    }
    if (errorMessage) {
        return errorMessage;
    }
    return "Server Error";
}

function getStatus(result: unknown): number {
    if (typeof result === "object" && result !== null && "status" in result && typeof result.status === "number") {
        return result.status;
    }
    return 500;
}

function getDetail(result: unknown): string | undefined {
    if (typeof result === "object" && result !== null && "detail" in result && typeof result.detail === "string") {
        return result.detail;
    }
}

function getErrors(result: unknown): Record<string, string[]> | undefined {
    if (typeof result === "object" && result !== null && "errors" in result && typeof result.errors === "object") {
        return result.errors as Record<string, string[]>;
    }
}


function getProblemDetails(responseResult: unknown, errorMessage?: string): ProblemDetails {
    return {
        title: getTitle(responseResult, errorMessage),
        status: getStatus(responseResult),
        detail: getDetail(responseResult),
        errors: getErrors(responseResult)
    };
}

export default getProblemDetails;
