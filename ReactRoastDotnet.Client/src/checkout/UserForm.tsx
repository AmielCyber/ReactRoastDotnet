import {useForm} from "react-hook-form";
import {Link} from "react-router-dom";
// My imports
import type RegisterUser from "../models/RegisterUser.ts";
import {useAppDispatch} from "../store/store.ts";
import {clearCart} from "../store/cart-slice.ts";
import {emailOptions, nameOptions} from "../auth/inputOptions.ts";
import AuthInput from "../auth/AuthInput.tsx";
import CheckoutActions from "./CheckoutActions.tsx";
import checkoutStep from "./checkoutHelper.ts";

type Props = {
    onNext: VoidFunction;
    onPrev: VoidFunction;
}

function UserForm(props: Props) {
    const dispatch = useAppDispatch();
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
        dispatch(clearCart());
        props.onNext();
    }

    const firstName = register("firstName", nameOptions);
    const lastName = register("lastName", nameOptions);
    const email = register("email", emailOptions);

    return (
        // eslint-disable-next-line @typescript-eslint/no-misused-promises
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
            <Link to={"/auth/sign-in"} className="link-secondary">Already registered? Sign In</Link>
            <div className="form-control mt-6">
                <CheckoutActions stepNum={checkoutStep.submitOrder} isSubmitting={isSubmitting} onBack={props.onPrev}/>
            </div>
        </form>
    );
}

export default UserForm;
