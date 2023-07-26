import {useEffect, useState} from "react";
import {NavLink} from "react-router-dom";
// My imports.
import type CartItem from "../models/CartItem.ts";
import {path} from "../routes.tsx";
import useCartStore from "../store/cartStore.ts";
import CartIcon from "../icons/CartIcon.tsx";

const cartLinkClasses = "hover:bg-base-300 text-primary hover:text-accent";
const topCartButtonClasses = "btn btn-circle btn-ghost badge badge-sm badge-secondary" + cartLinkClasses;
const activeClass = "active text-secondary ";

const inactiveIndicatorClasses = "indicator-item badge badge-sm badge-secondary text-secondary-content";
const activeIndicatorClasses = inactiveIndicatorClasses + " animate-ping";

type Props = {
    isTopNav: boolean;
}

function GetCartLinkComponent(buttonClasses: string, indicatorClasses: string, numOfItems: number) {
    return (
        <NavLink
            end
            to={path.checkout}
            className={({isActive}) => isActive ? activeClass + buttonClasses : buttonClasses}
            key="cartLink"
        >
            <div className="indicator">
                <CartIcon/>
                <span className={indicatorClasses}>
                            {numOfItems}
                        </span>
            </div>
        </NavLink>
    )
}

function CartLink(props: Props) {
    const [animateIndicator, setAnimateIndicator] = useState(true);
    const cartItems = useCartStore(state => state.items);

    useEffect(() => {
        if (cartItems.length === 0) {
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
    }, [cartItems])

    const numOfItems = cartItems.reduce((count: number, currItem: CartItem) => currItem.quantity + count, 0);

    const buttonClasses = props.isTopNav ? topCartButtonClasses : cartLinkClasses;
    const indicatorClasses = animateIndicator ? activeIndicatorClasses : inactiveIndicatorClasses;

    if (props.isTopNav) {
        return (
            <li>
                {GetCartLinkComponent(buttonClasses, indicatorClasses, numOfItems)}
            </li>
        )
    }
    // Handlers.
    return GetCartLinkComponent(buttonClasses, indicatorClasses, numOfItems);
}

export default CartLink;
