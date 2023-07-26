import {useState} from "react";
import {useForm} from "react-hook-form";
import {Link, Navigate} from "react-router-dom";
// My imports.
import type UserSignUpRequest from "../models/UserSignUpRequest.ts";
import {emailOptions, nameOptions, passwordOptions} from "../auth/inputOptions.ts";
import AuthFormHeader from "../auth/AuthFormHeader.tsx";
import AuthInput from "../auth/AuthInput.tsx";
import {signUp} from "../store/userActions.ts";

function SignUpPage() {
    const [guestEmail, setGuestEmail] = useState<string>();
    const {
        register,
        handleSubmit,
        formState: {
            isSubmitting,
            errors,
        }
    } = useForm<UserSignUpRequest>()

    if (guestEmail) {
        return <Navigate to="/auth/sign-in" state={{email: guestEmail}}/>
    }

    const submitForm = async (data: UserSignUpRequest) => {
        const result = await signUp({
            firstName: data.firstName,
            lastName: data.lastName,
            email: data.email,
            password: data.password
        });
        if (result.ok) {
            setGuestEmail(result.value);
        }
    }
    const firstName = register("firstName", nameOptions);
    const lastName = register("lastName", nameOptions);
    const email = register("email", emailOptions);
    const password = register("password", passwordOptions);

    return (
        <main className="flex justify-center min-h-full py-6 md:py-6 mb-20 md:mb-0">
            <div className="card w-full max-w-md shadow-2xl bg-base-100">
                <AuthFormHeader title="Sign Up"/>
                {/* eslint-disable-next-line @typescript-eslint/no-misused-promises */}
                <form className="card-body" onSubmit={handleSubmit(submitForm)}>
                    <AuthInput
                        ref={firstName.ref}
                        type="text"
                        name={firstName.name}
                        onChange={firstName.onChange}
                        onBlur={firstName.onBlur}
                        labelText="First Name"
                        errorMsg={errors?.firstName?.message}
                        autoComplete="given-name"
                    />
                    <AuthInput
                        ref={lastName.ref}
                        type="text"
                        name={lastName.name}
                        onChange={lastName.onChange}
                        onBlur={lastName.onBlur}
                        labelText="Last Name"
                        errorMsg={errors?.lastName?.message}
                        autoComplete="family-name"
                    />
                    <AuthInput
                        ref={email.ref}
                        type="email"
                        name={email.name}
                        onChange={email.onChange}
                        onBlur={email.onBlur}
                        labelText="Email"
                        errorMsg={errors?.email?.message}
                        autoComplete="email"
                    />
                    <AuthInput
                        ref={password.ref}
                        type="password"
                        name={password.name}
                        onChange={password.onChange}
                        onBlur={password.onBlur}
                        labelText="Password"
                        errorMsg={errors?.password?.message}
                        autoComplete="new-password"
                    />
                    <Link to={"/auth/sign-in"} className="link-secondary">Already registered? Sign In</Link>
                    <div className="form-control mt-6">
                        <button className="btn btn-primary" type="submit">
                            Sign Up
                            {isSubmitting && <span className="loading loading-spinner"></span>}
                        </button>
                    </div>
                </form>
            </div>
        </main>
    );
}

export default SignUpPage;
