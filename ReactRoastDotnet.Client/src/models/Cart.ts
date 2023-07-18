// My import.
import CartItem from "./CartItem.ts";

interface Cart {
    items: CartItem[];
    lastModified: Date;
}

export default Cart;
