import NavBar from "./layout/NavBar.tsx";
import {Outlet} from "react-router-dom";

function App() {

  return (
      <>
        <NavBar />
        <Outlet />
      </>

  );
}

export default App
