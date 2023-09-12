import toast, {ErrorIcon} from "react-hot-toast";
// My Imports.
import type {ProblemDetails} from "get-problem-details";
import ProblemDetailsToast from "./ProblemDetailsToast.tsx";

function problemToast(problemDetails: ProblemDetails) {
    toast((toast) => <ProblemDetailsToast toast={toast} problemDetails={problemDetails}/>, {
        icon: <ErrorIcon/>,
    });
}

export default problemToast;
