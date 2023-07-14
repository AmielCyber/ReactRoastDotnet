import {NavLink} from "react-router-dom";

// TODO: Add React Router Links
function NavigationLinks() {
    return (
        <>
            <li>
                <NavLink end to="/">
                    Home
                </NavLink>
            </li>
            <li>
                <NavLink end to="/menu">
                    Menu
                </NavLink>
            </li>
            <li>
                <a>Account</a>
            </li>
            <li>
                <a>Sign In</a>
            </li>
            <li>
                <a>Create Account</a>
            </li>
        </>
    );
}

export default NavigationLinks;
