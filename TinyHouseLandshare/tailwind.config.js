module.exports = {
    content: ["./Views/**/*.{html,cshtml,js}"],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/line-clamp'),
    ],
}
