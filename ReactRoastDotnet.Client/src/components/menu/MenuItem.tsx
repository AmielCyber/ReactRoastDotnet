// My imports.
import type ProductItem from "../../models/ProductItem.ts";
import type CartItem from "../../models/CartItem.ts"
import {useAppDispatch} from "../../store/store.ts";
import {addCartItem} from "../../store/cart-slice.ts";

type Props = {
    productItem: ProductItem
}

function getCartItem(productItem: ProductItem): CartItem {
    return {
        id: productItem.id,
        type: productItem.type,
        name: productItem.name,
        price: productItem.price,
        quantity: 1,
    };
}

function MenuItem(props: Props) {
    const price = props.productItem.price.toLocaleString("en-US", {style: "currency", currency: "USD"});
    const dispatch = useAppDispatch();

    return (
        <div className="card sm:card-side bg-base-100 shadow-xl pt-2">
            <cite
                className="w-48 flex-none self-center tooltip tooltip-bottom tooltip-accent"
                data-tip={"Image by " + props.productItem.imageCreator + " at Unsplash"}
            >
                <figure className="justify-center self-center">
                    <img src={props.productItem.image} alt={props.productItem.name}/>
                </figure>
            </cite>
            <section className="card-body">
                <h2 className="card-title self-center md:self-start">{props.productItem.name}</h2>
                <p className="text-emerald-600 text-lg self-center md:self-start"><strong>{price}</strong></p>
                <p>{props.productItem.description}</p>
                <div className="card-actions justify-center md:justify-end">
                    <button className="btn btn-primary btn-block sm:btn-wide" onClick={() => dispatch(addCartItem({
                        cartItem: getCartItem(props.productItem),
                        quantity: 1
                    }))}>+Add
                    </button>
                </div>
            </section>
        </div>
    );
}

export default MenuItem;
