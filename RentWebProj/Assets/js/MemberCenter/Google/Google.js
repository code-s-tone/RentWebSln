
//jQuery處理button click event 當畫面DOM都載入時....
$(function () {
    $("#btnSignIn").on("click", function () {
        GoogleLogin();//Google 登入
    });
    $("#btnDisconnect").on("click", function () {
        Google_disconnect();//和Google App斷連
    });
});

function GoogleSigninInit() {
    gapi.load('auth2', function () {
        gapi.auth2.init({
            client_id: "826421646593-v6dtpka401bnj4khaps5d43al7uh9l6j.apps.googleusercontent.com"
        });
        console.log("GoogleSigninInit有啟用到")
    });//end gapi.load
}//end GoogleSigninInit function



function GoogleLogin() {
    document.querySelector(".Login").classList.add("spinner-1");
    let auth2 = gapi.auth2.getAuthInstance();//取得GoogleAuth物件
    auth2.signIn().then(function (GoogleUser) {

        let user_id = GoogleUser.getId();//取得user id，不過要發送至Server端的話，請使用↓id_token
        let AuthResponse = GoogleUser.getAuthResponse(true);//true會回傳access token ，false則不會，自行決定。如果只需要Google登入功能應該不會使用到access token
        let id_token = AuthResponse.id_token;//取得id_token
        $.ajax({
            url: "../Member/Google",
            method: "post",
            data: { id_token: id_token },
            success: function (msg) {
                console.log("Google登入成功");
   
                window.location.href = `${document.referrer}`;
           /*     window.location.href = "../Home/Index";*/
            },
            error: function (msg) {
                document.querySelector(".Login").classList.remove("spinner-1");
            }
        });//end $.ajax

    },
        function (error) {
            console.log("Google登入失敗");
            console.log(error);
        });

}//end function GoogleLogin



function Google_disconnect() {
    let auth2 = gapi.auth2.getAuthInstance(); //取得GoogleAuth物件

    auth2.disconnect().then(function () {
        console.log('User disconnect.');
    });
}