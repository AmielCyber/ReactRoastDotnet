// My imports.
import TopNavLink from "./TopNavLink.tsx";
import {path} from "../routes.tsx";
import CoffeeIcon from "../icons/CoffeeIcon.tsx";

function HomeLink() {
    return (
        <TopNavLink route={path.home}>
            <div
                className="tooltip tooltip-right tooltip-accent pb-1"
                data-tip="Home"
                aria-label="Home"
            >
                <CoffeeIcon/>
            </div>
        </TopNavLink>
    );
}

export default HomeLink;
