
//彈出Modal才指定
//DateModalLauncher
//StartDateToPost
//ExpirationDateToPost
//disablePeriodsJSON

let startDateTimeText;
let endDateTimeText;
let formatDivider = ' ';
let dateTimeFormat = datePicker.config.dateFormat + formatDivider + timePicker[0].config.dateFormat;



//dateTimePicker本身設定
//設定日期完成
completeBtn.addEventListener('click', function () {
    //console.log(disablePeriodsJSON);
    startDateTimeText = combinDateTime(0);
    endDateTimeText = combinDateTime(1);
    //顯示文字在指定的物件上
    showPeriodText(startDateTimeText, endDateTimeText);
    //改變傳遞表單值
    StartDateToPost.value = startDateTimeText;
    ExpirationDateToPost.value = endDateTimeText;
});


