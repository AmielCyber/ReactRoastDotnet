import {NavLink} from "react-router-dom";
// My imports.
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import NavigationLinks from "./NavigationLinks.tsx";
import CartButton from "../cart/CartButton.tsx";
import DropDownNavMenu from "./DropDownNavMenu.tsx";

function NavBar() {
    return (
        <div className="navbar bg-base-100 max-w-screen-xl mx-auto">
            <div className="navbar-start ml-8">
                <div tabIndex={0}>
                    <NavLink className="btn btn-ghost btn-circle normal-case text-xl gap-2 group" to="/">
                        <CoffeeIcon/>
                    </NavLink>
                </div>
            </div>
            <div className="navbar-center hidden md:flex">
                <ul className="menu menu-horizontal px-1">
                    <NavigationLinks/>
                </ul>
            </div>
            <div className="navbar-end mr-8">
                <CartButton/>
                <DropDownNavMenu/>
            </div>
        </div>
    );
}

export default NavBar;