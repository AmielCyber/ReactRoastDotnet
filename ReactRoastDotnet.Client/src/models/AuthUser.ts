// My import.
import User from "./User"

interface AuthUser extends User {
    token: string;
}

export default AuthUser;
