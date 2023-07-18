import {useContext} from "react";
// My imports.
import CartIcon from "../icons/CartIcon.tsx";
import {CartModalContext, CartContextType} from "../../hooks/CartModalContext.tsx";
import {useAppSelector} from "../../store/store";

const cartButtonClasses = " hover:bg-base-300 text-primary hover:text-accent";
const topCartButtonClasses = "btn btn-circle btn-ghost badge badge-sm badge-secondary" + cartButtonClasses;

type Props = {
    isTopNav: boolean;
}


function CartButton(props: Props) {
    const {setShowCart} = useContext(CartModalContext) as CartContextType;
    const { cart} = useAppSelector((state) => state.cart);

    // Handlers.
    const showCartHandler = () => {
        setShowCart(true);
    };

    return (
        <button className={props.isTopNav? topCartButtonClasses : cartButtonClasses} onClick={showCartHandler}>
            <div className="indicator">
                <CartIcon/>
                <span className="indicator-item badge badge-sm badge-secondary text-secondary-content">
                    {cart.items.length}
                </span>
            </div>
        </button>
    );
}

export default CartButton;
