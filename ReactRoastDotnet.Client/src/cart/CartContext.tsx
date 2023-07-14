import type {Dispatch, ReactNode, SetStateAction} from "react";
import { createContext, useState } from "react";

export type CartContextType = {
    showCart: boolean;
    setShowCart: Dispatch<SetStateAction<boolean>>;
};

export const CartContext = createContext<CartContextType | null>(null);
const defaultShowCart= false;

type Props = {
    children: ReactNode;
};

export function CartProvider(props: Props) {
    const [showCart, setShowCart] = useState(defaultShowCart);

    return <CartContext.Provider value={{ showCart, setShowCart }}>{props.children}</CartContext.Provider>;
}
