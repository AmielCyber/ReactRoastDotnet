// My imports.
import CartIcon from "../icons/CartIcon.tsx";
import Cart from "../../models/Cart.ts";
import {useContext} from "react";
import {CartContext, CartContextType} from "./CartContext.tsx";

const cartButtonClasses = " hover:bg-base-300 text-primary hover:text-accent";
const topCartButtonClasses = "btn btn-circle btn-ghost badge badge-sm badge-secondary" + cartButtonClasses;

const cartDemo: Cart = {
    items: [],
    lastModified: new Date(Date.now())
}

type Props = {
    isTopNav: boolean;
}


function CartButton(props: Props) {
    const {setShowCart} = useContext(CartContext) as CartContextType;

    // Handlers.
    const showCartHandler = () => {
        setShowCart(true);
    };

    return (
        <button className={props.isTopNav? topCartButtonClasses : cartButtonClasses} onClick={showCartHandler}>
            <div className="indicator">
                <CartIcon/>
                <span className="indicator-item badge badge-sm badge-secondary text-secondary-content">
                    {cartDemo.items.length}
                </span>
            </div>
        </button>
    );
}

export default CartButton;
