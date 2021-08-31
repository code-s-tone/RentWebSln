//輔助函式庫
function combinDateTime(i) {
    return flatpickr.formatDate(datePicker.selectedDates[i], datePicker.config.dateFormat) +
        formatDivider + timePicker[i].input.value;
}

//顯示期間文字 給使用者看
function showPeriodText( startDateTimeText, endDateTimeText) {
    DateModalLauncher.classList.add('setted');
    DateModalLauncher.innerHTML = `<div>${startDateTimeText}</div>~<div>${endDateTimeText}</div>`;
}



//dateTimePicker本身設定
let collapseBtn = document.querySelector('button[data-bs-target=".collapseItem"]');
let completeBtn = document.querySelector('#complete');

collapseBtn.disabled = true;
completeBtn.disabled = true;

collapseBtn.addEventListener('click', function () {
    if (collapseBtn.classList.contains('collapsed')) {
        collapseBtn.innerText = "繼續設定時間";
    }
    else {
        collapseBtn.innerText = "返回設定日期";
    }
});

const datePicker = flatpickr("#datePicker", {
    mode: "range",
    dateFormat: "Y / m / d",
    //disable: [
    //    {
    //        from: "2021 / 10 / 04",
    //        to: "2021 / 10 / 06"
    //    },
    //    {
    //        from: "2021 / 10 / 08",
    //        to: "2021 / 10 / 09"
    //    }
    //],

    minDate: new Date(),

    inline: true,

    onChange: function (selectedDates, dateStr, instance) {//固定的參數群
        if (selectedDates.length == 2 && selectedDates[1] >= selectedDates[0]) {
            let s = flatpickr.formatDate(selectedDates[0], instance.config.dateFormat);
            let e = flatpickr.formatDate(selectedDates[1], instance.config.dateFormat);

            document.querySelector("#start").innerHTML = s;
            document.querySelector("#end").innerHTML = e;
            //日期設定好> 才可以按collapseBtn
            //啟用
            collapseBtn.disabled = false;
            if (timePicker[0].selectedDates.length == 1 &&
                timePicker[1].selectedDates.length == 1
            ){
                //日期時間都設定好> 才可以按completeBtn
                completeBtn.disabled = false;
            }

        } else {
            //禁止
            collapseBtn.disabled = true;
            completeBtn.disabled = true;
        }
    }
});

const timePicker = flatpickr(".timePicker", {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    minTime: "08:00",
    maxTime: "22:30",

    onChange: function (selectedDates, dateStr, instance) {//固定的參數群
        if (timePicker[0].selectedDates.length == 1 &&
            timePicker[1].selectedDates.length == 1 &&
            datePicker.selectedDates.length == 2 &&
            datePicker.selectedDates[1] > datePicker.selectedDates[0]) {
            //日期時間都設定好> 才可以按completeBtn
            completeBtn.disabled = false;
        } else {
            completeBtn.disabled = true;
        }
    }
});