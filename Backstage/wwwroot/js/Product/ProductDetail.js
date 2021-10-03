let json =
{
    "productId": "這是什麼都沒填，就被送回後端的情況",
    "productName": "string",
    "description": "string",
    "dailyRate": 0,
    "launchDate": "2021-10-01T13:20:52.783Z",
    "withdrawalDate": "2021-10-01T13:20:52.783Z",
    "discontinuation": false,
    "updateTime": "2021-10-01T13:20:52.783Z",
    "newProduct": true,
    "productImages": [
        {
            "sourceImages": "string"
        }
    ]
}
let vm;
function Binding() {

    vm = new Vue({
        el: "#app",
        data: {
            items: json,
            ProductIdError: true,
            ProductIdErrMsg: '',
            ProductNameError: true,
            ProductNameErrMsg: '',
            DescriptionError: true,
            DescriptionErrMsg: '',
            DailyRateError: true,
            DailyRateErrMsg: '',
            isdisabledFn: true,//所有的Rrror都是false的時候，buttom才能使用
            disabled: true
        },
        watch: {
            'items.ProductId': function () {
                var isText = /^[a-zA-Z0-9]+$/;
                if (!isText.test(this.items.ProductId)) {
                    this.ProductIdError = true;
                    this.ProductIdErrMsg = '請勿包含特殊字元';
                }
                else if (this.items.ProductId.length > 8) {
                    this.ProductIdError = true;
                    this.ProductIdErrMsg = '請勿超過8個字';
                }
                else {
                    this.ProductIdError = false;
                }
                if (vm.$data.ProductIdError == false && vm.$data.ProductNameError == false && vm.$data.DescriptionError == false && vm.$data.DailyRateError == false) {
                    vm.$data.disabled = false

                }
                else {
                    vm.$data.disabled = true
                }
            },
            'items.ProductName': function () {
                if (this.items.ProductName.length > 50) {
                    this.ProductNameError = true;
                    this.ProductNameErrMsg = '請勿超過50個字';
                }
                else {
                    this.ProductNameError = false;
                }
                if (vm.$data.ProductIdError == false && vm.$data.ProductNameError == false && vm.$data.DescriptionError == false && vm.$data.DailyRateError == false) {
                    vm.$data.disabled = false

                }
                else {
                    vm.$data.disabled = true
                }
            },
            'items.Description': function () {

                if (this.items.Description.length < 10) {
                    this.DescriptionError = true;
                    this.DescriptionErrMsg = '請填寫字數至少10個字';
                }
                else {
                    this.DescriptionError = false;
                }
                if (vm.$data.ProductIdError == false && vm.$data.ProductNameError == false && vm.$data.DescriptionError == false && vm.$data.DailyRateError == false) {
                    vm.$data.disabled = false
                }
                else {
                    vm.$data.disabled = true
                }
            },
            'items.DailyRate': function () {
                var isText = /[0-9]/;
                if (!isText.test(this.items.DailyRate)) {
                    this.DailyRateError = true;
                    this.DailyRateErrMsg = '請勿輸入數字以外的字元或符號';
                }
                else if (parseInt(this.items.DailyRate) < 1) {
                    this.DailyRateError = true;
                    this.DailyRateErrMsg = '請設定最少1元的金額';
                }
                else {
                    this.DailyRateError = false;
                }
                if (vm.$data.ProductIdError == false && vm.$data.ProductNameError == false && vm.$data.DescriptionError == false && vm.$data.DailyRateError == false) {
                    vm.$data.disabled = false
                }
                else {
                    vm.$data.disabled = true
                }
            },

        },
        methods: {
            fetchx() {
                document.querySelector(".spinner").classList.remove("d-none")
                //把div裡的圖片整理後，替換items陣列
                let PostArray = [];
                document.querySelectorAll(".img").forEach((e) => {
                    let entityObj = {
                        "SourceImages": e.src
                    }
                    PostArray.push(entityObj)
                })
                this.$data.items.ProductImages = PostArray;
                var result = JSON.stringify(vm.$data.items)
                fetch("/api/Product/UpdateProduct", {
                    method: "POST",
                    body: result,
                    headers: {
                        "Content-type": "application/json; charset=UTF-8"
                    }
                })
                    .then(response => {

                        document.querySelector(".spinner").classList.add("d-none")
                        return response.json();

                    })
                    .then(result => {
                        swal(result.result, {
                            icon: "info",
                        }).delay(2000);
                    })
                    .catch(ex => {
                        document.querySelector(".spinner").classList.add("d-none")

                    });
            },

        },
        components: {
            vuejsDatepicker
        },
    })
}

function BaseImg() {
    if (vm.$data.items.ProductImages != undefined) {
        var imgR = vm.$data.items.ProductImages

        var div = document.createElement("div");
        for (var i = 0; i < imgR.length; i++) {
            var file = imgR[i];
            var url = file.SourceImages

            //產生Img元素class名為img
            var box = document.createElement("img");
            box.setAttribute("src", url);
            box.className = 'img';
            //產生Div元素
            var imgBox = document.createElement("div");
            imgBox.style.display = 'inline-block';
            imgBox.className = 'img-item';
            //產生Img頭上的刪除標示
            var deleteIcon2 = document.createElement("span");
            deleteIcon2.className = 'delete';
            deleteIcon2.innerText = 'X';
            deleteIcon2.dataset.filename = imgR[i].name;
            //加入刪除標示進div的容器
            imgBox.appendChild(deleteIcon2);

            imgBox.appendChild(box);
            var body = document.getElementsByClassName("gallery")[0];
            body.appendChild(imgBox);

        };
        //註冊刪除
        $(".delete").click(function () {
            $(this).parent().remove();

        })
    }
}


function LoadData() {
    var getUrlString = location.href;
    var url = new URL(getUrlString);
    var result = url.searchParams.get('id');
    const Url = `/api/Product/GetProductDetail/${result}`;

    fetch(Url,
        {
            method: "get",
            headers: { 'Content-Type': 'application/json' },
        })
        .then(res => res.json())
        .then(result => {
            if (result.length != 0) {
                Binding()
                vm.$data.items = result[0];
                $(".Product_title").text("編輯產品頁");
                $("._ProductId").attr("readonly", "readonly")
                console.log("資料進來了");
            }
            else {
                Binding()
                vm.$data.items = json;
                $(".Product_title").text("新增產品頁");
            }

        })
        .catch(ex => {

            console.log("Fetch沒進來，所以是空白資料");
        })
};


//在dom原件就位的時候，進行Loading
$(document).ready(LoadData());


