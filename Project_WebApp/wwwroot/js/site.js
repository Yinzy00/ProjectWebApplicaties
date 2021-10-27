// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let borderTextColor = "px solid #E8D500";
let borderTextTransparent = "px solid transparent";




function checkBodyHeight() {
    //let windowHeight = window.innerHeight;
    let htmlHeight = document.querySelector("html").clientHeight;
    let x = (document.querySelector("header").clientHeight + document.querySelector("main").clientHeight + document.querySelector("footer").clientHeight)
    if (x < htmlHeight) {
        document.querySelector(".footer").style.position = "absolute";
    }
    else {
        document.querySelector(".footer").style.position = "relative";
    }
}

checkBodyHeight();

//HeaderDriehoek.style.borderRight = (windowWidth / 1.2) + borderTextColor;
//AuthDriehoek.style.borderBottom = (windowHeight / 1.5) + borderTextTransparent;
//AuthDriehoek.style.borderTop = (windowHeight / 2) + borderTextTransparent;
//AuthDriehoek.style.borderLeft = (windowWidth / 1.5) + borderTextColor;
window.onresize = () => {
    let windowWidth = window.innerWidth;
    //let windowHeight = window.innerHeight;
    let htmlHeight = document.querySelector("html").offsetHeight;

    let HeaderDriehoek = document.querySelector("#HeaderDriehoek");
    let AuthDriehoek = document.querySelector(".AuthDriehoek");

    //let HomeBgShapeCircle = document.querySelector(".HomeBgShapeCircle");

    //windowHeight = window.innerHeight;
    //windowWidth = window.innerWidth;
    checkBodyHeight();
    HeaderDriehoek.style.borderRight = (windowWidth / 1.2) + borderTextColor;

    if (AuthDriehoek !== null) {
        AuthDriehoek.style.borderBottom = (htmlHeight / 2) + borderTextTransparent;
        AuthDriehoek.style.borderTop = (htmlHeight / 2) + borderTextTransparent;
        AuthDriehoek.style.borderLeft = (windowWidth / 1.5) + borderTextColor;
    }
    //if (HomeBgShapeCircle !== null) {
    //    HomeBgShapeCircle.style.height = (windowWidth/1) + "px";
    //    HomeBgShapeCircle.style.width = windowWidth + "px";
    //    HomeBgShapeCircle.style.top = "-" + (windowWidth/2) + "px";
    //    //HomeBgShapeCircle.style.width = windowWidth;
    //}
}

window.onresize();




