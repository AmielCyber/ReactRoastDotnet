import BotNavButton from "./BotNavButton.tsx";
import {path} from "../routes.tsx";
import SignUpIcon from "../icons/SignUpIcon.tsx";
import SignInIcon from "../icons/SignInIcon.tsx";

function BotNavGuestLinks() {
    return (
        <>
            <BotNavButton route={path.signUp}>
                <SignUpIcon/>
                <span className="btm-nav-label">Sign Up</span>
            </BotNavButton>
            <BotNavButton route={path.signIn}>
                <SignInIcon/>
                <span className="btm-nav-label">Sign In</span>
            </BotNavButton>
        </>
    )
}

export default BotNavGuestLinks;
