//頭像顯示(偷吃步)
document.querySelector(".Profile2").src = document.querySelector(".Profile").src
//隱藏的input鍵跟i標籤連結在一起
document.querySelector(".fa-camera").addEventListener("click", function () {
    document.querySelector(".efg").click()

})
const myFile = document.querySelector('#file2');
myFile.addEventListener('change', function (e) {
    const file = e.target.files[0]
    const reader = new FileReader()
    reader.readAsDataURL(file)

    reader.onload = function () {
        // 將圖片 src 替換為 DataURL
        myFile.setAttribute("value", reader.result);

        //            swal({
        //title: "欲更換的頭像",
        //icon: `${document.querySelector('#file2').getAttribute("value")}`,
        //            });

        swal({
            title: "確定要修改為這個頭像嗎?",
            text: "一旦更動後就無法取回之前的頭像",
            icon: `${document.querySelector('#file2').getAttribute("value")}`,
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    swal("頭像已更新完成", {
                        icon: "success",
                    });
                    document.querySelector(".Profile2").src = reader.result;
                    $.ajax({
                        type: "POST",
                        url: "/Member/ProfilePhoto",
                        traditional: true,
                        data: { "blobUrl": document.querySelector('#file2').getAttribute("value") },
                        success: function () {
                            console.log("傳送成功")
                            window.location.href = "/Member/MemberCenter";
                        },
                        error: function () {
                            console.log("傳送失敗")
                        }
                    });
                } else {
                    swal("取消，請重來");
                }
            });
    }
})
