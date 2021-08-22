// 庭安
// 商品卡片
function productCard_Hover(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://tnntoday.com/wp-content/uploads/2021/04/Maserati.jpg');
} 

function productCard_Unhover(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://www.chanel.com/images//t_one///q_auto:good,f_jpg,fl_lossy,dpr_1.2/w_620/earrings-gold-pink-pearly-white-metal-resin-imitation-pearls-strass-metal-resin-imitation-pearls-strass-packshot-default-ab7009b06560nf087-8841063301150.jpg');
}

// 只是換照片測試 不需要留
function productCard_Hover2(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://tnntoday.com/wp-content/uploads/2021/04/Maserati.jpg');
}

function productCard_Unhover2(element) {
    element.querySelector('img:first-child').setAttribute('src', 'https://www.chanel.com/images//t_one///q_auto:good,f_jpg,fl_lossy,dpr_1.2/w_1920/necklace-gold-pearly-white-crystal-metal-resin-imitation-pearls-glass-pearls-strass-metal-resin-imitation-pearls-glass-pearls-strass-packshot-alternative-ab7012b06558nf085-8841059467294.jpg');
}