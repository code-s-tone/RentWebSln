
//彈出Modal才指定
//DateModalLauncher
//StartDateToPost
//ExpirationDateToPost
//disablePeriodsJSON




//dateTimePicker本身設定
//設定日期完成
completeBtn.addEventListener('click', function () {
    //console.log(disablePeriodsJSON);
    startDateTimeText = combinDateTime(0);
    endDateTimeText = combinDateTime(1);

    //若大小關係不對就交換    

    //顯示文字在指定的物件上
    showPeriodText(startDateTimeText, endDateTimeText);
    //改變傳遞表單值
    StartDateToPost.value = startDateTimeText;
    ExpirationDateToPost.value = endDateTimeText;
});


