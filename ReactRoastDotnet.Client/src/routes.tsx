import {lazy, Suspense} from "react";
import {createBrowserRouter, Navigate} from "react-router-dom";
// My imports.
import App from "./App.tsx";
import HomePage from "./pages/HomePage.tsx";
import LoadingPage from "./pages/LoadingPage.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import SignInPage from "./pages/SignInPage.tsx";
import SignUpPage from "./pages/SignUpPage.tsx";

const MenuPage = lazy(() => import("./pages/MenuPage.tsx"));

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {path: "/", element: <HomePage/>},
            {path: "/menu", element: <Suspense fallback={<LoadingPage pageName={"menu"}/>}><MenuPage/></Suspense>},
            {
                path: "/auth/sign-in",
                element: <Suspense fallback={<LoadingPage pageName={"sign-in"}/>}><SignInPage/></Suspense>
            },
            {
                path: "/auth/sign-up",
                element: <Suspense fallback={<LoadingPage pageName={"sign-in"}/>}><SignUpPage/></Suspense>
            },
            {
                path: "/not-found",
                element: <Suspense fallback={<LoadingPage pageName={"not-found"}/>}><NotFoundPage/></Suspense>
            },
            {path: "*", element: <Navigate replace to="/not-found"/>},
        ],
    },
]);

export default router;
