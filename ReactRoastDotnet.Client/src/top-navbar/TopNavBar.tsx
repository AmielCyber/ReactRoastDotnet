// My imports.
import TopNavLinks from "./TopNavLinks.tsx";
import CartButton from "../cart/CartButton.tsx";

type Props = {
    isAuthenticated: boolean;
}

function TopNavBar(props: Props) {
    return (
        <div
            className="navbar bg-base-100 opacity-90 top-0 bottom-0 sticky max-w-screen-lg mx-auto hidden md:flex z-20">
            <nav className="justify-start ml-8">
                <ul className="menu menu-horizontal menu-lg px-1">
                    <TopNavLinks isAuthenticated={props.isAuthenticated}/>
                </ul>
            </nav>
            <div className="justify-end ml-auto mr-8">
                <CartButton isTopNav={true}/>
            </div>
        </div>
    );
}

export default TopNavBar;
