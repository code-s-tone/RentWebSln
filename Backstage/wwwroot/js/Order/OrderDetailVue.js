
let Json =
{
    "orderID": "",
    "fullName": "",
    "storeName": "",
    "phone": "",
    "goodsStatusID": "",
    "storeID": "",

}
let vm;
let url = location.href;
vm = new Vue({
    el: "#app",
    data: {
        items: Json,
        usernameError: true,
        userErrMsg: '',
        storeError: true,
        storeErrMsg: '',
        phoneError: true,
        phoneErrMsg: '',
        goodsError: true,
        goodsErrMsg: '',
        disabled: true

    },
    methods: {
        EditData() {
            let PUrl = "/api/OrderApi/UpdateOrder";
            fetch(PUrl,
                {
                    method: "Post",
                    body: JSON.stringify(this.$data.items),
                    headers: {

                        'Content-Type': 'application/json'
                    },
                })
                .then(res => {

                    this.$data.items = res[0];
                    console.log(res);
                    console.log(res.ok);
                    console.log(res.status);

                    swal("修改成功", {
                        icon: 'success',
                        button: false,
                    })
                    window.setTimeout("window.location ='/Order/Index/'", 2000);//兩秒後跳轉
                })
                .catch(ex => {
                    console.log("錯了");
                })
        },
    },
    watch: {
        'items.fullName': function () {
            var isText = /^[a-zA-Z0-9]+$/;
            if (this.items.fullName == '') {
                this.usernameError = true;
                this.userErrMsg = '姓名不得為空';
            }
            //else if (!isText.test(this.items.fullName)) {
            //    this.usernameError = true;
            //    this.userErrMsg = '請勿包含特殊字元';

            //}
            else {
                this.usernameError = false;
                this.userErrMsg = '';

            }
            if (vm.$data.usernameError == false && vm.$data.storeError == false && vm.$data.phoneError == false) {
                vm.$data.disabled = false

            }
            else {
                vm.$data.disabled = true
            }
        },
        'items.storeID': function () {
            var isText = /^[123]{1}$/;
            if (this.items.storeID == '') {
                this.storeError = true;
                this.storeErrMsg = '分店不得為空';
            }
            else if (!isText.test(this.items.storeID)) {
                this.storeError = true;
                this.storeErrMsg = '請輸入正確的分店格式 EX : 1、2、3';
            }
            else {
                this.storeError = false;
                this.storeErrMsg = '';
            }
            if (vm.$data.usernameError == false && vm.$data.storeError == false && vm.$data.phoneError == false ) {
                vm.$data.disabled = false

            }
            else {
                vm.$data.disabled = true
            }
        },
        'items.phone': function () {
            var isText = /^[0-9]+$/;
            if (this.items.phone == '') {
                this.phoneError = true;
                this.phoneErrMsg = '電話不得為空';
            }
            else if (!isText.test(this.items.phone)) {
                this.phoneError = true;
                this.phoneErrMsg = '請勿包含數字以外的特殊字元';

            }
            else if (this.items.phone.length != 10) {
                this.phoneError = true;
                this.phoneErrMsg = '請輸入正確的手機格式';
            }
            else {
                this.phoneError = false;
                this.phoneErrMsg = '';

            }
            if (vm.$data.usernameError == false && vm.$data.storeError == false && vm.$data.phoneError == false) {
                vm.$data.disabled = false

            }
            else {
                vm.$data.disabled = true
            }
        },
        //'items.goodsStatusID': function () {
        //    var isText = /[\u4e00-\u9fa5]{3}$/;
        //    if (this.items.goodsStatusID == '') {
        //        this.goodsError = true;
        //        this.goodsErrMsg = '貨物狀態不得為空';
        //    }
        //    else if (!isText.test(this.items.goodsStatusID)) {
        //        this.goodsError = true;
        //        this.goodsErrMsg = '請輸入正確的貨物狀態格式';
        //    }
        //    else {
        //        this.goodsError = false;
        //        this.goodsErrMsg = '';

        //    }
        //}

    }
});

function LoadData() {
    let orderID = url.split("/")[5]
    let GUrl = `/api/OrderApi/GetOrderDetail/${orderID}`;
    fetch(GUrl,
        {
            method: "Get",
            headers: {
                'Content-Type': 'application/json'
            },

        }).then(res => res.json())
        .then(result => {
            vm.$data.items = result[0];
            console.log(result);
            console.log("有進來了");
        })
        .catch(ex => {
            console.log("錯了");
        })
};

$(document).ready(function () {
    LoadData();
});
