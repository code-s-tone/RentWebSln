// Sidenav裡的下拉選單
let dropdownBtns = document.querySelectorAll(".dropdown-btn");
dropdownBtns.forEach(btn => {
    btn.addEventListener("click", function () {
        this.classList.toggle("active");
        let dropdownContent = this.nextElementSibling;

        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        }
        else {
            dropdownContent.style.display = "block";
        }
    })
});

$(document).ready(function () {

    $(document).on('click', function (e) {
        e.preventDefault();
        if (e.target.classList.contains("hb") && !($(".sidenav").hasClass("show"))) {
            $(".sidenav").addClass("show");
            $(".overlayMobile").addClass("active");
            $(".hb").addClass("hb_open");
        }
        else if ($(".sidenav").hasClass("show") && ($("#sidebarContent").find(e.target).length == 0 || e.target.classList.contains("hb"))) {
            $(".sidenav").removeClass("show");
            $(".overlayMobile").removeClass("active");
            $(".hb").removeClass("hb_open");
        }
    })
});