import NavigationLinks from "./NavigationLinks.tsx";
import {Bars3Icon} from "@heroicons/react/24/solid";

function DropDownNavMenu() {
  return (
    <div className="dropdown dropdown-end">
      <label tabIndex={0} className="btn btn-ghost md:hidden m-1">
        <Bars3Icon className="h-5 w-5 fill-primary hover:fill-primary-focus" />
      </label>
      <ul tabIndex={0} className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-100 rounded-box w-52">
        <NavigationLinks />
      </ul>
    </div>
  );
}

export default DropDownNavMenu;