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
    const cartItems = useCartStore(state => state.items);
    const clearCart = useCartStore(state => state.clearCart);

    const cartIsEmpty = cartItems.length < 1;

    return (
        <>
            <Cart cartItems={cartItems} cartIsEmpty={cartIsEmpty}/>
            <CheckoutActions
                stepNum={checkoutStep.reviewCart}
                onClearCart={clearCart}
                onNext={props.onNext}
                onBack={props.onPrev}
                disableOnNext={cartIsEmpty}
            />
        </>
    );
}

export default CartReview;
