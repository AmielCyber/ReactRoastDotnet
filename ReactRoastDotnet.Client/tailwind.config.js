/** @type {import('tailwindcss').Config} */
export default {
    content: [
        "./index.html",
        "./src/**/*.{js,ts,jsx,tsx}",
    ],
    plugins: [
        require("@tailwindcss/typography"),
        require("daisyui")
    ],
    theme: {
        daisyui: {
            themes: ["light", "dark"],
        },
        extend: {},
    },
}

