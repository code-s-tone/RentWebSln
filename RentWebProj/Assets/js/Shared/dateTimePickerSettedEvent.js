
//彈出Modal才指定
//DateModalLauncher
//StartDateToPost
//ExpirationDateToPost
//disablePeriodsJSON

//禁用日期
datePicker.config.disable = disablePeriodsJSON;

let startDateTimeText;
let endDateTimeText;
let formatDivider = ' ';
let dateTimeFormat = datePicker.config.dateFormat + formatDivider + timePicker[0].config.dateFormat;


//dateTimePicker本身設定
//設定日期完成
completeBtn.addEventListener('click', function () {
    startDateTimeText = combinDateTime(0);
    endDateTimeText = combinDateTime(1);
    //顯示文字在指定的物件上
    showPeriodText(DateModalLauncher, startDateTimeText, endDateTimeText);
    //改變傳遞表單值
    StartDateToPost.value = startDateTimeText;
    ExpirationDateToPost.value = endDateTimeText;
});


//輔助函式庫
function combinDateTime(i) {
    return flatpickr.formatDate(datePicker.selectedDates[i], datePicker.config.dateFormat) +
        formatDivider + timePicker[i].input.value;
}

//顯示期間文字 給使用者看
function showPeriodText(dateModalLauncher ,startDateTimeText, endDateTimeText ) {
    dateModalLauncher.classList.add('setted');
    dateModalLauncher.innerHTML = `<div>${startDateTimeText}</div>~<div>${endDateTimeText}</div>`;
}
