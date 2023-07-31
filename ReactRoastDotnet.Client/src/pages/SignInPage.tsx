import {useForm} from "react-hook-form";
import {Link, Navigate, useLocation} from "react-router-dom";
// My imports.
import type LoginRequest from "../models/LoginRequest.ts";
import useUserStore from "../store/userStore.ts";
import {emailOptions, signInPasswordOptions} from "../auth/inputOptions.ts";
import AuthFormHeader from "../auth/AuthFormHeader.tsx";
import AuthInput from "../auth/AuthInput.tsx";
import {path} from "../routes.tsx";

function SignInPage() {
    const {
        register,
        handleSubmit,
        formState: {
            isSubmitting,
            errors,
        }
    } = useForm<LoginRequest>()
    const authUser = useUserStore(state => state.user);
    const signInUser = useUserStore(state => state.signInUser);
    const location = useLocation();
    // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
    const guestEmail = location.state?.email as string | undefined;

    if (authUser) {
        return <Navigate to={path.home}/>
    }

    const submitForm = async (data: LoginRequest) => {
        await signInUser({
            email: data.email,
            password: data.password,
        });
    }

    const email = register("email", emailOptions);
    const password = register("password", signInPasswordOptions);

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
                        value={guestEmail}
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
                    <Link to={path.signUp} className="link-secondary">Not registered? Sign Up</Link>
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
