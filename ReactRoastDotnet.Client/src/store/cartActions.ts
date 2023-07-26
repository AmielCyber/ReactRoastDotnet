import type CartItem from "../models/CartItem";

class NegativeQuantityError extends Error {
    constructor(msg: string) {
        super(msg);
        console.error(msg);
    }
}

function addItem(items: CartItem[], cartItem: CartItem, quantity = 1): CartItem[] {
    if (quantity < 1) {
        throw new NegativeQuantityError("cartItem must have a positive quantity value in addCartItem");
    }
    if (items.length === 0) {
        return new Array<CartItem>(cartItem);
    }
    const existingItem = items.find(item => item.id === cartItem.id);
    if (existingItem) {
        return items.map(item => {
            if (item.id === cartItem.id) {
                return {
                    ...cartItem,
                    quantity: item.quantity + quantity
                };
            }
            return item;
        });
    }
    const newItems = items.slice();
    newItems.push(cartItem);

    return newItems;
}

function removeItem(items: CartItem[], itemId: number, quantity?: number): CartItem[] {
    if (items.length === 0) {
        return [];
    }
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

function removeAllItemsWithId(items: CartItem[], itemId: number): CartItem[] {
    return items.filter(item => item.id !== itemId);
}

function removeEveryItem(): CartItem[] {
    return new Array<CartItem>();
}

export {addItem, removeItem, removeAllItemsWithId, removeEveryItem};
