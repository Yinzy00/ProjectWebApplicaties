// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Header driehoek
let windowWidth = window.innerWidth;
let driehoek = document.querySelector(".driehoek");
driehoek.style.borderRight = (windowWidth / 1.2) + "px solid #E8D500";;
window.onresize = () => {
    windowWidth = window.innerWidth;
    driehoek.style.borderRight = (windowWidth / 1.2) + "px solid #E8D500";;
}