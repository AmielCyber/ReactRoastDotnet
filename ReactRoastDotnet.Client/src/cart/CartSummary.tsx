// My imports.
import type Cart from "../models/Cart.ts";
import type CartItem from "../models/CartItem.ts";
import CartList from "./CartList.tsx";
import CartSummaryTotal from "./CartSummaryTotal.tsx";

type Props = {
    cart: Cart;
}

function CartSummary(props: Props) {
    let totalItems = 0;

    const totalPriceFormatted = `$${props.cart
        .items
        .reduce((total: number, curr: CartItem) => {
            totalItems += curr.quantity;
            return (curr.price * curr.quantity) + total
        }, 0)
        .toFixed(2)
    }`;

    return (
        <section>
            <CartList cart={props.cart}/>
            <CartSummaryTotal totalItems={totalItems} totalPrice={totalPriceFormatted}/>
        </section>
    )

}

export default CartSummary;
