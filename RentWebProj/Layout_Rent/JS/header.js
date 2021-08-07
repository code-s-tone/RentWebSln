//判斷搜尋裡有沒有打字 如果有打字或是游標還在 就不會還原成放大鏡
$("#header-search-input").on('focus', function () {
	$(this).parent('label').addClass('active');
});

$("#header-search-input").on('blur', function () {
	if ($(this).val().length == 0)
		$(this).parent('label').removeClass('active');
});

// 登入後的樣式 (先用按下登入鈕假裝已經登入) 之後改成判斷登入
document.querySelector(".nav-btn .loginBtn").addEventListener("click", changeHeaderCSS_login);

function changeHeaderCSS_login() {
	document.querySelector('.nav-btn').style.display = 'none';

}