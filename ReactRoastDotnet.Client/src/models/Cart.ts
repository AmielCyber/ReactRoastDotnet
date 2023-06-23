import {CartItem} from "./CartItem.ts";

// TODO: Rewrite Cart from our API DTO
export default interface Cart {
    items: CartItem[];
    totalItems: number;
    totalPrice: number;
}
