import {useForm} from "react-hook-form";
// My imports.
import type LoginUser from "../models/LoginUser.ts";
import {emailOptions, passwordOptions} from "../auth/inputOptions.ts";
import AuthFormHeader from "../auth/AuthFormHeader.tsx";
import AuthInput from "../auth/AuthInput.tsx";
import {Link} from "react-router-dom";

function SignInPage() {
    const {
        register,
        handleSubmit,
        formState: {
            isSubmitting,
            errors,
        }
    } = useForm<LoginUser>()

    // TODO: CALL API
    const submitForm = async (data: LoginUser) => {
        await new Promise((r) => setTimeout(r, 3000));
        console.log(data)
    }

    const email = register("email", emailOptions);
    const password = register("password", passwordOptions);

    return (
        <main className="flex justify-center min-h-full py-6 md:py-24 mb-20 md:mb-0">
            <div className="card w-full max-w-md shadow-2xl bg-base-100">
                <AuthFormHeader title="Sign In"/>
                {/* eslint-disable-next-line @typescript-eslint/no-misused-promises */}
                <form className="card-body" onSubmit={handleSubmit(submitForm)}>
                    <AuthInput
                        type="email"
                        ref={email.ref}
                        name={email.name}
                        onChange={email.onChange}
                        onBlur={email.onBlur}
                        labelText="Email"
                        errorMsg={errors?.email?.message}
                        autoComplete="email"
                    />
                    <AuthInput
                        type="password"
                        ref={password.ref}
                        name={password.name}
                        onChange={password.onChange}
                        onBlur={password.onBlur}
                        labelText="Password"
                        errorMsg={errors?.password?.message}
                        autoComplete="current-password"
                    />
                    <Link to={"/auth/sign-up"} className="link-secondary">Not registered? Sign Up</Link>
                    <div className="form-control mt-6">
                        <button className="btn btn-primary" type="submit">
                            Login
                            {isSubmitting && <span className="loading loading-spinner"></span>}
                        </button>
                    </div>
                </form>
            </div>
        </main>
    );
}

export default SignInPage;
