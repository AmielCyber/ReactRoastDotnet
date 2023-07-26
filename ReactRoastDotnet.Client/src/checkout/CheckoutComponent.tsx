// My imports.
import checkoutStep from "./checkoutHelper.ts";
import OrderConfirmation from "./OrderConfirmation.tsx";
import CartReview from "./CartReview.tsx";
import SubmitOrder from "./SubmitOrder.tsx";

type Props = {
    stepNum: number;
    onPrev: VoidFunction;
    onNext: VoidFunction;
}

function CheckoutComponent(props: Props) {
    if (props.stepNum === checkoutStep.submitOrder) {
        return <SubmitOrder onPrev={props.onPrev} onNext={props.onNext}/>
    }
    if (props.stepNum === checkoutStep.receipt) {
        return <OrderConfirmation/>
    }
    return <CartReview onNext={props.onNext} onPrev={props.onPrev}/>
}

export default CheckoutComponent;
