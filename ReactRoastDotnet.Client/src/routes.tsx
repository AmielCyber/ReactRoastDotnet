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

const path = {
    home: "/",
    menu: "/menu",
    checkout: "/checkout",
    signIn: "/auth/sign-in",
    signUp: "/auth/sign-up",
    notFound: "/not-found"
}

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {path: path.home, element: <HomePage/>},
            {path: path.menu, element: <Suspense fallback={<LoadingPage pageName={path.menu}/>}><MenuPage/></Suspense>},
            {
                path: path.checkout,
                element: <Suspense fallback={<LoadingPage pageName={path.checkout}/>}><CheckoutPage/></Suspense>
            },
            {
                path: path.signIn,
                element: <Suspense fallback={<LoadingPage pageName={path.signIn}/>}><SignInPage/></Suspense>
            },
            {
                path: path.signUp,
                element: <Suspense fallback={<LoadingPage pageName={path.signUp}/>}><SignUpPage/></Suspense>
            },
            {
                path: path.notFound,
                element: <NotFoundPage/>
            },
            {path: "*", element: <Navigate replace to={path.notFound}/>},
        ],
    },
]);

export default router;
export {path}
