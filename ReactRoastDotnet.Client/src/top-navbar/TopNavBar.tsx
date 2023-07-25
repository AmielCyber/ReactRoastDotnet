// My imports.
import HomeLink from "./HomeLink.tsx";
import TopNavLinks from "./TopNavLinks.tsx";
import CartLink from "../cart/CartLink.tsx";

type Props = {
    isAuthenticated: boolean;
}

function TopNavBar(props: Props) {
    return (
        <div className="bg-base-100 top-0 sticky opacity-90 hidden md:flex z-20">
            <div className="navbar mx-auto max-w-screen-md">
                <nav className="navbar-start">
                    <ul>
                        <HomeLink/>
                    </ul>
                </nav>
                <nav className="navbar-center">
                    <ul className="menu menu-horizontal menu-lg px-1">
                        <TopNavLinks isAuthenticated={props.isAuthenticated}/>
                    </ul>
                </nav>
                <nav className="navbar-end">
                    <ul>
                        <CartLink isTopNav={true}/>
                    </ul>
                </nav>
            </div>
        </div>
    );
}

export default TopNavBar;
