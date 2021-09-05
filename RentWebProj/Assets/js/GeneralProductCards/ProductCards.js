// 庭安
// 商品卡片

function productCard_Hover2(element) {
    let FirstSrc = element.querySelector('img:first-child').getAttribute('src');
    let SecondSrc = FirstSrc.replace('_1', '_2')
    element.querySelector('img:first-child').setAttribute('src2', FirstSrc);
    element.querySelector('img:first-child').setAttribute('src', SecondSrc);
}

function productCard_Unhover2(element) {
    let SecondtSrc = element.querySelector('img:first-child').getAttribute('src2');
    element.querySelector('img:first-child').setAttribute('src', SecondtSrc);
}