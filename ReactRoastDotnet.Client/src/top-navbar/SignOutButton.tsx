// My import.
import useUserStore from "../store/userStore.ts";

function SignOutButton() {
    const signOut = useUserStore(state => state.signOutUser);

    const onSignOut = () => {
        signOut();
    }

    return (
        <button onClick={onSignOut} className="hover:text-primary">
            Sign Out
        </button>
    );
}

export default SignOutButton;
