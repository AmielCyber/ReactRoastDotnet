import getRequest from "../api/getRequest.ts";
import toast from "react-hot-toast";

function ErrorPage() {
    const prom = new Promise(resolve => setTimeout(resolve, 5000))

    return (
        <div>
            <h2>Errors for testing.</h2>
            <button className="btn btn-error" onClick={() => void toast.promise(prom,{
                loading: "loading",
                success: "success",
                error: "LOL"
            }) }>Bad Request</button>
            <button className="btn btn-error" onClick={() => void getRequest("errors/unauthorized")}>Unauthorized</button>
            <button className="btn btn-error" onClick={() => void getRequest("errors/validation-error")}>Validation
                Error
            </button>
            <button className="btn btn-error" onClick={() => void getRequest("errors/not-found")}>Not Found</button>
            <button className="btn btn-error" onClick={() => void getRequest("errors/server-error")}>Server Error</button>
        </div>
    )
}

export default ErrorPage;
