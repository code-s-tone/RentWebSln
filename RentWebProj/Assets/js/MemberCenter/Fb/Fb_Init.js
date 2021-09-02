(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

window.fbAsyncInit = function () {
    FB.init({
        appId: "223201836424269",//FB appID
        cookie: true,  // enable cookies to allow the server to access the session
        xfbml: true,  // parse social plugins on this page
        version: 'v11.0' // use graph api version
    });
    FB.AppEvents.logPageView();

};
