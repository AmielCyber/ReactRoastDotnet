import {ReactNode} from "react";
import {NavLink} from "react-router-dom";

const buttonNavClasses = "hover:bg-base-300 text-primary hover:text-accent ";
const activeNavClasses = "active text-secondary hover:bg-base-300";

type Props = {
    children: ReactNode;
    route: string;
}

function BotNavButton(props: Props) {
    return (
        <NavLink to={props.route} className={({isActive}) => isActive ? activeNavClasses : buttonNavClasses}>
            {props.children}
        </NavLink>
    );
}

export default BotNavButton;
