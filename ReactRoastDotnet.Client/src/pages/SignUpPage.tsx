import {useForm} from "react-hook-form";
// My imports.
import type RegisterUser from "../models/RegisterUser.ts";
import {emailOptions, nameOptions, passwordOptions} from "../auth/inputOptions.ts";
import AuthFormHeader from "../auth/AuthFormHeader.tsx";
import AuthInput from "../auth/AuthInput.tsx";
import {Link} from "react-router-dom";

function SignUpPage() {
    const {
        register,
        handleSubmit,
        formState: {
            isSubmitting,
            errors,
        }
    } = useForm<RegisterUser>()

    // TODO: CALL API
    const submitForm = async (data: RegisterUser) => {
        await new Promise((r) => setTimeout(r, 3000));
        console.log(data)
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
