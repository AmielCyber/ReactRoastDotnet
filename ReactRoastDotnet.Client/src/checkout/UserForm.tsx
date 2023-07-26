import {useState} from "react";
import {useForm} from "react-hook-form";
// My imports
import type UserSignUpRequest from "../models/UserSignUpRequest.ts";
import type AuthUser from "../models/AuthUser.ts";
import useCartStore from "../store/cartStore.ts";
import {emailOptions, nameOptions} from "../auth/inputOptions.ts";
import AuthInput from "../auth/AuthInput.tsx";
import CheckoutActions from "./CheckoutActions.tsx";
import checkoutStep from "./checkoutHelper.ts";

type Props = {
    onNext: VoidFunction;
    onPrev: VoidFunction;
    user?: AuthUser;
}

function UserForm(props: Props) {
    const [isNotChecked, setIsNotChecked] = useState(true);
    const {
        register,
        handleSubmit,
        formState: {
            isSubmitting,
            errors,
        }
    } = useForm<UserSignUpRequest>({
        defaultValues: {
            firstName: props.user?.firstName,
            lastName: props.user?.lastName,
            email: props.user?.email,
        }
    })
    const clearCart = useCartStore(state => state.clearCart);


    const onChecked = () => setIsNotChecked(!isNotChecked);

    // TODO: CALL ORDER API
    const submitForm = async (data: UserSignUpRequest) => {
        await new Promise((r) => setTimeout(r, 3000));
        console.log(data)
        clearCart();
        props.onNext();
    }


    const firstName = register("firstName", nameOptions);
    const lastName = register("lastName", nameOptions);
    const email = register("email", emailOptions);

    const isGuest = props.user === undefined;

    return (
        <section>
            <h2 className="text-center text-xl mt-4 font-bold">{isGuest && "Enter "}Order Details</h2>
            {/*eslint-disable-next-line @typescript-eslint/no-misused-promises*/}
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
                    value={props.user?.firstName}
                    disabled={!!props.user}
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
                    value={props.user?.lastName}
                    disabled={!!props.user}
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
                    value={props.user?.email}
                    disabled={!!props.user}
                />
                <label className="label cursor-pointer">
                    <span className="label-text">I understand this is a demo application; therefore, no actual order will be placed.</span>
                    <input type="checkbox" className="checkbox checkbox-primary" onClick={onChecked}/>
                </label>
                <div className="form-control mt-6">
                    <CheckoutActions stepNum={checkoutStep.submitOrder} isSubmitting={isSubmitting}
                                     onBack={props.onPrev} disableOnNext={isNotChecked}/>
                </div>
            </form>

        </section>
    );
}

export default UserForm;
