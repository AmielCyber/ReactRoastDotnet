const emailOptions = {
    required: "Email is required",
    pattern: {
        value: /^.+@[^.].*\.[a-z]{2,}$/,
        message: "Please enter a valid email"
    }
}
const passwordOptions = {
    required: "Password is required",
    minLength: {
        value: 6,
        message: "Min length is 6"
    },
    pattern: {
        value: /(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$/,
        message: "Password must contain the following characters: 1 uppercase, 1 lowercase, 1 digit, and 1 special."
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

export {emailOptions, passwordOptions, nameOptions};
