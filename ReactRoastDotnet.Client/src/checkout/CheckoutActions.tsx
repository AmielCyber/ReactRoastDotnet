// My import.
import checkoutStep from "./checkoutHelper.ts";

type Props = {
    stepNum: number;
    onClearCart?: VoidFunction;
    onBack: VoidFunction;
    onNext?: VoidFunction;
    isSubmitting?: boolean;
    disableOnNext?:  boolean;
}

function CheckoutActions(props: Props) {
    return (
        <menu className="modal-action flex flex-col sm:flex-row items-stretch gap-2 mx-4">
            {props.stepNum === checkoutStep.reviewCart && (
                <li className="flex flex-col">
                    <button
                        id="clearCart"
                        className="btn btn-sm btn-error"
                        type="button"
                        onClick={props.onClearCart}
                    >
                        Clear Cart
                    </button>
                </li>
            )}
            <li className="flex flex-col">
                <button
                    id="backButton"
                    key="backButton"
                    className="btn btn-sm btn-primary"
                    type="button"
                    onClick={props.onBack}
                    disabled={props.stepNum === checkoutStep.reviewCart}
                >
                    Back
                </button>
            </li>
            <li className="flex flex-col">
                <button
                    id="nextButton"
                    key="nextButton"
                    className="btn btn-sm btn-success"
                    type={props.stepNum === checkoutStep.submitOrder ? "submit" : "button"}
                    onClick={props.onNext}
                    disabled={props.stepNum === checkoutStep.receipt || props.disableOnNext}
                >
                    {props.stepNum === checkoutStep.submitOrder ? "Submit" : "Next"}
                    {props.isSubmitting && <span className="loading loading-spinner"></span>}
                </button>

            </li>
        </menu>
    )
}

export default CheckoutActions;
