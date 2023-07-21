type Props = {
    totalPrice: string;
    totalItems: number;
}

function CartSummaryTotal(props: Props) {
    return (
        <section className="flex-col my-0 py-0 justify-center gap-2 pt-4 pl-8 max-w-screen-sm">
            <div className="flex items-center gap-4">
                <h3 className="text-lg card-title">Total Items:</h3>
                <strong className="text-secondary">{props.totalItems}</strong>
            </div>
            <div className="flex items-center gap-4">
                <h3 className="card-title text-lg">Total Price:</h3>
                <strong className="text-emerald-500">{props.totalPrice}</strong>
            </div>
        </section>
    )
}

export default CartSummaryTotal;
