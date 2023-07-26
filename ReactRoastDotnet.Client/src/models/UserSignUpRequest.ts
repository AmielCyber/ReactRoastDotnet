// My import.
import User from "./User.ts";

interface UserSignUpRequest extends User {
    password: string;
}

export default UserSignUpRequest;
