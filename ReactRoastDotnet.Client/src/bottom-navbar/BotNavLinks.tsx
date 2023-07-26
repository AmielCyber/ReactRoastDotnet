// My imports.
import BotNavButton from "./BotNavButton.tsx";
import {path} from "../routes.tsx";
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import BagIcon from "../icons/BagIcon.tsx";
import BotNavAuthLinks from "./BotNavAuthLinks.tsx";
import BotNavGuestLinks from "./BotNavGuestLinks.tsx";
import CartLink from "../cart/CartLink.tsx";

type Props = {
    isAuthenticated: boolean;
}

function BotNavLinks(props: Props) {
    return (
        <>
            <BotNavButton route={path.home}>
                <CoffeeIcon/>
                <span className="btm-nav-label">Home</span>
            </BotNavButton>
            <BotNavButton route={path.menu}>
                <BagIcon/>
                <span className="btm-nav-label">Order</span>
            </BotNavButton>
            {props.isAuthenticated ? <BotNavAuthLinks/> : <BotNavGuestLinks/>}
            <CartLink isTopNav={false}/>
        </>
    )

}

export default BotNavLinks;
