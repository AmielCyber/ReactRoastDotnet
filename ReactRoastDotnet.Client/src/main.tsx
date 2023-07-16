import React from 'react'
import ReactDOM from 'react-dom/client'
import {RouterProvider} from "react-router-dom";
// My imports
import {CartProvider} from "./components/cart/CartContext.tsx";
import './index.css'
import router from "./routes.tsx"

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <CartProvider>
            <RouterProvider router={router}/>
        </CartProvider>
    </React.StrictMode>,
);
