import {NavLink} from "react-router-dom";

// TODO: Add React Router Links
function NavigationLinks() {
    return (
        <>
            <li tabIndex={0}>
                <NavLink end to="/">
                    Home
                </NavLink>
            </li>
            <li tabIndex={0}>
                <NavLink end to="/menu">
                    Menu
                </NavLink>
            </li>
            <li tabIndex={0}>
                <a>Account</a>
            </li>
            <li tabIndex={0}>
                <a>Sign In</a>
            </li>
            <li tabIndex={0}>
                <a>Create Account</a>
            </li>
        </>
    );
}

export default NavigationLinks;