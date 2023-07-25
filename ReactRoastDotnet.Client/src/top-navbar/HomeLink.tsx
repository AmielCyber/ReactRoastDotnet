// My imports.
import TopNavLink from "./TopNavLink.tsx";
import CoffeeIcon from "../icons/CoffeeIcon.tsx";

function HomeLink() {
    return (
        <TopNavLink route="/">
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
