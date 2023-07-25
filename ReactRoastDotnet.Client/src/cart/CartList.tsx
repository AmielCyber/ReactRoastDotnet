// My imports.
import CartItem from "./CartItem.tsx";

type Props = {
    cartItems: CartItem[]
}

function CartList(props: Props) {
    return (
        <ul>
            {props.cartItems.map(item => <CartItem item={item} key={item.id}/>)}
        </ul>
    );
}

export default CartList;
