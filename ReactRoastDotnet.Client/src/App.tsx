import {Outlet} from "react-router-dom";
// My imports.
import TopNavBar from "./top-navbar/TopNavBar.tsx";
import BotNavBar from "./bottom-navbar/BotNavBar.tsx";

function App() {

    return (
        <>
            <TopNavBar isAuthenticated={false}/>
            <Outlet/>
            <BotNavBar isAuthenticated={false}/>
        </>
    );
}

export default App
