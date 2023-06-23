import {Dialog, Transition} from "@headlessui/react";
import {Fragment, useRef} from "react";
import {XMarkIcon} from "@heroicons/react/20/solid";

type Props = {
    showModal: boolean;
    onClose: VoidFunction;
    dialogTitle: string;
    children: React.ReactNode;
};

function Modal(props: Props) {
    const closeButtonRef = useRef(null);
    return (
        <Transition appear show={props.showModal} as={Fragment}>
            <Dialog as="div" className="relative z-10" onClose={props.onClose} initialFocus={closeButtonRef}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-out duration-300"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in duration-200"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    {/* Black Drop */}
                    <div className="fixed inset-0 bg-black bg-opacity-25"/>
                </Transition.Child>
                {/* Scrollable modal. */}
                <div className="fixed inset-0 overflow-y-auto">
                    {/* Modal Container */}
                    <div className="flex mt-10 justify-center p-4 text-center">
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
                                    ref={closeButtonRef}
                                    className="btn btn-sm btn-primary btn-ghost btn-circle absolute left-2 top-2 text-primary"
                                    onClick={props.onClose}
                                >
                                    <XMarkIcon/>
                                </button>
                                {props.children}
                            </Dialog.Panel>
                        </Transition.Child>
                    </div>
                </div>
            </Dialog>
        </Transition>
    );
}

export default Modal;