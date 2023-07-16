import {Outlet} from "react-router-dom";
import TopNavBar from "./components/top-navbar/TopNavBar.tsx";
import BotNavBar from "./components/bottom-navbar/BotNavBar.tsx";
import CartModal from "./components/cart/CartModal.tsx";

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
