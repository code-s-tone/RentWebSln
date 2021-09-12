var isAuthenticated_Cards;

function productIdToCart(id) {
    if (isAuthenticated_Cards) {
        AjaxPostProductIdToCart(id);
    }
    else {
        alert("親 請先登入喔");
    }
}

function AjaxPostProductIdToCart(id) {
    $.ajaxSetup({ cache: false });

    $.ajax({
        type: 'POST',
        url: "/Product/ProductToCart",
        data: { PID: id },
        success: function (response) {
            if (response) {
                alert("已成功加入購物車 結帳前記得選取租借時間喔！");
            }
            else {
                alert("您之前已經加過此商品了喔！很喜歡就趕快租走吧！");
            }
        }
    });
}