import {Link} from "react-router-dom";
import {path} from "../routes.tsx";

function NotFoundPage() {
    return (
        <main className="hero min-h-full px-6 py-24 sm:py-32 lg:px-8 mb-20 md:mb-0">
            <div className="hero-content text-center">
                <div className="max-w-md">
                    <h1 className="text-5xl font-bold">
                            <span className="text-transparent bg-clip-text bg-gradient-to-r from-secondary to-primary">
                            404
                            </span>
                    </h1>
                    <h2 className="text-3xl font-bold sm:text-5xl">
                        Page not found
                    </h2>
                    <p className="py-6">Sorry, we could not find the page you were looking for.</p>
                    <div className="mt-2 flex items-center justify-center gap-x-6">
                        <Link to={path.home} className="btn btn-primary">
                            Go back home
                        </Link>
                    </div>

                </div>
            </div>
        </main>
    )
}

export default NotFoundPage;
