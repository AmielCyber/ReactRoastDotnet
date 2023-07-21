import ErrorIcon from "../icons/ErrorIcon.tsx";

function DisplayErrorAlert() {
    return (
        <section className="alert alert-error max-w-screen-sm mx-auto">
            <ErrorIcon/>
            <h2 className="text-error-content">Error! Failed to fetch products.</h2>
        </section>
    );
}

export default DisplayErrorAlert;
