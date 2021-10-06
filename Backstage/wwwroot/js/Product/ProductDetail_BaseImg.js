window.onload = function () {
    BaseImg()
    var files = [];
    var that = this;
    $("#upload").click(function () {
        $("#file").trigger("click");
    })

    $("#file").change(function () {
        var img = document.getElementById("file").files;
        fileList = Array.from(img);
        var div = document.createElement("div");
        for (var i = 0; i < img.length; i++) {
            var file = img[i];
            var url = URL.createObjectURL(file);

            const reader = new FileReader()
            reader.readAsDataURL(file)
            reader.onload = function () {
                // 將圖片 src 替換為 DataURL
                //產生Img元素class名為img
                var box = document.createElement("img");
                box.setAttribute("src", reader.result);
                box.className = 'img';
                //產生Div元素
                var imgBox = document.createElement("div");
                imgBox.style.display = 'inline-block';
                imgBox.className = 'img-item box3';
                //產生Img頭上的XX
                var deleteIcon = document.createElement("span");
                deleteIcon.className = 'delete';
                deleteIcon.innerText = 'X';
                /*  deleteIcon.dataset.filename = img[i].name;*/
                //加入XX
                imgBox.appendChild(deleteIcon);
                imgBox.appendChild(box);
                var body = document.getElementsByClassName("gallery")[0];
                body.appendChild(imgBox);
                that.files = img;
                $(deleteIcon).click(function () {
                    var filename = $(this).data("filename");
                    $(this).parent().remove();
                    var fileList = Array.from(that.files);

                    for (var j = 0; j < fileList.length; j++) {
                        if (fileList[j].name === filename) {
                            fileList.splice(j, 1);
                            break;
                        }
                    }
                    that.files = fileList
                })

            }
        }

    })

}