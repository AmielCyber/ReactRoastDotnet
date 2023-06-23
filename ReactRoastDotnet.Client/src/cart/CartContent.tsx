import {useCallback, useState} from "react";
// My imports.
import Cart from "../models/Cart.ts";
import EmptyCart from "./EmtpyCart";

type Props = {
    onClose: VoidFunction;
    cart: Cart;
};

function CartContent(props: Props) {
    const [isCheckout, setIsCheckout] = useState(false);

    const orderHandler = useCallback(() => {
        setIsCheckout(true);
    }, []);
    const cancelHandler = useCallback(() => {
        setIsCheckout(false);
    }, []);
    const clearCartHandler = useCallback(() => {
        console.log("Cleared")
    }, []);

    const totalPriceFormatted = `$${props.cart.totalPrice.toFixed(2)}`;
    const hasItems = props.cart.totalItems > 0;

    if (!hasItems) {
        return <EmptyCart onClose={props.onClose}/>;
    }
    return <p>This is not ready yet and this is not suppose to show...</p>;
}

export default CartContent;