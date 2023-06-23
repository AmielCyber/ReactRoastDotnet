import { useState } from "react";
// My imports.
import CartIcon from "../icons/CartIcon.tsx";
import Modal from "../layout/Modal";
import CartContent from "../cart/CartContent";
import Cart from "../models/Cart.ts";

function CartButton() {
  const [showCart, setShowCart] = useState(false);

  const cartDemo: Cart = {
    items: [],
    totalItems: 1,
    totalPrice: 5.95,
  }

  // Handlers.
  const showCartHandler = () => {
    setShowCart(true);
  };
  const hideCartHandler = () => {
    setShowCart(false);
  };
  return (
    <>
      <button tabIndex={0} className="btn btn-ghost btn-circle group" onClick={showCartHandler}>
        <div className="indicator">
          <CartIcon />
          <span className="indicator-item badge badge-sm badge-secondary">{cartDemo.totalItems}</span>
        </div>
      </button>
      <Modal showModal={showCart} onClose={hideCartHandler} dialogTitle="Payment Successful">
        <CartContent cart={cartDemo} onClose={hideCartHandler} />
      </Modal>
    </>
  );
}

export default CartButton;
