import {ReactNode} from "react";
import {NavLink} from "react-router-dom";

const buttonNavClasses = "hover:bg-base-300";
const activeNavClasses = "active text-secondary " + buttonNavClasses;

type Props = {
    children: ReactNode;
    route: string;
}

function BottomNavButton(props: Props) {
    return (
        <NavLink to={props.route} className={({isActive}) => isActive ? activeNavClasses : buttonNavClasses}>
            {props.children}
        </NavLink>
    );
}

export default BottomNavButton;
