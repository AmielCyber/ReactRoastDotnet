// My import.
import TopNavLink from "./TopNavLink.tsx";

function TopNavGuestLinks() {
    return (
        <>
            <TopNavLink route="/auth/sign-in">
                Sign In
            </TopNavLink>
            <TopNavLink route="/auth/sign-up">
                Sign Up
            </TopNavLink>
        </>
    )
}

export default TopNavGuestLinks;
