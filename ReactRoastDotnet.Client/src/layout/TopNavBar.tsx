import {NavLink} from "react-router-dom";
// My imports.
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import NavigationLinks from "./NavigationLinks.tsx";
import CartButton from "../cart/CartButton.tsx";

function TopNavBar() {
    return (
        <div className="navbar bg-base-100 max-w-screen-lg mx-auto hidden md:flex">
            <div className="navbar-start ml-8">
                <NavLink className="btn btn-ghost btn-circle normal-case text-xl gap-2 group" to="/">
                    <CoffeeIcon isTopIcon={true}/>
                </NavLink>
            </div>
            <div className="navbar-center hidden md:flex">
                <ul className="menu menu-horizontal px-1">
                    <NavigationLinks/>
                </ul>
            </div>
            <div className="navbar-end mr-8">
                <CartButton/>
            </div>
        </div>
    );
}

export default TopNavBar;
