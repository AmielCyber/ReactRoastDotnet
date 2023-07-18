import type {Dispatch, ReactNode, SetStateAction} from "react";
import {createContext, useState} from "react";

type CartContextType = {
    showCart: boolean;
    setShowCart: Dispatch<SetStateAction<boolean>>;
};

const CartModalContext = createContext<CartContextType | null>(null);
const defaultShowCart = false;

type Props = {
    children: ReactNode;
};

function CartModalProvider(props: Props) {
    const [showCart, setShowCart] = useState(defaultShowCart);

    return <CartModalContext.Provider value={{showCart, setShowCart}}>{props.children}</CartModalContext.Provider>;
}

export {CartModalContext, CartModalProvider}
export type {CartContextType}
