import {Outlet} from "react-router-dom";
import TopNavBar from "./layout/TopNavBar.tsx";
import BottomNavBar from "./layout/BottomNavBar.tsx";
import CartModal from "./cart/CartModal.tsx";

function App() {

    return (
        <>
            <CartModal />
            <TopNavBar/>
            <Outlet/>
            <BottomNavBar isAuthenticated={false}/>
        </>

    );
}

export default App
