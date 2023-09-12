import type AuthUser from "../models/AuthUser";
import type LoginRequest from "../models/LoginRequest.ts";
import type Result from "../models/Result.ts";
import type UserSignUpRequest from "../models/UserSignUpRequest.ts";
import getProblemDetails from "get-problem-details";

const BASE_URL = import.meta.env.VITE_API_URL as string;
const ACCOUNT_LOGIN_URL = BASE_URL + "account/login";
const ACCOUNT_REGISTER_URL = BASE_URL + "account/register";

async function signIn(userCredentials: LoginRequest): Promise<Result<AuthUser>> {
    try {
        const response: Response = await fetch(ACCOUNT_LOGIN_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userCredentials),
        });
        if (!response.ok) {
            const responseResult = await response.json() as unknown;
            return {
                ok: false,
                problemDetails: getProblemDetails(responseResult)
            };
        }
        const user = await response.json() as AuthUser;
        return {
            ok: true,
            value: user,
        };
    } catch (e) {
        return {
            ok: false,
            problemDetails: getProblemDetails("An Unknown Error Occurred")
        };
    }
}

async function signUp(userCredentials: UserSignUpRequest): Promise<Result<string>> {
    try {
        const response: Response = await fetch(ACCOUNT_REGISTER_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userCredentials),
        });
        if (!response.ok) {
            const responseResult = await response.json() as unknown;
            return {
                ok: false,
                problemDetails: getProblemDetails(responseResult)
            };
        }
        const user = await response.json() as AuthUser;
        return {
            ok: true,
            value: user.email
        };
    } catch (e) {
        return {
            ok: false,
            problemDetails: getProblemDetails("An Unknown Error Occurred")
        };
    }
}

export {signIn, signUp};
