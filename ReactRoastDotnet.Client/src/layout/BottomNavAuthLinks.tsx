import AccountIcon from "../icons/AccountIcon.tsx";
import SignOutIcon from "../icons/SignOutIcon.tsx";
import BottomNavButton from "./BottomNavButton.tsx";

function BottomNavAuthLinks() {
    return (
        <>
            <BottomNavButton route="/account">
                <AccountIcon/>
                <span className="btm-nav-label">Account</span>
            </BottomNavButton>
            <BottomNavButton route="/account/signout">
                <SignOutIcon/>
                <span className="btm-nav-label">Sign Out</span>
            </BottomNavButton>
        </>
    );
}

export default BottomNavAuthLinks;
