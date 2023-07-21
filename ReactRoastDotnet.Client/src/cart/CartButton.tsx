import {useContext, useEffect, useState} from "react";
// My imports.
import type CartItem from "../models/CartItem.ts";
import {CartContextType, CartModalContext} from "../hooks/CartModalContext.tsx";
import {useAppSelector} from "../store/store.ts";
import CartIcon from "../icons/CartIcon.tsx";

const cartButtonClasses = "hover:bg-base-300 text-primary hover:text-accent";
const topCartButtonClasses = "btn btn-circle btn-ghost badge badge-sm badge-secondary " + cartButtonClasses;

const inactiveIndicatorClasses = "indicator-item badge badge-sm badge-secondary text-secondary-content";
const activeIndicatorClasses = inactiveIndicatorClasses + " animate-ping";

type Props = {
    isTopNav: boolean;
}

function CartButton(props: Props) {
    const [animateIndicator, setAnimateIndicator] = useState(true);
    const {setShowCart} = useContext(CartModalContext) as CartContextType;
    const {cart} = useAppSelector((state) => state.cart);

    useEffect(() => {
        if (cart.items.length === 0) {
            if (animateIndicator) {
                setAnimateIndicator(false);
            }
            return;
        }
        setAnimateIndicator(true);
        const timer = setTimeout(() => {
            setAnimateIndicator(false);
        }, 400);

        return () => {
            clearTimeout(timer);
        }
    }, [cart])


    const numOfItems = cart.items.reduce((count: number, currItem: CartItem) => currItem.quantity + count, 0);
    const indicatorClasses = animateIndicator ? activeIndicatorClasses : inactiveIndicatorClasses;

    // Handlers.
    const showCartHandler = () => {
        setShowCart(true);
    };

    return (
        <button className={props.isTopNav ? topCartButtonClasses : cartButtonClasses} onClick={showCartHandler}>
            <div className="indicator">
                <CartIcon/>
                <span className={indicatorClasses}>
                    {numOfItems}
                </span>
            </div>
        </button>
    );
}

export default CartButton;
