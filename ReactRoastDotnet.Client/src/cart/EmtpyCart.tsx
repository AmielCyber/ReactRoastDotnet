import {Dialog} from "@headlessui/react";

type Props = {
    onClose: VoidFunction;
};

function EmptyCart(props: Props) {
    return (
        <>
            <Dialog.Title as="h3" className="text-lg font-medium leading-6 text-gray-900">
                Your cart is empty.
            </Dialog.Title>
            <div className="mt-2">
                <button onClick={props.onClose} className="btn btn-primary mt-6">
                    Close
                </button>
            </div>
        </>
    );
}

export default EmptyCart;