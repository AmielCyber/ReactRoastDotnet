import {Bars3Icon} from "@heroicons/react/24/solid";
import NavigationLinks from "./NavigationLinks.tsx";

function SideBarMenu() {
    return (
        <div className="drawer drawer-end">
            <input id="my-drawer-4" type="checkbox" className="drawer-toggle"/>
            <div className="drawer-content">
                {/* Page content here */}
                <label htmlFor="my-drawer-4" className="drawer-button btn btn-ghost md:hidden">
                    <Bars3Icon className="h-5 w-5 fill-primary hover:fill-primary-focus"/>
                </label>
            </div>
            <div className="drawer-side z-[1]">
                <label htmlFor="my-drawer-4" className="drawer-overlay"></label>
                <ul className="menu p-4 w-60 h-full bg-base-200 text-base-content">
                    <NavigationLinks/>
                </ul>
            </div>
        </div>
    );
}

export default SideBarMenu;