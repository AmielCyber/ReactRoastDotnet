import agent from "../api/agent.ts";

function ErrorPage() {
    return (
        <div>
            <div className="alert">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                     className="stroke-info shrink-0 w-6 h-6">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                          d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
                <span>we use cookies for no reason.</span>
                <div>
                    <button className="btn btn-sm">Deny</button>
                    <button className="btn btn-sm btn-primary">Accept</button>
                </div>
            </div>
            <h2>Errors for testing.</h2>
            <button className="btn btn-error" onClick={() => void agent.TestErrors.get400Error()}>Bad Request</button>
            <button className="btn btn-error" onClick={() => void agent.TestErrors.get401Error()}>Unauthorized</button>
            <button className="btn btn-error" onClick={() => void agent.TestErrors.getValidationError()}>Validation
                Error
            </button>
            <button className="btn btn-error" onClick={() => void agent.TestErrors.get404Error()}>Not Found</button>
            <button className="btn btn-error" onClick={() => void agent.TestErrors.get500Error()}>Server Error</button>
        </div>
    )
}

export default ErrorPage;
