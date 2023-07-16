import TopNavLink from "./TopNavLink.tsx";

function TopNavAuthLinks() {
    return (
        <>
            <TopNavLink route="/account">
                Account
            </TopNavLink>
            <TopNavLink route="/account/sign-out">
                Sign Out
            </TopNavLink>
        </>
    )
}

export default TopNavAuthLinks;
