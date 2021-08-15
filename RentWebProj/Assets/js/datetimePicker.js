let DateModalLauncher = document.querySelector('button[data-bs-target="#DateModal"]');
let collapseBtn = document.querySelector('button[data-bs-target=".collapseItem"]');
let completeBtn = document.querySelector('#complete');
collapseBtn.disabled = true;
completeBtn.disabled = true;

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
    dateFormat: "Y\\年m\\月d\\日",
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
            if (TimePicker[0].selectedDates.length==1 
                && TimePicker[1].selectedDates.length==1
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

const TimePicker = flatpickr(".TimePicker", {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    minTime: "08:00",
    maxTime: "22:30",

    onChange: function( selectedDates, dateStr, instance) {//固定的參數群
        if (TimePicker[0].selectedDates.length==1 
            && TimePicker[1].selectedDates.length==1
            && datePicker.selectedDates.length==2 
            && datePicker.selectedDates[1]>datePicker.selectedDates[0]
        ){
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
