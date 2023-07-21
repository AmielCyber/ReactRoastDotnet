import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import type Cart from "../models/Cart";
import type CartItem from "../models/CartItem";
import {addItem, removeAllItemsWithId, removeEveryItem, removeItem} from "./cart-actions";

interface CartState {
    cart: Cart;
    status: string;
}

const initialState: CartState = {
    cart: {
        items: [],
    },
    status: "idle",
};

type RemoveItemAction = {
    itemId: number;
    quantity?: number;
}

type AddItemAction = {
    cartItem: CartItem;
    quantity?: number;
}

export const cartSlice = createSlice({
    name: "cart",
    initialState,
    reducers: {
        addCartItem: (state, action: PayloadAction<AddItemAction>) => {
            state.cart.items = addItem(state.cart.items, action.payload.cartItem, action.payload.quantity)
        },
        removeCartItem: (state, action: PayloadAction<RemoveItemAction>) => {
            state.cart.items = removeItem(state.cart.items, action.payload.itemId, action.payload.quantity);
        },
        removeAllCartItemsWithId: (state, action: PayloadAction<number>) => {
            state.cart.items = removeAllItemsWithId(state.cart.items, action.payload);
        },
        clearCart: (state) => {
            state.cart.items = removeEveryItem();
        }
    }
})

export const {
    addCartItem, removeCartItem, removeAllCartItemsWithId,
    clearCart
} = cartSlice.actions;
