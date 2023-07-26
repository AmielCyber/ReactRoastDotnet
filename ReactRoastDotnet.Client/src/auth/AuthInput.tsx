// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-nocheck
// Needed since react-hook-form does not play well with TS.
import {forwardRef, Ref} from "react";
import {ChangeHandler, RefCallBack} from "react-hook-form";


type Props = {
    type: string;
    name: string;
    onChange: ChangeHandler;
    onBlur: ChangeHandler;
    labelText: string;
    autoComplete?: string;
    errorMsg?: string;
    value?: string;
    disabled?: boolean;
}

const inputClasses = "input input-bordered focus:input-primary dark:text-white";
const errorInputClasses = "input input-bordered input-error";
const labelClasses = "label-text";
const errorLabelClasses = "label-text text-error";

function InputComponent(props: Props, ref: Ref<RefCallBack>) {
    return (
        <div className="form-control">
            <label className="label">
                <span
                    className={props.errorMsg ? errorLabelClasses : labelClasses}
                >
                    {props.labelText}
                </span>
            </label>
            <input
                id={props.name}
                type={props.type}
                ref={ref}
                name={props.name}
                /* eslint-disable-next-line @typescript-eslint/no-misused-promises */
                onChange={props.onChange}
                /* eslint-disable-next-line @typescript-eslint/no-misused-promises */
                onBlur={props.onBlur}
                placeholder={props.name}
                className={props.errorMsg ? errorInputClasses : inputClasses}
                autoComplete={props.autoComplete}
                value={props.value}
                disabled={props.disabled}
            />
            <p className="text-sm text-error pt-1 pl-2">{props.errorMsg ?? ""}</p>
        </div>
    );
}

const AuthInput = forwardRef(InputComponent);

export default AuthInput;


