// My imports.
import useUserStore from "../store/userStore.ts";
import SignOutIcon from "../icons/SignOutIcon.tsx";

const buttonNavClasses = "hover:bg-base-300 text-primary hover:text-accent ";

// TODO: Add Account Links
function BotNavAuthLinks() {
    const signOut = useUserStore(state => state.signOutUser);

    const onSignOut = () => {
        signOut();
    }

    return (
        <>
            <button className={buttonNavClasses} onClick={onSignOut}>
                <SignOutIcon/>
                <span className="btm-nav-label">Sign Out</span>
            </button>
        </>
    );
}

export default BotNavAuthLinks;
