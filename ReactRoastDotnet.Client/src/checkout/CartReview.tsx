// My imports.
import Cart from "../cart/Cart.tsx";
import CheckoutActions from "./CheckoutActions.tsx";
import checkoutStep from "./checkoutHelper.ts";
import useCartStore from "../store/cartStore.ts";

type Props = {
    onNext: VoidFunction;
    onPrev: VoidFunction;
}

function CartReview(props: Props) {
    const clearCart = useCartStore(state => state.clearCart);
    return (
        <>
            <Cart/>
            <CheckoutActions stepNum={checkoutStep.reviewCart} onClearCart={clearCart} onNext={props.onNext} onBack={props.onPrev}/>
        </>
    );
}

export default CartReview;
