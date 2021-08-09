
let DateModalLauncher = document.querySelector('button[data-bs-target="#DateModal"]');
let TimeModalLauncher = document.querySelector('button[data-bs-target="#TimeModal"]');
let completeBtn = document.querySelector('#complete');

DateModalLauncher.addEventListener('click', function () {
    let sD = datePicker.selectedDates;
    if (sD.length == 2 && sD[1] > sD[0]) {
        TimeModalLauncher.disabled = false;
    } else {
        TimeModalLauncher.disabled = true;
    }
});

TimeModalLauncher.addEventListener('click', function () {
    if (TimePicker[0].selectedDates.length == 1 && TimePicker[1].selectedDates.length == 1) {
        completeBtn.disabled = false;
    } else {
        completeBtn.disabled = true;
    }
});

const datePicker = flatpickr("#datePicker", {
    mode: "range",
    dateFormat: "Y\\年m\\月d\\日",
    // altInput: true,
    // altFormat: "F j, Y",

    minDate: new Date(),
    disable: ["2021-08-11", new Date(2021, 7, 19)],

    inline: true,


    onChange: function (selectedDates, dateStr, instance) {//固定的參數群
        if (selectedDates.length == 2 && selectedDates[1] > selectedDates[0]) {
            TimeModalLauncher.disabled = false;
        } else {
            TimeModalLauncher.disabled = true;
        }
        let s = flatpickr.formatDate(selectedDates[0], instance.config.dateFormat);
        let e = flatpickr.formatDate(selectedDates[1], instance.config.dateFormat);

        document.querySelector("#start").innerHTML = "從：" + s;
        document.querySelector("#end").innerHTML = "到：" + e;
    },
});



const TimePicker = flatpickr(".TimePicker", {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    minTime: "08:00",
    maxTime: "22:30",

    onChange: function (selectedDates, dateStr, instance) {//固定的參數群
        if (TimePicker[0].selectedDates.length == 1 && TimePicker[1].selectedDates.length == 1) {
            completeBtn.disabled = false;
        } else {
            completeBtn.disabled = true;
        }
    }
});


    // const startTimePicker = flatpickr("#startTimePicker", {
    // });

    // const endTimePicker = flatpickr("#endTimePicker", {
    // });

completeBtn.addEventListener('click', function () {
    let DateModalEl = document.getElementById('DateModal');
    let DateModal = bootstrap.Modal.getInstance(DateModalEl);
    DateModal.toggle();
    let a = document.querySelectorAll('.modal-backdrop.fade.show');
    a.forEach(x => {
        console.log(x);
        x.classList.remove('show');
    });
});