import SignUpIcon from "../icons/SignUpIcon.tsx";
import SignInIcon from "../icons/SignInIcon.tsx";
import BotNavButton from "./BotNavButton.tsx";

function BotNavGuestLinks() {
    return (
        <>
            <BotNavButton route="auth/sign-up">
                <SignUpIcon/>
                <span className="btm-nav-label">Sign Up</span>
            </BotNavButton>
            <BotNavButton route="auth/sign-in">
                <SignInIcon/>
                <span className="btm-nav-label">Sign In</span>
            </BotNavButton>
        </>
    )
}

export default BotNavGuestLinks;
