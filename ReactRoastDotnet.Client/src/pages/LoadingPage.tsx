type Props = {
    pageName: string;
}

function LoadingPage(props: Props) {
    return (
        <main className="hero min-h-full px-6 py-24 sm:py-32 lg:px-8 mb-20 md:mb-0">
            <div className="hero-content mt-8 text-center">
                <div className="max-w-md text-gray-900 dark:text-white">
                    <h1 className="mb-4 text-3xl font-extrabold md:text-5xl lg:text-6xl">
                        <span className="text-transparent bg-clip-text bg-gradient-to-r from-secondary to-primary">
                            Loading
                        </span>{" "}
                    </h1>
                    <p className="py-4 text-xl">{props.pageName} page...</p>
                        <progress className="progress w-56 progress-secondary"></progress>
                </div>
            </div>
        </main>
    );
}

export default LoadingPage;
