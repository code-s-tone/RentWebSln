let DateModalLauncher = document.querySelector('button[data-bs-target="#DateModal"]');
let collapseBtn = document.querySelector('button[data-bs-target=".collapseItem"]');
let completeBtn = document.querySelector('#complete');
let actionBtns = document.querySelectorAll('.decision-group .orange');

collapseBtn.disabled = true;
completeBtn.disabled = true;
actionBtns.forEach(x=> x.disabled = true);

collapseBtn.addEventListener('click',function(){
    if(  collapseBtn.classList.contains('collapsed')  ){
        collapseBtn.innerText="繼續設定時間";
    }
    else{
        collapseBtn.innerText="返回設定日期";
    }
});

const datePicker = flatpickr("#datePicker", {
    mode: "range",
    dateFormat: "Y / m / d",
    // altInput: true,
    // altFormat: "F j, Y",

    minDate: new Date(),
    disable: ["2021-08-11", new Date(2021, 7, 19) ],

    inline:true,

    onChange: function( selectedDates, dateStr, instance) {//固定的參數群
        if (selectedDates.length == 2 && selectedDates[1]>selectedDates[0]){
            let s = flatpickr.formatDate(selectedDates[0], instance.config.dateFormat);
            let e = flatpickr.formatDate(selectedDates[1], instance.config.dateFormat);

            document.querySelector("#start").innerHTML= s;
            document.querySelector("#end").innerHTML= e;
        //日期設定好> 才可以按collapseBtn
            //啟用
            collapseBtn.disabled = false;
            if (timePicker[0].selectedDates.length==1 
                && timePicker[1].selectedDates.length==1
            ){
                //日期時間都設定好> 才可以按completeBtn
                completeBtn.disabled =  false;
            }

        }else{
            //禁止
            collapseBtn.disabled = true;
            completeBtn.disabled =  true;
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

    onChange: function( selectedDates, dateStr, instance) {//固定的參數群
        if (timePicker[0].selectedDates.length==1 
            && timePicker[1].selectedDates.length==1
            && datePicker.selectedDates.length==2 
            && datePicker.selectedDates[1]>datePicker.selectedDates[0]){        
            //日期時間都設定好> 才可以按completeBtn
            completeBtn.disabled =  false;
        }else{
            completeBtn.disabled =  true;
        }
    }
});
// const startTimePicker = flatpickr("#startTimePicker", {
// });
// const endTimePicker = flatpickr("#endTimePicker", {
// });

let startDateTimeStr;
let endDateTimeStr;
let divider = '   ';

completeBtn.addEventListener('click',function(){
    startDateTimeStr =  combinDateTime(0);
    endDateTimeStr =  combinDateTime(1);

    DateModalLauncher.classList.add('setted');
    DateModalLauncher.innerHTML = `<div>${startDateTimeStr}</div>~<div>${endDateTimeStr}</div>`;

    actionBtns.forEach(x=>x.disabled = false);
    

});

function combinDateTime(i){
    return flatpickr.formatDate(datePicker.selectedDates[i], datePicker.config.dateFormat)+
    divider + timePicker[i].input.value;
}




