import type User from "../models/User";
import type AuthUser from "../models/AuthUser";

const CACHE_NAME = "app-cache";

type UserType = User | AuthUser;

function getStoredUserCache(): UserType | null {
    let user: UserType | null = null;

    const cache = localStorage.getItem(CACHE_NAME);
    if (cache !== null) {
        // If there is a cache stored.
        const userCache = JSON.parse(cache) as UserType | undefined;
        if (userCache) {
            user = userCache;
        }
    }

    return user;
}

function storeUserCache(user: UserType) {
    localStorage.setItem(
        CACHE_NAME,
        JSON.stringify(user)
    );
}

export {getStoredUserCache, storeUserCache};
