var item;
let vm;
vm = new Vue({
    el: "#app",
    data: {
        items: [
            {
                ProductId: "01",
                ProductName: "",
                Description: "",
                DailyRate: "",
                LaunchDate: "",
                WithdrawalDate: "",
                Discontinuation: false,
                UpdateTime:"",
                ProductImages: []
            }

        ],

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

            this.$data.items[0].ProductImages = PostArray;
            fetch("https://localhost:5001/api/Product/UpdateProduct", {
                method: "POST",
                body: JSON.stringify(this.$data.items[0]),
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            })
                .then(response => {
                    console.log(response);
                    console.log(response.ok);
                    console.log(response.status);
                    swal("修改成功", {
                        icon: "success",
                    }).delay(2000);
                    document.querySelector(".spinner").classList.add("d-none")

                })
                .then(result => {
                    console.log(result)
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
function BaseImg() {
    var imgR = vm.$data.items[0].ProductImages

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
        //產生Img頭上的XX
        var deleteIcon2 = document.createElement("span");
        deleteIcon2.className = 'delete';
        deleteIcon2.innerText = 'X';
        deleteIcon2.dataset.filename = imgR[i].name;
        //加入XX
        imgBox.appendChild(deleteIcon2);

        imgBox.appendChild(box);
        var body = document.getElementsByClassName("gallery")[0];
        body.appendChild(imgBox);

    };

    $(".delete").click(function () {
        $(this).parent().remove();

    })

}


function LoadData() {
    var url = location.href;
    var id = url.split("/")[5]
    const Url = `/api/Product/GetProductDetail/${id}`;

    fetch(Url,
        {
            method: "get",
            headers: { 'Content-Type': 'application/json' },
        })
        .then(res => res.json())
        .then(result => {
            vm.$data.items = result;
            console.log("終於看到你進來了");
        })
        .catch(ex => {
            console.log("你沒進來?");
        })
};
function NewImg() {
    
}


$(document).ready(function () {

    LoadData();

});
