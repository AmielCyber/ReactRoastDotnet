import {Outlet} from "react-router-dom";
// My imports.
import TopNavBar from "./components/top-navbar/TopNavBar.tsx";
import BotNavBar from "./components/bottom-navbar/BotNavBar.tsx";
import CartModal from "./components/cart/CartModal.tsx";
import {useEffect} from "react";

function App() {

    useEffect(() => {
        console.log("START!");
    }, [])

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
