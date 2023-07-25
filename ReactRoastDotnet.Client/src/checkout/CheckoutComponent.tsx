// My imports.
import checkoutStep from "./checkoutHelper.ts";
import UserForm from "./UserForm.tsx";
import Receipt from "./Receipt.tsx";
import CartReview from "./CartReview.tsx";

type Props = {
    stepNum: number;
    onPrev: VoidFunction;
    onNext: VoidFunction;
}

function CheckoutComponent(props: Props) {
    if (props.stepNum === checkoutStep.submitOrder) {
        return <UserForm onPrev={props.onPrev} onNext={props.onNext}/>
    }
    if (props.stepNum === checkoutStep.receipt) {
        return <Receipt/>
    }
    return <CartReview onNext={props.onNext} onPrev={props.onPrev}/>
}

export default CheckoutComponent;
