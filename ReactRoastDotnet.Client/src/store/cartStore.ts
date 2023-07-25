import {create} from "zustand";
// My imports.
import type CartItem from "../models/CartItem.ts"
import {addItem, removeAllItemsWithId, removeEveryItem, removeItem} from "./cartActions.ts";

interface CartState {
    items: CartItem[];
    status: string;
}

const initialState: CartState = {
    items: new Array<CartItem>(),
    status: "idle",
};

type Actions = {
    addCartItem: (cartItem: CartItem, quantity: number) => void;
    removeCartItem: (itemId: number, quantity: number) => void;
    removeAllCartItemsWithId: (itemId: number) => void;
    removeEveryCartItem: () => void;
}

const useCartStore = create<CartState & Actions>((set) => ({
    ...initialState,
    addCartItem: (cartItem: CartItem, quantity: number) =>
        set(state => {
            return ({items: addItem(state.items, cartItem, quantity)});
        }),
    removeCartItem: (itemId: number, quantity: number) =>
        set(state => {
            return ({items: removeItem(state.items, itemId, quantity)});
        }),
    removeAllCartItemsWithId: (itemId: number) =>
        set(state => {
            return ({items: removeAllItemsWithId(state.items, itemId)});
        }),
    removeEveryCartItem: () =>
        set(_ => {
            return ({items: removeEveryItem()})
        })
}))

export default useCartStore;
