// 庭安
// 商品卡片
let FirstSrc;
let SecondSrc;

function productCard_Hover(element) {
    FirstSrc = element.querySelector('img').getAttribute('src');
    SecondSrc = FirstSrc.replace('_1', '_2');
    element.querySelector('img').setAttribute('src', SecondSrc);
console.log(FirstSrc);
}

function productCard_Unhover(element) {
    element.querySelector('img').setAttribute('src', FirstSrc);
}