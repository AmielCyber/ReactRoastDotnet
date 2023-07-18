import type CartItem from "../models/CartItem";
class NegativeQuantityError extends Error {
    constructor(msg: string) {
        super(msg);
        console.error(msg);
    }
}
function addCartItem(items: CartItem[], cartItem: CartItem): CartItem[] {
    if (cartItem.quantity < 1) {
        throw new NegativeQuantityError("cartItem must have a positive quantity value in addCartItem");
    }
    return items.map(item => {
        if (item.id === cartItem.id) {
            return {
                ...cartItem,
                quantity: cartItem.quantity + item.quantity
            };
        }
        return item;
    });
}

function removeCartItem(items: CartItem[], itemId: number, quantity?: number): CartItem[] {
    if (quantity) {
        if (quantity < 1) {
            throw new NegativeQuantityError("quantity must be a positive value in removeCartItem");
        }
        const existingItem = items.find(item => item.id === itemId);
        const updatedQuantity = existingItem ? existingItem.quantity - quantity : 0;
        if (updatedQuantity > 0) {
            return items.map(item => {
                if (item.id === itemId) {
                    return {
                        ...item,
                        quantity: updatedQuantity,
                    }
                }
                return item;
            })
        }
    }
    return items.filter(item => item.id !== itemId);
}

function clearCart(): CartItem[] {
    return new Array<CartItem>();
}

function getCurrentDate(){
    return new Date(Date.now());
}

export {addCartItem, removeCartItem, clearCart, getCurrentDate};
