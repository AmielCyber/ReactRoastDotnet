import {forwardRef, Ref} from "react";
import {Dialog} from "@headlessui/react";
// My imports.
import {useAppDispatch, useAppSelector} from "../store/store.ts";
import {clearCart} from "../store/cart-slice.ts";
import CartSummary from "./CartSummary.tsx";


type Props = {
    onClose: VoidFunction;
}

function Content(props: Props, ref: Ref<HTMLButtonElement>) {
    const {cart} = useAppSelector((state) => state.cart);
    const hasItems = cart.items.length > 0;
    const dispatch = useAppDispatch();

    const handleClearCart = () => {
        dispatch(clearCart());
    }

    if (!hasItems) {
        return (
            <div className="text-base-content">
                <Dialog.Title className="text-center font-bold text-lg">Your Cart</Dialog.Title>
                <p className="text-center">Your Cart is empty</p>
                <div className="modal-action">
                    <button
                        id="closeButton"
                        key="closeButton"
                        ref={ref}
                        className="btn btn-sm btn-primary"
                        onClick={props.onClose}
                    >
                        Close
                    </button>
                </div>
            </div>
        )
    }

    return (
        <>
            <Dialog.Title className="text-center font-bold text-lg">Your Cart</Dialog.Title>
            <CartSummary cart={cart}/>
            <div className="modal-action flex-col md:flex-row justify-center md:justify-end items-stretch md:items-baseline gap-2 ">
                <button
                    id="clearCart"
                    className="btn btn-sm btn-error ml-2 md:ml-0"
                    onClick={handleClearCart}
                >
                    Clear Cart
                </button>
                <button
                    id="closeButton"
                    key="closeButton"
                    className="btn btn-sm btn-primary"
                    onClick={props.onClose}
                >
                    Close
                </button>
                <button
                    id="checkoutButton"
                    key="checkoutButton"
                    ref={ref}
                    className="btn btn-sm btn-success text-success-content"
                    onClick={props.onClose}
                >
                    Checkout
                </button>
            </div>
        </>
    );
}

const CartContent = forwardRef<HTMLButtonElement, Props>(Content);
export default CartContent;
