import {create} from "zustand";
// My imports.
import type AuthUser from "../models/AuthUser";
import type LoginRequest from "../models/LoginRequest.ts";
import type {ProblemDetails} from "get-problem-details";
import {signIn} from "./userActions.ts";
import problemToast from "../toast/problemToast.tsx";

// TODO: Add persistence middleware

interface UserState {
    user?: AuthUser;
    error?: ProblemDetails;
    loading: boolean;
}

const initialState: UserState = {
    user: undefined,
    error: undefined,
    loading: false,
}

type Actions = {
    signInUser: (userCredentials: LoginRequest) => Promise<void>;
    signOutUser: () => void;
}

const useUserStore = create<UserState & Actions>((set) => ({
    ...initialState,
    signInUser: async (userCredentials: LoginRequest) => {
        set({loading: true});
        const result = await signIn(userCredentials);
        if (result.ok) {
            set({
                user: result.value,
                loading: false,
            })
        } else {
            set({
                error: result.problemDetails,
                loading: false,
            })
            problemToast(result.problemDetails);
        }
    },
    signOutUser: () => set(initialState),
}));

export default useUserStore;
