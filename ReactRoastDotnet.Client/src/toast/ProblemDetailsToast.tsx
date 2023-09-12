import type {ReactNode} from "react";
import type {Toast} from "react-hot-toast";
import toast from "react-hot-toast";
// My imports.
import type {ProblemDetails} from "get-problem-details";
import XMarkIcon from "../icons/XMarkIcon.tsx";

type Props = {
    toast: Toast;
    problemDetails: ProblemDetails
}

function getProblemDetailsErrors(errors: Record<string, string[]>) {
    const validationErrorList = new Array<ReactNode>();
    for (const [key, val] of Object.entries(errors)) {
        validationErrorList.push(
            <li key={key}>
                <p>{val[0]}</p>
            </li>
        )
    }
    return validationErrorList;
}

function ProblemDetailsToast(props: Props) {
    let errorList: ReactNode[] | null = null;
    if (props.problemDetails.errors) {
        errorList = getProblemDetailsErrors(props.problemDetails.errors);
    }

    return (
        <div className="flex flex-col justify-between items-center py-1">
            <button className="btn btn-sm btn-circle btn-ghost absolute right-0 top-0"
                    aria-label="close"
                    onClick={() => toast.dismiss(props.toast.id)}
            >
                <XMarkIcon/>
            </button>
            <h4 className="text-md text-center font-medium">{props.problemDetails.title}</h4>
            <div className="text-sm text-left">
                <p>{props.problemDetails.detail}</p>
                {errorList && (
                    <ul>{errorList}</ul>
                )}
            </div>
        </div>
    )
}

export default ProblemDetailsToast;
