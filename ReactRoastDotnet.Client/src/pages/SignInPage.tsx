import CoffeeIcon from "../icons/CoffeeIcon.tsx";

function SignInPage() {
    return (
        <main className="flex justify-center min-h-full py-24 md:py-24 mb-20 md:mb-0">
            <div className="card w-full max-w-md shadow-2xl bg-base-100">
                <div className="flex flex-col justify-center items-center text-center lg:text-left">
                    <div className="mt-10 text-primary">
                        <CoffeeIcon/>
                    </div>
                    <h1 className="mt-2 text-center text-2xl font-bold leading-9">
                        Sign in to your account
                    </h1>
                </div>
                <div className="card-body">
                    <div className="form-control">
                        <label className="label">
                            <span className="label-text">Email</span>
                        </label>
                        <input type="text" placeholder="email" className="input input-bordered focus:input-primary"/>
                    </div>
                    <div className="form-control">
                        <label className="label">
                            <span className="label-text">Password</span>
                        </label>
                        <input type="text" placeholder="password" className="input input-bordered focus:input-primary"/>
                    </div>
                    <div className="form-control mt-6">
                        <button className="btn btn-primary">Login</button>
                    </div>
                </div>
            </div>
        </main>
    );
}

export default SignInPage;
