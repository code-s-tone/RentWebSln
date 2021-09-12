function FBLogin() {
    FB.getLoginStatus(function (res) {
        console.log(`status:${res.status}`);//Debug

        if (res.status === "connected") {

            Del_FB_App();


        } else if (res.status === 'not_authorized' || res.status === "unknown") {

            FB.login(function (response) {
                document.querySelector(".Login").classList.add("spinner-1");
                if (response.status === 'connected') {
                    let userID = response["authResponse"]["userID"];
                    let access = response["authResponse"]["accessToken"];
                    console.log(`已授權App登入FB 的 userID:${userID}`);
                    console.log(`acessToken:${access}`);
                    $.ajax({
                        url: "../Member/Fb",
                        method: "post",
                        data: { id_token: access },
                        success: function () {
                            console.log("fb的accessToken傳送成功");
                            window.location.href = `${document.referrer}`;
                        }

                    });
                } else {
                    document.querySelector(".Login").classList.remove("spinner-1");
                    // user FB取消授權
                    alert("Facebook帳號無法登入");
                }
                //"public_profile"可省略，仍然可以取得name、userID
            }, { scope: 'email' });
        }
    });
}
