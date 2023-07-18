import React from 'react'
import ReactDOM from 'react-dom/client'
import {RouterProvider} from "react-router-dom";
import {Provider} from "react-redux";
// My imports
import {store} from "./store/store.ts"
import {CartModalProvider} from "./hooks/CartModalContext.tsx";
import './index.css'
import router from "./routes.tsx"

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <CartModalProvider>
          <Provider store={store}>
              <RouterProvider router={router}/>
          </Provider>
      </CartModalProvider>
  </React.StrictMode>,
)
