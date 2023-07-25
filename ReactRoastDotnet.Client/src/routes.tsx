import {lazy, Suspense} from "react";
import {createBrowserRouter, Navigate} from "react-router-dom";
// My imports.
import App from "./App.tsx";
import HomePage from "./pages/HomePage.tsx";
import LoadingPage from "./pages/LoadingPage.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";

const MenuPage = lazy(() => import("./pages/MenuPage.tsx"));
const CheckoutPage = lazy(() => import("./pages/CheckoutPage.tsx"));
const SignInPage = lazy(() => import("./pages/SignInPage.tsx"));
const SignUpPage = lazy(() => import("./pages/SignUpPage.tsx"));

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {path: "/", element: <HomePage/>},
            {path: "/menu", element: <Suspense fallback={<LoadingPage pageName={"menu"}/>}><MenuPage/></Suspense>},
            {
                path: "/checkout",
                element: <Suspense fallback={<LoadingPage pageName={"checkout"}/>}><CheckoutPage/></Suspense>
            },
            {
                path: "/auth/sign-in",
                element: <Suspense fallback={<LoadingPage pageName={"sign-in"}/>}><SignInPage/></Suspense>
            },
            {
                path: "/auth/sign-up",
                element: <Suspense fallback={<LoadingPage pageName={"sign-up"}/>}><SignUpPage/></Suspense>
            },
            {
                path: "/not-found",
                element: <NotFoundPage/>
            },
            {path: "*", element: <Navigate replace to="/not-found"/>},
        ],
    },
]);

export default router;
