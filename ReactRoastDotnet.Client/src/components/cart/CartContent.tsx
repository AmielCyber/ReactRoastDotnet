import {forwardRef, Ref} from "react";
// My imports.
import type Cart from "../../models/Cart.ts";
import {Dialog} from "@headlessui/react";
import CartSummary from "./CartSummary.tsx";

const cartDemo: Cart = {
    items: [
        {id: 1, price: 5, type: "Drink", name: "Coffee", quantity: 2},
        {id: 2, price: 4, type: "Drink", name: "Latte", quantity: 1},
        {id: 3, price: 4.75, type: "Drink", name: "Cold Brew", quantity: 3},
        {id: 4, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
        {id: 5, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
        {id: 6, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
    ],
    // items: [],
    lastModified: new Date(Date.now())
}

type Props = {
    onClose: VoidFunction;
}

function CartContent(props: Props, ref: Ref<HTMLButtonElement>) {
    const hasItems = cartDemo.items.length > 0;

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
                        className="btn btn-sm btn-error text-error-content"
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
            <CartSummary cart={cartDemo} />
            <div className="modal-action">
                <button
                    id="closeButton"
                    key="closeButton"
                    className="btn btn-sm btn-error text-error-content"
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

export default forwardRef<HTMLButtonElement, Props>(CartContent);
