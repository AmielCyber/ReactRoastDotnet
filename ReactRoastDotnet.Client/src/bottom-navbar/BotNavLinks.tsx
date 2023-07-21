// My imports.
import BotNavButton from "./BotNavButton.tsx";
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import BagIcon from "../icons/BagIcon.tsx";
import BotNavAuthLinks from "./BotNavAuthLinks.tsx";
import BotNavGuestLinks from "./BotNavGuestLinks.tsx";

type Props = {
    isAuthenticated: boolean;
}

function BotNavLinks(props: Props) {
    return (
        <>
            <BotNavButton route="/">
                <CoffeeIcon/>
                <span className="btm-nav-label">Home</span>
            </BotNavButton>
            <BotNavButton route="menu">
                <BagIcon/>
                <span className="btm-nav-label">Order</span>
            </BotNavButton>
            {props.isAuthenticated ? <BotNavAuthLinks/> : <BotNavGuestLinks/>}
        </>
    )

}

export default BotNavLinks;
