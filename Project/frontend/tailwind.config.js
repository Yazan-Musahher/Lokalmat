/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
     colors: {
      'custom-brown': '#6F4722',
      'dark-green': '#073403',
     },
    },
  },
  plugins: [],
}