import SignUpIcon from "../icons/SignUpIcon.tsx";
import SignInIcon from "../icons/SignInIcon.tsx";
import BottomNavButton from "./BottomNavButton.tsx";

function BottomNavGuestLinks() {
    return (
        <>
            <BottomNavButton route="account/create">
                <SignUpIcon/>
                <span className="btm-nav-label">Sign Up</span>
            </BottomNavButton>
            <BottomNavButton route="account/signin">
                <SignInIcon/>
                <span className="btm-nav-label">Sign In</span>
            </BottomNavButton>
        </>
    )
}

export default BottomNavGuestLinks;
