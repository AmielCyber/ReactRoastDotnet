import {Link} from "react-router-dom";
// My import.
import {path} from "../routes.tsx";

function OrderConfirmation() {
    return (
        <section className="hero mt-6">
            <div className="hero-content flex-col text-center">
                <h2 className="text-3xl font-bold">Your Order was created!</h2>
                <div className="flex self-center">
                    <Link className="btn btn-primary" to={path.home}>
                        Go To Home
                    </Link>
                </div>
            </div>
        </section>);
}

export default OrderConfirmation
