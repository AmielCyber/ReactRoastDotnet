import CartButton from "../cart/CartButton.tsx";
import BotNavLinks from "./BotNavLinks.tsx";

type Props = {
    isAuthenticated: boolean;
}

function BotNavBar(props: Props) {
    return (
        <nav className="btm-nav md:hidden">
            <BotNavLinks isAuthenticated={props.isAuthenticated}/>
            <CartButton isTopNav={false}/>
        </nav>
    );
}

export default BotNavBar;

