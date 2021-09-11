
let dNoneOnPhone = document.querySelectorAll(".dNoneOnPhone");
document.querySelector("#Keyword").addEventListener("click", function () {
    dNoneOnPhone.forEach(x => x.classList.remove('dNoneOnPhone'));
});

