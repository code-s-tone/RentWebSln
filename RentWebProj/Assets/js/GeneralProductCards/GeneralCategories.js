// 庭安
// 種類卡片
function categoryCard_Hover(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://media.vogue.com.tw/photos/5ff5c24f7fdb36c87cf72cef/master/pass/IMG_9003.JPG');
}

function categoryCard_Unhover(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://media.vogue.com.tw/photos/5fe5aaba4663bf91fe312514/master/pass/2021%E9%A6%99%E6%B0%B4%20(1).jpg');
}

// 只是換照片測試 不需要留
function categoryCard_Hover2(element) {
    element.querySelector('img:first-child').setAttribute('src', 'img/Lv02.jpg');
}

function categoryCard_Unhover2(element) {
    element.querySelector('img:first-child').setAttribute('src', 'img/Lv01.jpg');
}