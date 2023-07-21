import {Outlet} from "react-router-dom";
// My imports.
import TopNavBar from "./top-navbar/TopNavBar.tsx";
import BotNavBar from "./bottom-navbar/BotNavBar.tsx";
import CartModal from "./cart/CartModal.tsx";

function App() {

    return (
        <>
            <CartModal/>
            <TopNavBar isAuthenticated={false}/>
            <Outlet/>
            <BotNavBar isAuthenticated={false}/>
        </>
    );
}

export default App
