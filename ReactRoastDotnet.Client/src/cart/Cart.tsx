// My imports
import type CartItem from "../models/CartItem.ts";
import EmptyCart from "./EmptyCart.tsx";
import CartList from "./CartList.tsx";
import CartTotal from "./CartTotal.tsx";

type Props = {
    cartItems: CartItem[]
    cartIsEmpty: boolean;
}

function Cart(props: Props) {

    if (props.cartIsEmpty) {
        return <EmptyCart/>
    }

    return (
        <div className="flex flex-col w-full items-stretch">
            <section className="px-2 mx-2">
                <h2 className="text-center text-xl mt-6 font-bold">Cart</h2>
                <CartList cartItems={props.cartItems}/>
            </section>
            <section className="px-2 mx-2 self-start">
                <h2 className="card-title text-xl my-4">Cart Summary</h2>
                <CartTotal cartItems={props.cartItems}/>
            </section>
        </div>
    );
}

export default Cart;
