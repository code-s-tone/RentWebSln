$('#category').on('change', function (e) {

    var optionSelected = $("option:selected", this);
    //console.log(optionSelected);
    var valueSelected = this.value;
    var subSelector = document.querySelector('#subCategory');

    //取消Ajax快取
    $.ajaxSetup({ cache: false });

    $.ajax({
        type: 'GET',
        url: "/Product/GetSubCategoryOptions?categoryID=" + valueSelected,

        dataType: "json",
        success: function (response) {
            let optionIni = document.createElement('option');
            subSelector.innerText = "";
            optionIni.value = "0";
            optionIni.innerText =optionSelected[0].childNodes[0].nodeValue == "選擇商品種類" ?  "選擇子分類" : `從${optionSelected[0].childNodes[0].nodeValue}再細分`;
            //改寫成上面那句原因: 如果選了某大類又不要選類別了 選回預設(選擇商品種類)後的子類標題也要回預設
            //optionIni.innerText = `從${optionSelected[0].childNodes[0].nodeValue}再細分`;
            subSelector.appendChild(optionIni);

            response.forEach(sub => {
                let option = document.createElement('option');
                option.innerText = sub.SubCategoryName;
                option.value = sub.SubCategoryID;
                subSelector.appendChild(option);
            });
        }
    });
});