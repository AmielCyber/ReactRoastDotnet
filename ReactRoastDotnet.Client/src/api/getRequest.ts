import toast from "react-hot-toast";
// My imports.
import getProblemDetails from "get-problem-details";
import problemToast from "../toast/problemToast.tsx";

const baseURL = import.meta.env.VITE_API_URL as string;

function getErrorMessage(e: unknown): string {
    return (e
        && typeof (e) === "object"
        && "message" in e
        && typeof (e.message) === "string") ?
        e.message : "Something went wrong...";
}

async function getRequest<T>(
    url: string,
    method?: string,
    body?: BodyInit,
    headers?: HeadersInit,
    successMessage?: string,
): Promise<T | void> {
    let response: Response | undefined = undefined;

    try {
        response = await fetch(baseURL + url, {
            method: method,
            headers: headers,
            body: body
        });

        if (!response.ok) {
            const responseResult = await response.json() as unknown;
            const problemDetails = getProblemDetails(responseResult);
            problemToast(problemDetails);
            return;
        }

        const result = await response.json() as Promise<T>
        if (successMessage) {
            toast.success(successMessage);
        }
        return result;
    } catch (e) {
        const message = getErrorMessage(e);
        const problemDetails = getProblemDetails(response, message);
        problemToast(problemDetails);
    }
}

export default getRequest;
