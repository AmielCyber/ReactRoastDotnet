import {NavLink} from "react-router-dom";
// My import.
import type User from "../models/User.ts";
import useUserStore from "../store/userStore.ts";
import {path} from "../routes.tsx";

function getGreeting(user: User | undefined) {
    if (user) {
        return `Welcome back ${user.firstName} ${user.lastName}, to `;
    }
    return "Welcome to ";
}

function HomePage() {
    const user = useUserStore(state => state.user);

    return (
        <main className="hero min-h-full px-6 py-24 sm:py-32 lg:px-8 mb-20 md:mb-0">
            <div className="hero-content mt-8 text-center">
                <div className="max-w-md text-gray-900 dark:text-white">
                    <h1 className="mb-4 text-3xl font-extrabold md:text-5xl lg:text-6xl">
                        {getGreeting(user)}
                        <span className="text-transparent bg-clip-text bg-gradient-to-r from-secondary to-primary">
              React Roast Dotnet
            </span>{" "}
                    </h1>
                    <p className="py-4 text-xl">Order fresh coffee to go</p>
                    <NavLink end to={path.menu} className="link link-secondary link-hover">
                        <p className="hover:animate-pulse text-2xl ">
                            Order Now!
                        </p>
                    </NavLink>
                </div>
            </div>
        </main>
    );
}

export default HomePage;
