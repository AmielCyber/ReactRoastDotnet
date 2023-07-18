import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import type Cart from "../models/Cart";
import type CartItem from "../models/CartItem";
import {addCartItem, removeCartItem, clearCart, getCurrentDate} from "./cart-actions";

interface CartState {
    cart: Cart;
    status: string;
}

const initialState: CartState = {
    cart: {
        // TODO: Remove demo cart when we finish with product list page.
        items: [
            {id: 1, price: 5, type: "Drink", name: "Coffee", quantity: 2},
            {id: 2, price: 4, type: "Drink", name: "Latte", quantity: 1},
            {id: 3, price: 4.75, type: "Drink", name: "Cold Brew", quantity: 3},
            {id: 4, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
            {id: 5, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
            {id: 6, price: 4.75, type: "Drink", name: "Machiatto", quantity: 3},
        ],
        //items: [],
        lastModified: new Date(Date.now())
    },
    status: "idle",
};

type RemoveItemAction = {
    itemId: number;
    quantity?: number;
}

export const cartSlice = createSlice({
    name: "cart",
    initialState,
    reducers: {
        addItem: (state, action: PayloadAction<CartItem>) => {
            state.cart.items = addCartItem(state.cart.items, action.payload)
            state.cart.lastModified = getCurrentDate();
        },
        removeItem: (state, action: PayloadAction<RemoveItemAction> ) => {
            state.cart.items = removeCartItem(state.cart.items, action.payload.itemId, action.payload.quantity);
            state.cart.lastModified = getCurrentDate();
        },
        removeAllItems: (state) => {
            state.cart.items = clearCart();
            state.cart.lastModified = getCurrentDate();
        }
    }
})

export const  {addItem, removeItem, removeAllItems} = cartSlice.actions;
