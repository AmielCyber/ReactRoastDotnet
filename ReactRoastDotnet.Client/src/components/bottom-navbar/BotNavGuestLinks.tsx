import SignUpIcon from "../icons/SignUpIcon.tsx";
import SignInIcon from "../icons/SignInIcon.tsx";
import BotNavButton from "./BotNavButton.tsx";

function BotNavGuestLinks() {
    return (
        <>
            <BotNavButton route="account/create">
                <SignUpIcon/>
                <span className="btm-nav-label">Sign Up</span>
            </BotNavButton>
            <BotNavButton route="account/signin">
                <SignInIcon/>
                <span className="btm-nav-label">Sign In</span>
            </BotNavButton>
        </>
    )
}

export default BotNavGuestLinks;
