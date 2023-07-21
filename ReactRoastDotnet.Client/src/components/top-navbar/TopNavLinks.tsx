// My imports.
import CoffeeIcon from "../icons/CoffeeIcon.tsx";
import TopNavAuthLinks from "./TopNavAuthLinks.tsx";
import TopNavGuestLinks from "./TopNavGuestLinks.tsx";
import TopNavLink from "./TopNavLink.tsx";


type Props = {
    isAuthenticated: boolean;
}

function TopNavLinks(props: Props) {
    return (
        <>
            <TopNavLink route="/">
                <div className="pb-1">
                    <CoffeeIcon/>
                </div>
                {" Home"}
            </TopNavLink>
            <TopNavLink route="/menu">
                Menu
            </TopNavLink>
            {props.isAuthenticated ? <TopNavAuthLinks/> : <TopNavGuestLinks/>}
        </>
    );
}

export default TopNavLinks;
