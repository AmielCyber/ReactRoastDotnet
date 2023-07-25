import {useState} from "react";
// My imports.
import checkoutStep from "../checkout/checkoutHelper.ts";
import CheckoutTitle from "../checkout/CheckoutTitle.tsx";
import CheckoutStepsDisplay from "../checkout/CheckoutStepsDisplay.tsx";
import CheckoutComponent from "../checkout/CheckoutComponent.tsx";

function CheckoutPage() {
    const [stepNum, setStepNum] = useState(checkoutStep.reviewCart);

    const setNextStepNum = () => {
        if (stepNum < checkoutStep.receipt) {
            setStepNum(stepNum + 1);
        }
    };
    const setPrevStep = () => {
        if (stepNum > checkoutStep.reviewCart) {
            setStepNum(stepNum - 1);
        }
    }

    return (
        <main className="mx-auto max-w-screen-lg pb-2 md:pb-10 mb-24 md:mb-0">
            <div className="flex flex-col">
                <CheckoutTitle/>
                <CheckoutStepsDisplay stepNum={stepNum}/>
            </div>
            <div className="max-w-screen-md mx-auto">
                <CheckoutComponent stepNum={stepNum} onPrev={setPrevStep} onNext={setNextStepNum}/>
            </div>
        </main>
    );
}

export default CheckoutPage;
