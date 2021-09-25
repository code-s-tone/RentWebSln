let blogEditorApp = new Vue({
    el: "#blogEditorApp",
    data: {
        inputData: {
            title: "",
            date: "",
            imgUrl: "",
            imgTitle: "",
            preview: "",
        },
        inputDataCheck: {
            titleError: false,
            titleErrMsg: "",
            dateError: false,
            dateErrMsg: "",
            imgUrlError: false,
            imgUrlErrMsg: "",
            imgTitleError: false,
            imgTitleErrMsg: "",
            previewError: false,
            previewErrMsg: "",

        },
        dataAvailable: true, //true means all data is correct, so btn is not disabled (!true or false)
    },
    watch: {
        "inputData.title": {
            immediate: true,
            handler: function () {
                if (this.inputData.title == "") {
                    this.inputDataCheck.titleError = true;
                    this.inputDataCheck.titleErrMsg = "標題不得為空";
                }
                else {
                    this.inputDataCheck.titleError = false;
                    this.inputDataCheck.titleErrMsg = "";
                }
                this.checkDataAvailable();
            }
        },
        "inputData.date": {
            immediate: true,
            handler: function () {
                if (this.inputData.date == "") {
                    this.inputDataCheck.dateError = true;
                    this.inputDataCheck.dateErrMsg = "日期不得為空";
                }
                else {
                    this.inputDataCheck.dateError = false;
                    this.inputDataCheck.dateErrMsg = "";
                }
                this.checkDataAvailable();
            }
        },
        "inputData.imgTitle": {
            immediate: true,
            handler: function () {
                if (this.inputData.imgTitle == "") {
                    this.inputDataCheck.imgTitleError = true;
                    this.inputDataCheck.imgTitleErrMsg = "名稱不得為空";
                }
                else {
                    this.inputDataCheck.imgTitleError = false;
                    this.inputDataCheck.imgTitleErrMsg = "";
                }
                this.checkDataAvailable();
            }
        },
        "inputData.imgUrl": {
            immediate: true,
            handler: function () {
                let urlRegexp = /^https?:\/\/(.+\/)+.+(\.(gif|png|jpg|jpeg|webp|svg|psd|bmp|tif))$/i;
                if (this.inputData.imgUrl == "") {
                    this.inputDataCheck.imgUrlError = true;
                    this.inputDataCheck.imgUrlErrMsg = "網址不得為空";
                }
                else if (!urlRegexp.test(this.inputData.imgUrl)) {
                    this.inputDataCheck.imgUrlError = true;
                    this.inputDataCheck.imgUrlErrMsg = "非正確網址格式";
                }
                else {
                    this.inputDataCheck.imgUrlError = false;
                    this.inputDataCheck.imgUrlErrMsg = "";
                }
                this.checkDataAvailable();
            }
        },


        "inputData.preview": {
            immediate: true,
            handler: function () {
                let limitRegexp = /^[^\s]{1,100}$/;
                if (this.inputData.preview == "") {
                    this.inputDataCheck.previewError = true;
                    this.inputDataCheck.previewErrMsg = "內容不得為空";
                }
                else if (!limitRegexp.test(this.inputData.preview)) {
                    this.inputDataCheck.previewError = true;
                    this.inputDataCheck.previewErrMsg = "預覽內容勿超過100字";
                }
                else {
                    this.inputDataCheck.previewError = false;
                    this.inputDataCheck.previewErrMsg = "";
                }
                this.checkDataAvailable();
            },
        },
    },
    methods: {
        checkDataAvailable() {
            for (let prop in this.inputDataCheck) {
                //if there is any error(err==true) in checkarray, dataAvailable is false
                if (this.inputDataCheck[prop] == true) {
                    this.dataAvailable = false;
                    return;
                }
            }
            this.dataAvailable = true;
        },
    },
},

)

CKEDITOR.replace('BlogContent', {
    //uiColor: '#fc8d61',
    uiColor: '#ff9388',
});

//confirm if users type something on editor before post data to backend
function checkContent() {
    var data = CKEDITOR.instances.BlogContent.getData()
    if (data.length == 0) {
        alert("請補上文章內容再送出")
        return false;
    }
    else {
        return true;
    }
}