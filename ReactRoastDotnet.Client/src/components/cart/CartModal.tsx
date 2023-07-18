import {Dialog, Transition} from "@headlessui/react";
import {createRef, Fragment, useContext} from "react";
// My imports.
import XMarkIcon from "../icons/XMarkIcon.tsx";
import {CartModalContext, CartContextType} from "../../hooks/CartModalContext.tsx";
import CartContent from "./CartContent.tsx";


function CartModal() {
    const {showCart, setShowCart} = useContext(CartModalContext) as CartContextType;
    const submitButtonRef = createRef<HTMLButtonElement>();
    const closeCart = () => {
        setShowCart(false);
    }

    return (
        <Transition appear show={showCart} as={Fragment}>
            <Dialog as="div" className="relative z-20" onClose={closeCart} initialFocus={submitButtonRef}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-out duration-300"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in duration-200"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    {/*Backdrop*/}
                    <div className="fixed inset-0 bg-black bg-opacity-25"/>
                </Transition.Child>
                {/* Scrollable modal. */}
                <div className="fixed inset-0 overflow-y-auto">
                    {/* CartModal Container */}
                    <div className="flex mt-10 justify-center p-4 ">
                        <Transition.Child
                            as={Fragment}
                            enter="ease-out duration-300"
                            enterFrom="opacity-0 scale-95"
                            enterTo="opacity-100 scale-100"
                            leave="ease-in duration-200"
                            leaveFrom="opacity-100 scale-100"
                            leaveTo="opacity-0 scale-95"
                        >
                            <Dialog.Panel className="modal-box relative">
                                <button
                                    className="btn btn-sm btn-primary btn-ghost btn-circle absolute left-2 top-2 text-primary"
                                    onClick={closeCart}
                                >
                                    <XMarkIcon/>
                                </button>
                                <CartContent onClose={closeCart} ref={submitButtonRef}/>
                            </Dialog.Panel>
                        </Transition.Child>
                    </div>
                </div>
            </Dialog>
        </Transition>
    );
}

export default CartModal;
