import {useState} from "react";
import {Link} from "react-router-dom";
// My imports
import useUserStore from "../store/userStore.ts";
import UserForm from "./UserForm.tsx";
import {path} from "../routes.tsx";

type Props = {
    onNext: VoidFunction;
    onPrev: VoidFunction;
}

function SubmitOrder(props: Props) {
    const [continueAsGuest, setContinueAsGuest] = useState(false);
    const user = useUserStore(state => state.user);

    if (continueAsGuest || user) {
        return <UserForm onNext={props.onNext} onPrev={props.onPrev} user={user}/>
    }

    return (
        <section className="hero-content mt-8 text-center">
            <div className="max-w-md text-gray-900 dark:text-white">
                <h2 className="mb-4 text-xl font-extrabold md:text-2xl lg:text-3xl">
                    Continue as guest?
                </h2>
                <div className="flex flex-col justify-center items-center gap-4">
                    <button className="btn btn-primary" onClick={() => setContinueAsGuest(true)}>Yes</button>
                    <Link to={path.signIn} className="link-secondary">No? Sign In</Link>
                </div>
            </div>
        </section>
    );
}

export default SubmitOrder;
