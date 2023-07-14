import {Outlet} from "react-router-dom";
import TopNavBar from "./layout/TopNavBar.tsx";
import BottomNavBar from "./layout/BottomNavBar.tsx";

function App() {
    return (
        <>
            <TopNavBar/>
            <Outlet/>
            <BottomNavBar isAuthenticated={false}/>
        </>

    );
}

export default App
