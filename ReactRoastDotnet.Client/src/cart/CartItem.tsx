import type CartItem from "../models/CartItem.ts";
import useCartStore from "../store/cartStore.ts";
import TrashIcon from "../icons/TrashIcon.tsx";
import PlusIcon from "../icons/PlusIcon.tsx";
import MinusIcon from "../icons/MinusIcon.tsx";

const buttonClasses = "btn btn-sm text-primary hover:text-accent tooltip tooltip-right tooltip-accent";
const toolTipClasses ="tooltip tooltip-left sm:tooltip-bottom tooltip-accent";

type Props = {
    item: CartItem;
}

function CartItem(props: Props) {
    const addCartItem = useCartStore(state => state.addCartItem);
    const removeCartItem = useCartStore(state => state.removeCartItem);
    const removeAllCartItemsWithId = useCartStore(state => state.removeAllCartItemsWithId);

    const totalCost = props.item.price.toLocaleString("en-US", {style: "currency", currency: "USD"});

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
                    <li className={toolTipClasses} data-tip="Remove All">
                        <button className={buttonClasses}
                                onClick={() => removeAllCartItemsWithId(props.item.id)} aria-label="Remove All">
                            <TrashIcon/>
                        </button>
                    </li>
                    <li className={toolTipClasses} data-tip="Add">
                        <button className={buttonClasses}
                                onClick={() => addCartItem(props.item, 1)} aria-label="Add">
                            <PlusIcon/>
                        </button>
                    </li>
                    <li className={toolTipClasses} data-tip="Subtract">
                        <button className={buttonClasses}
                                onClick={() => removeCartItem(props.item.id, 1)} aria-label="Subtract">
                            <MinusIcon/>
                        </button>
                    </li>
                </menu>
            </section>
        </li>
    );
}

export default CartItem;
