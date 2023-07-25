import {Link} from "react-router-dom";

function EmptyCart() {
    return (
        <section className="hero mt-6">
            <div className="hero-content flex-col text-center">
                <h2 className="text-3xl font-bold">Your Cart is empty!</h2>
                <div className="flex self-center">
                    <Link className="btn btn-primary" to="/menu">
                        Add Items
                    </Link>
                </div>
            </div>
        </section>
    );
}

export default EmptyCart;
