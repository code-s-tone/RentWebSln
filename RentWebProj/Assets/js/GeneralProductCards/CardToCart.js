var isAuthenticated_Cards;

function productIdToCart(id) {
    if (isAuthenticated_Cards) {
        AjaxPostProductIdToCart(id);
    }
    else {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar":true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr["info"]("登入後擁有專屬購物車更方便!", "親 請先登入^^")
    }
}

function AjaxPostProductIdToCart(id) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-center",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    let countProductsInCart = parseInt($(".cartQuantity").text().trim());
    $.ajaxSetup({ cache: false });

    $.ajax({
        type: 'POST',
        url: "/Product/ProductToCart",
        data: { PID: id },
        success: function (response) {
            if (response) {
                countProductsInCart++;
                $(".cartQuantity").text(countProductsInCart);
                toastr['success']('結帳前記得選取租借時間喔！', '已成功加入購物車');            
            }
            else {
                toastr['success']('很喜歡就趕快租走吧！', '您之前已經加過此商品了喔！');
            }
        }
    });
}

