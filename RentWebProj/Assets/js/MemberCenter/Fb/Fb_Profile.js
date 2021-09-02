function GetProfile() {
    document.getElementById('content').innerHTML = "";//先清空顯示結果

    //FB.api()使用說明：https://developers.facebook.com/docs/javascript/reference/FB.api
    //取得用戶個資
    FB.api("/me", "GET", { fields: 'last_name,first_name,name,email' }, function (user) {
        //user物件的欄位：https://developers.facebook.com/docs/graph-api/reference/user
        if (user.error) {
            console.log(response);
        } else {
            document.getElementById('content').innerHTML = JSON.stringify(user);
            $.ajax({
                url: "../Member/Fb",
                method: "post",
                data: { id_token: JSON.stringify(user) },
                success: function () {
                    console.log("Fb登入成功");
                }
            });
        }
    });

}