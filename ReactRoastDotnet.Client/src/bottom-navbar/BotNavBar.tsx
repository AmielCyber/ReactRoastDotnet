import BotNavLinks from "./BotNavLinks.tsx";

type Props = {
    isAuthenticated: boolean;
}

function BotNavBar(props: Props) {
    return (
        <nav className="btm-nav btm-nav-lg md:hidden">
            <BotNavLinks isAuthenticated={props.isAuthenticated}/>
        </nav>
    );
}

export default BotNavBar;

