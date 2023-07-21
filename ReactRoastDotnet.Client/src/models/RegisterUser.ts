import User from "./User.ts";
interface RegisterUser extends User{
    password: string;
}
export default RegisterUser;
