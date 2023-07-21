import {removeCartItem, addCartItem, removeAllCartItemsWithId} from "../../store/cart-slice.ts";
import type CartItem from "../../models/CartItem.ts";
import {useAppDispatch} from "../../store/store.ts";
import TrashIcon from "./TrashIcon.tsx";
import PlusIcon from "../icons/PlusIcon.tsx";
import MinusIcon from "../icons/MinusIcon.tsx";

type Props = {
    item: CartItem;
}
function CartItem(props: Props){
    const totalCost = props.item.price.toLocaleString("en-US", {style: "currency", currency: "USD"});
    const dispatch = useAppDispatch();

    return(
        <li className="flex flex-col">
            <div className="flex flex-row w-full items-center justify-between">
                <section className="card-body">
                    <h2 className="card-title">
                        {props.item.name}
                        {props.item.quantity > 1 &&  <div className="badge badge-secondary">x{props.item.quantity}</div>}
                    </h2>
                    <strong className="text-emerald-500">{totalCost}</strong>
                </section>
                <section className="flex flex-col sm:flex-row items-center justify-center gap-2 mr-2">
                    <button className="btn btn-sm" onClick={() => dispatch(removeAllCartItemsWithId(props.item.id))}>
                        <TrashIcon />
                    </button>
                    <button className="btn btn-sm" onClick={() => dispatch(addCartItem({cartItem: props.item}))}>
                        <PlusIcon />
                    </button>
                    <button className="btn btn-sm" onClick={() => dispatch(removeCartItem({itemId: props.item.id, quantity: 1}))}>
                        <MinusIcon />
                    </button>
                </section>
            </div>
            <div  className="divider h-0" />
        </li>
    );
}
export default CartItem;
