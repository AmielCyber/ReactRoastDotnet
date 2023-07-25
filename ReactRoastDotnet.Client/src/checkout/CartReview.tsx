// My imports.
import Cart from "../cart/Cart.tsx";
import CheckoutActions from "./CheckoutActions.tsx";
import checkoutStep from "./checkoutHelper.ts";

type Props = {
    onNext: VoidFunction;
    onPrev: VoidFunction;
}

function CartReview(props: Props) {
    return (
        <>
            <Cart/>
            <CheckoutActions stepNum={checkoutStep.reviewCart} onNext={props.onNext} onBack={props.onPrev}/>
        </>
    );
}

export default CartReview;
