// My imports.
import AccountIcon from "../icons/AccountIcon.tsx";
import SignOutIcon from "../icons/SignOutIcon.tsx";
import BotNavButton from "./BotNavButton.tsx";

function BotNavAuthLinks() {
    return (
        <>
            <BotNavButton route="/account/signout">
                <SignOutIcon/>
                <span className="btm-nav-label">Sign Out</span>
            </BotNavButton>
            <BotNavButton route="/account">
                <AccountIcon/>
                <span className="btm-nav-label">Account</span>
            </BotNavButton>
        </>
    );
}

export default BotNavAuthLinks;
