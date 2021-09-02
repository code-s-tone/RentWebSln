//判斷搜尋裡有沒有打字 如果有打字或是游標還在 就不會還原成放大鏡
$("#header-search-input").on('focus', function () {
    $(this).parent('label').addClass('active');
});

$("#header-search-input").on('blur', function () {
    if ($(this).val().length == 0)
        $(this).parent('label').removeClass('active');
});

// 登入後的樣式 (先用按下桌機登入鈕假裝已經登入) 之後改成判斷登入
//document.querySelector(".nav-btn .loginBtn").addEventListener("click", function () {
//    changeHeaderCSS_login()
//    changeSidebarCSS_login()
//})

//function changeHeaderCSS_login() {
//    document.querySelector('.nav-btn').style.display = 'none';
//    document.querySelector('.member-dropdown').style.display = 'block';
//}

//不論在桌機版或手機版按登出都要回復
//document.querySelector(".member-dropdown .signOutBtn").addEventListener("click", function () {
//    backHeaderCSS_signOut();
//    backSidebarCSS_signOut()

//})

//function backHeaderCSS_signOut() {
//    document.querySelector('.nav-btn').style.display = 'block';
//    document.querySelector('.member-dropdown').style.display = 'none';

//}