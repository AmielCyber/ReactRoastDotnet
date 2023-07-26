import {Outlet} from "react-router-dom";
// My imports.
import useUserStore from "./store/userStore.ts";
import TopNavBar from "./top-navbar/TopNavBar.tsx";
import BotNavBar from "./bottom-navbar/BotNavBar.tsx";

function App() {
    const user = useUserStore(state => state.user);
    const isAuthenticated = !!user;

    return (
        <>
            <TopNavBar isAuthenticated={isAuthenticated}/>
            <Outlet/>
            <BotNavBar isAuthenticated={isAuthenticated}/>
        </>
    );
}

export default App
