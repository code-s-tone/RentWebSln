$(document).ready(function () {
    //抓到總共有幾個產品
    let ProductSum = document.querySelectorAll(".Product").length;
    //SeeMore顯示還有幾張圖沒出來
    document.querySelector(".SeeMore").innerText = `See All ${ProductSum}`
    //把index超過2的產品圖display:none
    document.querySelectorAll(".Product").forEach((item, idx) => {
        if (idx > 1) {
            item.style.display = 'none'

        }
    })
});