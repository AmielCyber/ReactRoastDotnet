// My import.
import checkoutStep from "./checkoutHelper.ts";

const stepClass = "step";
const prevStepClass = stepClass + " step-primary";
const currStepClass = prevStepClass + " text-secondary";

function getStepClass(stepNum: number, currStepNum: number): string {
    if (stepNum < currStepNum) {
        return prevStepClass;
    } else if (stepNum === currStepNum) {
        return currStepClass;
    } else {
        return stepClass;
    }
}

type Props = {
    stepNum: number;
}

function CheckoutStepsDisplay(props: Props) {
    return (
        <ul className="steps pt-4">
            <li className={getStepClass(checkoutStep.reviewCart, props.stepNum)}>Cart Review</li>
            <li className={getStepClass(checkoutStep.submitOrder, props.stepNum)}>Submit Order</li>
            <li className={getStepClass(checkoutStep.receipt, props.stepNum)}>Order Confirmation</li>
        </ul>
    );
}

export default CheckoutStepsDisplay;
