document.querySelectorAll(".col-12 img").forEach(image => {
    image.onclick = function (x) {
        x.target.setAttribute("data-bs-toggle","modal");
        x.target.setAttribute("data-bs-target","#exampleModal");
    }
});

document.querySelectorAll(".SeeMore").forEach(image => {
    image.onclick = function (x) {
        x.target.setAttribute("data-bs-toggle","modal");
        x.target.setAttribute("data-bs-target","#exampleModal");
    }
});
var swiper = new Swiper(".mySwiper", {
pagination: {
    el: ".swiper-pagination",
  dynamicBullets: true,
},
effect:"coverflow",
zoom : true,
navigation: {
  nextEl: ".swiper-button-next",
  prevEl: ".swiper-button-prev",
},
});