import {NavLink} from "react-router-dom";
import {ReactNode} from "react";

const activeClass = "text-secondary";
const linkClass = "hover:text-primary";


type Props = {
    route: string;
    children: ReactNode;
}

function TopNavLink(props: Props) {
    return (
        <li>
            <NavLink end to={props.route} className={({isActive}) => isActive ? activeClass : linkClass}>
                {props.children}
            </NavLink>
        </li>
    );
}

export default TopNavLink;
