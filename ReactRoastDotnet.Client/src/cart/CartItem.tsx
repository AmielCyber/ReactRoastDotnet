import type CartItem from "../models/CartItem.ts";
import {useAppDispatch} from "../store/store.ts";
import {addCartItem, removeAllCartItemsWithId, removeCartItem} from "../store/cart-slice.ts";
import TrashIcon from "../icons/TrashIcon.tsx";
import PlusIcon from "../icons/PlusIcon.tsx";
import MinusIcon from "../icons/MinusIcon.tsx";

const buttonClasses = "btn btn-sm text-primary hover:text-accent";

type Props = {
    item: CartItem;
}

function CartItem(props: Props) {
    const totalCost = props.item.price.toLocaleString("en-US", {style: "currency", currency: "USD"});
    const dispatch = useAppDispatch();

    return (
        <li className="flex flex-col border-b border-accent py-2">
            <section className="flex w-full items-center justify-between">
                <div className="sm:card-body">
                    <h2 className="card-title">
                        {props.item.name}
                        {props.item.quantity > 1 && <div className="badge badge-secondary">x{props.item.quantity}</div>}
                    </h2>
                    <p>
                        <strong className="text-emerald-500">{totalCost}</strong>
                    </p>
                </div>
                <menu className="flex flex-col sm:flex-row gap-2 sm:px-2">
                    <li>
                        <button className={buttonClasses}
                                onClick={() => dispatch(removeAllCartItemsWithId(props.item.id))}>
                            <TrashIcon/>
                        </button>
                    </li>
                    <li>
                        <button className={buttonClasses} onClick={() => dispatch(addCartItem({cartItem: props.item}))}>
                            <PlusIcon/>
                        </button>
                    </li>
                    <li>
                        <button className={buttonClasses}
                                onClick={() => dispatch(removeCartItem({itemId: props.item.id, quantity: 1}))}>
                            <MinusIcon/>
                        </button>
                    </li>
                </menu>
            </section>
        </li>
    );
}

export default CartItem;
