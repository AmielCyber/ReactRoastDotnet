import {lazy, Suspense} from "react";
import {createBrowserRouter, Navigate} from "react-router-dom";
// My imports.
import App from "./App.tsx";
import HomePage from "./pages/HomePage.tsx";
import LoadingPage from "./pages/LoadingPage.tsx";

const MenuPage = lazy(() => import("./pages/MenuPage.tsx"));

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {path: "/", element: <HomePage/>},
            {path: "/menu", element: <Suspense fallback={<LoadingPage pageName={"menu"}/>}><MenuPage/></Suspense>},
            {path: "*", element: <Navigate replace to="/not-found"/>},
        ],
    },
]);

export default router;
