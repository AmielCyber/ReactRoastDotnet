// My import.
import type ProblemDetails from "./ProblemDetails.ts";

function getType(result: unknown): string | undefined {
    if (!!result && typeof result === "object" && "type" in result && typeof result.type === "string") {
        return result.type;
    }
}

function getTitle(result: unknown, errorMessage = "Server Error"): string {
    if(!!result && typeof result === "object"){
        if ("title" in result && typeof result.title === "string") {
            return result.title;
        }
        // If the response only returns a string with failed status code.
        if ("statusText" in result && typeof result.statusText === "string") {
            return result.statusText;
        }
    }
    // Default to the error message or to Server Error.
    return errorMessage;
}

function getStatus(result: unknown): number {
    if (!!result && typeof result === "object" && "status" in result && typeof result.status === "number") {
        return result.status;
    }
    // Default to a server error.
    return 500;
}

function getDetail(result: unknown): string | undefined {
    if (!!result && typeof result === "object" && "detail" in result && typeof result.detail === "string") {
        return result.detail;
    }
}

function getTraceId(result: unknown): string | undefined {
    if (!!result && typeof result === "object" && "traceId" in result && typeof result.traceId === "string") {
        return result.traceId;
    }
}

function getErrors(result: unknown): Record<string, string[]> | undefined {
    if (!!result
        && typeof result === "object"
        && "errors" in result
        && typeof result.errors === "object"
        && result.errors !== null
    ) {
        return result.errors as Record<string, string[]>;
    }
}

function getProblemDetails(responseResult: unknown, errorMessage?: string): ProblemDetails {
    return {
        type: getType(responseResult),
        title: getTitle(responseResult, errorMessage),
        status: getStatus(responseResult),
        detail: getDetail(responseResult),
        traceId: getTraceId(responseResult),
        errors: getErrors(responseResult)
    };
}

export default getProblemDetails;
