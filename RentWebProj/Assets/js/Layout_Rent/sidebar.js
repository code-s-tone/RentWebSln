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

// 漢堡點開秀sidebar 按其他地方後移除
$(document).ready(function () {

    $(document).on('click', function (e) {
        if (e.target.classList.contains("hb") && !($(".sidenav").hasClass("show"))) {
            e.preventDefault();
            $(".sidenav").addClass("show");
            $(".overlayMobile").addClass("active");
            $(".hb").addClass("hb_open");
        }
        else if ($(".sidenav").hasClass("show") && ($("#sidebarContent").find(e.target).length == 0 || e.target.classList.contains("hb"))) {
            e.preventDefault();
            $(".sidenav").removeClass("show");
            $(".overlayMobile").removeClass("active");
            $(".hb").removeClass("hb_open");
        }
    })
});
//--------------------------------
// 登入後的樣式 (先用按下桌機跟手機的登入鈕假裝已經登入) 之後改成判斷登入
document.querySelector(".sidebar-btn .loginBtn").addEventListener("click", function () {
    changeHeaderCSS_login()
    changeSidebarCSS_login()
})

function changeSidebarCSS_login() {
    document.querySelector('.sidebar-btn').style.display = 'none';
    document.querySelector('.logoBox').style.display = 'none';
    document.querySelector('#sidebarContent .profileBox').style.display = 'flex';
    document.querySelector('.sidebar-member-link').style.display = 'block';

}
//登出後恢復原狀
document.querySelector(".sidebar-member-link .signOutBtn").addEventListener("click", function () {
    backHeaderCSS_signOut();
    backSidebarCSS_signOut()
})

function backSidebarCSS_signOut() {
    document.querySelector('.sidebar-btn').style.display = 'flex';
    document.querySelector('.logoBox').style.display = 'flex';
    document.querySelector('#sidebarContent .profileBox').style.display = 'none';
    document.querySelector('.sidebar-member-link').style.display = 'none';

}