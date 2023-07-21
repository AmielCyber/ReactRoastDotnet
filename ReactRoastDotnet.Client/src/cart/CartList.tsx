// My imports.
import type Cart from "../models/Cart.ts";
import CartItem from "./CartItem.tsx";

type Props = {
    cart: Cart
}

function CartList(props: Props) {
    return <ul className="overflow-scroll max-h-72">
        {props.cart.items.map(item =>
            <CartItem item={item} key={item.id}/>)
        }
    </ul>
}

export default CartList;
