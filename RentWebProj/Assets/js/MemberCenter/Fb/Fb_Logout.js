function Del_FB_App() {
    FB.getLoginStatus(function (response) {//取得目前user是否登入FB網站
        console.log(response);
        if (response.status === 'connected') {



            FB.api("/me/permissions", "DELETE", function (response) {
                console.log("刪除結果");
                console.log(response); //gives true on app delete success
                //最後一個參數傳遞true避免cache
                FB.getLoginStatus(function (res) { }, true);//強制刷新cache避免login status下次誤判

            });

        } else {
            console.log("無法刪除FB App");
        }
    });

}