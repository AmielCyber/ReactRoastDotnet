// My import.
import TopNavLink from "./TopNavLink.tsx";
import {path} from "../routes.tsx";

function TopNavGuestLinks() {
    return (
        <>
            <TopNavLink route={path.signIn}>
                Sign In
            </TopNavLink>
            <TopNavLink route={path.signUp}>
                Sign Up
            </TopNavLink>
        </>
    )
}

export default TopNavGuestLinks;
