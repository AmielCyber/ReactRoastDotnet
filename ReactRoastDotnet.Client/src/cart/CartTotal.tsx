// My imports
import type CartItem from "../models/CartItem.ts";

type Props = {
    cartItems: CartItem[]
}

function CartTotal(props: Props) {
    let totalItems = 0;

    const totalPriceFormatted = `$${props.cartItems
        .reduce((total: number, curr: CartItem) => {
            totalItems += curr.quantity;
            return (curr.price * curr.quantity) + total
        }, 0)
        .toFixed(2)
    }`;

    return (
        <div className="flex flex-col gap-2 text-md sm:text-lg font-bold">
            <div className="flex items-center gap-10">
                <h3 className="">Total Items:</h3>
                <p className="text-secondary">{totalItems}</p>
            </div>
            <div className="flex items-center gap-10">
                <h3 className="">Total Price:</h3>
                <p className="text-emerald-500">{totalPriceFormatted}</p>
            </div>
        </div>
    )
}

export default CartTotal;
