completeBtn.addEventListener('click', function () {
    let modified = modifiedTarget();
    if (modified.value == "false") {
        modified.value = "true";
    }
    let ckBox = checkBoxTarget();
    if (ckBox.disabled == true) {
        ckBox.disabled = false;
    }

    //改變天數、小計、總計、異動過、核取權限
    let days = Math.ceil(Math.abs(
        (new Date(startDateTimeText) - new Date(endDateTimeText)) / 1000 / 60 / 60 / 24
    ))

    rentDaysTarget().innerText = days;
    subTotalTarget().innerText = days * dailyRate();

    let subs = document.querySelectorAll(`span.subTotal`);
    let total = 0;
    subs.forEach((x, idx) => {
        total += parseInt(subs[idx].innerText);
    });
    document.querySelector('span.total').innerText = total;
});
//checkbox勾選事件
checkBoxes.forEach(function (ckBox) {
    ckBox.addEventListener('change', function () {
        let i = ckBox.value;
        let PostTarget = document.querySelector(`input[name="ListChecked[${i}]"]`);
        PostTarget.value = ckBox.checked;
    });
})
//function Checked() {
//    let result = "";
//    document.querySelectorAll('.form-check-input').forEach((x, idx) => function () {
//        if (x.checked == true) {
//            result += x.value + ",";
//        }
//    })

//    let b = document.querySelector('.check');
//    b.setAttribute("value", result);
//}


//function Date() {
//    let start = "";
//    document.querySelectorAll('input[name="StartDate"]').forEach(function (x) {

//            start += startDateTimeText + ",";

//    })

//    let a = document.querySelector(".start");
//    a.setAttribute("value", start);
//}

//document.querySelectorAll('input[name="StartDate"]').forEach(function (btn) {
//    btn.addEventListener('change', Date);

//})