// My imports
import {useAppSelector} from "../store/store.ts";
import EmptyCart from "./EmptyCart.tsx";
import CartList from "./CartList.tsx";
import CartTotal from "./CartTotal.tsx";

function Cart() {
    const {cart} = useAppSelector(state => state.cart);

    if (cart.items.length < 1) {
        return <EmptyCart/>
    }

    return (
        <div className="flex flex-col w-full items-stretch">
            <section className="px-2 mx-2">
                <h2 className="text-center text-xl mt-6 font-bold">Cart</h2>
                <CartList cart={cart}/>
            </section>
            <section className="px-2 mx-2 self-start">
                <h2 className="card-title text-xl my-4">Cart Summary</h2>
                <CartTotal cart={cart}/>
            </section>
        </div>
    );
}

export default Cart;
