const lowerCaseRegex = /[a-z]/;
const upperCaseRegex = /[A-Z]/;
const numberRegex = /[0-9]/;
const specialCharRegex = /[!@#$%^&*()_\-+=\][}{":;'?.`,~]/;
function hasLowerCase(val: string): string | undefined{
    if(lowerCaseRegex.test(val)){
        return;
    }
    return "Password must contain a lowercase letter.";
}
function hasUpperCase(val: string): string | undefined{
    if(upperCaseRegex.test(val)){
        return;
    }
    return "Password must contain an uppercase letter.";
}
function hasNumber(val: string): string | undefined{
    if(numberRegex.test(val)){
        return;
    }
    return "Password must contain a number.";
}
function hasSpecialCharacter(val: string): string | undefined{
    if(specialCharRegex.test(val)){
        return;
    }
    return "Password must contain a special character.";
}
const signInPasswordOptions = {
    required: "Password is required",
    minLength: {
        value: 6,
        message: "Min length is 6"
    }
}
const signUpPasswordOptions = {
    required: "Password is required",
    minLength: {
        value: 6,
        message: "Min length is 6"
    },
    validate: {
        hasLowerCase,
        hasUpperCase,
        hasNumber,
        hasSpecialCharacter,
    },
}

const emailOptions = {
    required: "Email is required",
    pattern: {
        value: /^.+@[^.].*\.[a-z]{2,}$/,
        message: "Please enter a valid email"
    }
}

const nameOptions = {
    required: "Name is required",
    minLength: {
        value: 1,
        message: "Please enter a name"
    },
    pattern: {
        value: /^(?!\s*$).+/,
        message: "Please enter non-empty name"
    }
}

export {emailOptions, nameOptions, signInPasswordOptions, signUpPasswordOptions};
