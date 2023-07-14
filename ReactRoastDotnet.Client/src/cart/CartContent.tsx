import {forwardRef, Ref} from "react";
// My imports.
import Cart from "../models/Cart.ts";
import CartItem from "../models/CartItem.ts";

const cartDemo: Cart = {
    items: [{id: 1, price: 5, type: "Drink", name: "coffee", quantity: 2}],
    lastModified: new Date(Date.now())
}

type Props = {
    onClose: VoidFunction;
}

function CartContent(props: Props, ref: Ref<HTMLButtonElement>) {


    const totalPriceFormatted = `$${cartDemo
        .items 
        .reduce((total: number, curr: CartItem) => (total + curr.price) * curr.quantity, 0)
        .toFixed(2)
    }`;

    const hasItems = cartDemo.items.length > 0;

    if(!hasItems){
        return(
            <>
                <p>Cart is empty</p>
                <div className="modal-action">
                    <button
                        id="closeButton"
                        key="closeButton"
                        ref={ref}
                        className="btn btn-sm btn-error"
                        onClick={props.onClose}
                    >
                        Close
                    </button>
                </div>
            </>
        )
    }

        return(
            <>
            <p>Cart stuff</p>
                <p>Total {totalPriceFormatted}</p>
            <div className="modal-action">
                <button
                    id="closeButton"
                    key="closeButton"
                    className="btn btn-sm btn-error"
                    onClick={props.onClose}
                >
                    Close
                </button>
                <button
                    id="checkoutButton"
                    key="checkoutButton"
                    ref={ref}
                    className="btn btn-sm btn-success"
                    onClick={props.onClose}
                >
                    Checkout
                </button>
            </div>
            </>
        );
}

export default forwardRef<HTMLButtonElement, Props>(CartContent);
