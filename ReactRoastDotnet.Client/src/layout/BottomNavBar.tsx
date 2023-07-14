import CartButton from "../cart/CartButton.tsx";
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import HomeIcon from "../icons/HomeIcon.tsx";
import BottomNavGuestLinks from "./BottomNavGuestLinks.tsx";
import BottomNavAuthLinks from "./BottomNavAuthLinks.tsx";
import BottomNavButton from "./BottomNavButton.tsx";

type Props = {
    isAuthenticated: boolean;
}

function BottomNavBar(props: Props) {
    return (
        <div className="btm-nav md:hidden">
            <BottomNavButton route="/">
                <HomeIcon/>
                <span className="btm-nav-label">Home</span>
            </BottomNavButton>
            <BottomNavButton route="menu">
                <CoffeeIcon isTopIcon={false}/>
                <span className="btm-nav-label">Order</span>
            </BottomNavButton>
            {props.isAuthenticated ? <BottomNavAuthLinks/> : <BottomNavGuestLinks/>}
            <BottomNavButton>
                <CartButton/>
            </BottomNavButton>
        </div>
    );
}

export default BottomNavBar;

