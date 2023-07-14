import {ReactNode} from "react";
import {NavLink} from "react-router-dom";

const buttonNavClasses = "active:border-primary hover:border-primary active:ring-primary active:ring-2";
const activeNavClasses = "active text-secondary " + buttonNavClasses;

type Props = {
    children: ReactNode;
    route?: string;
}

function BottomNavButton(props: Props) {
    if (props.route) {
        return (
            <NavLink to={props.route} className={({isActive}) => isActive ? activeNavClasses : buttonNavClasses}>
                {props.children}
            </NavLink>
        );
    } else {
        return (
            <button className={buttonNavClasses}>
                {props.children}
            </button>
        );
    }
}

export default BottomNavButton;
