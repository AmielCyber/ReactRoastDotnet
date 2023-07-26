// My imports.
import TopNavAuthLinks from "./TopNavAuthLinks.tsx";
import TopNavGuestLinks from "./TopNavGuestLinks.tsx";
import TopNavLink from "./TopNavLink.tsx";
import {path} from "../routes.tsx";


type Props = {
    isAuthenticated: boolean;
}

function TopNavLinks(props: Props) {
    return (
        <>
            <TopNavLink route={path.menu}>
                Menu
            </TopNavLink>
            {props.isAuthenticated ? <TopNavAuthLinks/> : <TopNavGuestLinks/>}
        </>
    );
}

export default TopNavLinks;
