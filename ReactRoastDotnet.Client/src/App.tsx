import {Outlet} from "react-router-dom";
import {Toaster} from "react-hot-toast";
// My imports.
import useUserStore from "./store/userStore.ts";
import TopNavBar from "./top-navbar/TopNavBar.tsx";
import BotNavBar from "./bottom-navbar/BotNavBar.tsx";

const toastOptions = {
    style: {
        color: "hsl(var(--bc))",
        background: "hsl(var(--b1))",
    },
}

function App() {
    const user = useUserStore(state => state.user);
    const isAuthenticated = !!user;

    return (
        <>
            <Toaster position="top-right" toastOptions={toastOptions}/>
            <TopNavBar isAuthenticated={isAuthenticated}/>
            <Outlet/>
            <BotNavBar isAuthenticated={isAuthenticated}/>
        </>
    );
}

export default App
