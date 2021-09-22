
//宣告信箱變數
let changeEmailDisplay = document.querySelectorAll('.member-changeEmail-item');
let emailClear = document.querySelectorAll('.member-email-clear');
let currentEmail = document.querySelector('.member-display-service-email');
let newEmail = document.querySelector('.member-display-new-email');
let doubleNewEmail = document.querySelector('.member-display-doublecheck-email');
let emailDanger = document.querySelector('.email-danger');
let changeEmailDanger = document.querySelector('.checkemail-danger');

//宣告密碼變數
let changePasswordDisplay = document.querySelectorAll('.member-changePassword-item');
let passwordClear = document.querySelectorAll('.member-password-clear');
let newPassword = document.querySelector('.member-display-new-password');
let doubleNewPassword = document.querySelector('.member-display-doublecheck-password');
let passwordDanger = document.querySelector('password-danger');
let changePasswordDanger = document.querySelector('.check-password-danger');


//編輯
let personEditBtn = document.querySelector('.memberperson-button-edit');
let emailEditBtn = document.querySelector('.memberemail-button-edit');
let passwordEditBtn = document.querySelector('.memberpassword-button-edit');
//取消編輯
let personCancelEditBtn = document.querySelector('.memberperson-cancel-edit');
personCancelEditBtn.classList.add('notDisplay');
let emailCancelEditBtn = document.querySelector('.memberemail-cancel-edit');
emailCancelEditBtn.classList.add('notDisplay');
let passwordCancelEditBtn = document.querySelector('.memberpassword-cancel-edit');
passwordCancelEditBtn.classList.add('notDisplay');

//儲存
let personSaveBtn = document.querySelector('.memberperson-button-save');
personSaveBtn.disabled = true;
personSaveBtn.classList.add('buttonDisabled');
let emailSaveBtn = document.querySelector('.memberemail-button-save');
emailSaveBtn.disabled = true;
emailSaveBtn.classList.add('buttonDisabled');
let passwordSaveBtn = document.querySelector('.memberpassword-button-save');
passwordSaveBtn.disabled = true;
passwordSaveBtn.classList.add('buttonDisabled');

AccountInit();
PasswordInit();

//個人資訊
//member - display - lastname
let personFullName = document.querySelector('.member-display-lastname');
let nameErrorMsg = document.querySelector('.name-error-msg');

//member - display - year
let personYear = document.querySelector('.member-display-year');
let yearErrorMsg = document.querySelector('.year-error-msg');

//member - display - month
let personMonth = document.querySelector('.member-display-month');
let monthErrorMsg = document.querySelector('.month-error-msg');

//member - display - date
let personDate = document.querySelector('.member-display-date');
let dayErrorMsg = document.querySelector('.day-error-msg');

//member - display - phone
let personPhone = document.querySelector('.member-display-num');
let phoneErrorMsg = document.querySelector('.phone-error-msg');




//取得個資以API回傳資料庫
personSaveBtn.addEventListener('click', function () {
    let fullName_value = personFullName.value;
    let personYear_value = personYear.value;
    let personMonth_value = personMonth.value;
    let personDate_value = personDate.value;
    let personPhone_value = personPhone.value;

    let data = {
        MemberName: fullName_value,
        MemberYear: personYear_value,
        MemberMonth: personMonth_value,
        MemberDay: personDate_value,
        MemberPhone: personPhone_value
    };

    changeProfileApi(data);
});

const Urlprofile = "/api/MemberProfileAPI/ChangeProfile";
function changeProfileApi(data) {
    fetch(Urlprofile,
        {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(res => {
            return res.json();
        }).then(res => {
            console.log(res);
            swal("修改成功", '', 'success');
        });
}


//取得個資信箱以API回傳資料庫
emailSaveBtn.addEventListener('click', function () {
    let doubleNewEmail_value = doubleNewEmail.value;

    let dataEmail = {
        ComfirMemberEmail: doubleNewEmail_value
    };


    emailEditBtn.classList.remove('notDisplay');
    emailCancelEditBtn.classList.add('notDisplay');
    emailSaveBtn.disabled = true;
    emailSaveBtn.classList.add('buttonDisabled');
    AccountInit();
    changeEmailApi(dataEmail, currentEmail);
});

const Urlemail = "/api/MemberEmailAPI/ChangeUserEmail";
function changeEmailApi(data, currentEmail) {
    fetch(Urlemail,
        {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(response => {
            return response.json();
        }).then(response => {
            console.log(response);
            swal("修改成功", '', 'success');
            currentEmail.innerText = response.Result;
            
    
        });
}




let regNum = /^[0-9]*$/;
//let regPhone = /(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/
let regPhone = /^[09]{2}[0-9]{8}$/

let IsCanNotOpenSaveButton = () => {
    personSaveBtn.disabled = true;
    personSaveBtn.classList.add('buttonDisabled');
}

const IsCanOpenSaveButton = () => {
    personSaveBtn.disabled = false;
    personSaveBtn.classList.remove('buttonDisabled');
}


let validateForm = () => {
    let fullName = true;
    let year = true;
    let month = true;
    let day = true;
    let phone = true;
    if (personFullName.value === "") {
        nameErrorMsg.textContent = "姓名不得為空";
        IsCanNotOpenSaveButton();
        fullName = false;
    }
    else if (personFullName.value != ""){
        nameErrorMsg.textContent = '';
        fullName = true;
    }

    if (personYear.value === "") {
        yearErrorMsg.textContent = "年份不得為空";
        IsCanNotOpenSaveButton();
        year = false;

    }
    else if (personYear.value.length != 4) {
        yearErrorMsg.textContent = "年份必須為4位數";
        IsCanNotOpenSaveButton();
        year = false;
    }

    else if (regNum.test(personYear.value) === false) {
        yearErrorMsg.textContent = "年份內容不合規定";
        IsCanNotOpenSaveButton();
        year = false;
    }

    else if (regNum.test(personYear.value) === true) {
        yearErrorMsg.textContent = "";
        year = true;
        
    }

    if (personMonth.value === "") {
        monthErrorMsg.textContent = "月份不得為空";
        IsCanNotOpenSaveButton();
        month = false;

        
    }
    else if (personMonth.value.length > 2 ) {
        monthErrorMsg.textContent = "月份必須為1~2位數";
        month = false;
        
    }
    else if (personMonth.value < 1 || personMonth.value > 12) {
        monthErrorMsg.textContent = "月份必須為合法月份";
        month = false;
        
    }
    else if (regNum.test(personMonth.value) === false) {
        monthErrorMsg.textContent = "月份內容不合規定";
        month = false;
        
    }
    else if (regNum.test(personMonth.value) === true) {
        monthErrorMsg.textContent = "";
        month = true;
    }

    if (personDate.value === "") {
        dayErrorMsg.textContent = "日期不得為空";
        day = false;
        
    }
    else if (personDate.value.length > 2) {
        dayErrorMsg.textContent = "日期必須為1~2位數";
        day = false;
        
    }
    else if (personDate.value < 1 || personDate.value > 31) {
        dayErrorMsg.textContent = "日期必須為合法日期";
        day = false;
        
    }
    else if (regNum.test(personDate.value) === false) {
        dayErrorMsg.textContent = "日期內容不合規定";
        day = false;
    }
    else if (regNum.test(personDate.value) === true) {
        dayErrorMsg.textContent = "";
        day = true;
    }

    if (personPhone.value === "") {
        phoneErrorMsg.textContent = "電話不得為空";
        IsCanNotOpenSaveButton();
        phone = false;
        

        
    }
    else if (regPhone.test(personPhone.value) === false) {
        phoneErrorMsg.textContent = "電話內容/長度不合規定";
        phone = false;
        
    }
    else if (regPhone.test(personPhone.value) === true) {
        phoneErrorMsg.textContent = "";
        phone = true;
    }

    if (fullName === true && year === true && month === true && day === true && phone === true) {
        IsCanOpenSaveButton();
    }
    else {
        IsCanNotOpenSaveButton();
    }
};


//個人資訊
//member - display - lastname
personFullName.addEventListener('keyup', validateForm);
//member - display - year
personYear.addEventListener('keyup', validateForm);
//member - display - month
personMonth.addEventListener('keyup', validateForm);
//member - display - date
personDate.addEventListener('keyup', validateForm);
//member - display - phone
personPhone.addEventListener('keyup', validateForm);





//性別 暫

//門市  暫




//個資初始化
let personNameInp = document.querySelectorAll('.member-person-input');
//個資初始化狀態
function inputInit() {
    for (let i = 0; i < personNameInp.length; i++) {
        personNameInp[i].classList.add('inputDisabled');
    }
}

//初始化變更信箱欄
function AccountInit() {
    for (let i = 0; i < changeEmailDisplay.length; i++) {
        changeEmailDisplay[i].classList.add('notDisplay');
    }
}
//顯示變更信箱
function AccountDisplay() {
    for (let i = 0; i < changeEmailDisplay.length; i++) {
        changeEmailDisplay[i].classList.remove('notDisplay');
    }
}

//顯示變更信箱清除
function EmailClear() {
    for (let i = 0; i < emailClear.length; i++) {
        emailClear[i].value = '';
    }
}

//初始化變更密碼欄
function PasswordInit() {
    for (let i = 0; i < changePasswordDisplay.length; i++) {
        changePasswordDisplay[i].classList.add('notDisplay');
    }
}
//顯示變更密碼
function PasswordDisplay() {
    for (let i = 0; i < changePasswordDisplay.length; i++) {
        changePasswordDisplay[i].classList.remove('notDisplay');
    }
}

//顯示變更密碼清除
function PasswordClear() {
    for (let i = 0; i < passwordClear.length; i++) {
        passwordClear[i].value = '';
    }
}




//個資啟動修改
personEditBtn.addEventListener('click', function () {
    for (let i = 0; i < personNameInp.length; i++) {
        personNameInp[i].classList.remove('inputDisabled');
    }
    //personEditBtn.disabled = true;
    personEditBtn.classList.add('notDisplay');
    personCancelEditBtn.classList.remove('notDisplay');
    personSaveBtn.disabled = false;
    personSaveBtn.classList.remove('buttonDisabled');
    validateForm();
});

//取消個資修改
personCancelEditBtn.addEventListener('click', function () {
    personEditBtn.classList.remove('notDisplay');
    personCancelEditBtn.classList.add('notDisplay');
    personSaveBtn.disabled = true;
    personSaveBtn.classList.add('buttonDisabled');
    nameErrorMsg.textContent = '';
    yearErrorMsg.textContent = '';
    monthErrorMsg.textContent = '';
    dayErrorMsg.textContent = '';
    phoneErrorMsg.textContent = '';
    inputInit();
});
//個資變更送出
personSaveBtn.addEventListener('click', function () {
    inputInit();
    personEditBtn.disabled = false;
    personEditBtn.classList.remove('notDisplay');
    personCancelEditBtn.classList.add('notDisplay');
    personEditBtn.classList.remove('buttonDisabled');
    personSaveBtn.classList.add('buttonDisabled');
});


//信箱啟動修改
emailEditBtn.addEventListener('click', function () {
    emailCancelEditBtn.classList.remove('notDisplay');
    emailEditBtn.classList.add('notDisplay');
    AccountDisplay();
    EmailClear();
});

//信箱正規表達式
let regx = /\S+@\S+.\S+/;
let regxx = /([a-zA-Z0-9]+)([\.{1}])?([a-zA-Z0-9]+)\@gmail([\.])com/;
function checkEmail() {
    if (newEmail.value != doubleNewEmail.value) {
        emailSaveBtn.disabled = true;
        emailSaveBtn.classList.add('buttonDisabled');
    } else if (regxx.test(doubleNewEmail.value) === false) {
        emailSaveBtn.disabled = true;
        emailSaveBtn.classList.add('buttonDisabled');
    }
    else {
        emailSaveBtn.disabled = false;
        emailSaveBtn.classList.remove('buttonDisabled');
    }
}

function notedEmail() {
    if (newEmail.value === "") {
        emailDanger.textContent = "電子信箱不得為空";
    }

    else if (regxx.test(newEmail.value) === false) {
        emailDanger.textContent = "電子信箱不符合規定";
    }
    else{
        emailDanger.textContent = "";
    }
}

function notedCheckEmail() {
    if (newEmail.value != doubleNewEmail.value) {
        changeEmailDanger.textContent = "與輸入信箱不符合";
    }
    else {
        changeEmailDanger.textContent = "";
    }
}

newEmail.addEventListener('keyup', checkEmail);
newEmail.addEventListener('keyup', notedEmail);
doubleNewEmail.addEventListener('keyup', checkEmail);
doubleNewEmail.addEventListener('keyup', notedCheckEmail);

//取消信箱修改
emailCancelEditBtn.addEventListener('click', function () {
    emailCancelEditBtn.classList.add('notDisplay');
    emailEditBtn.classList.remove('notDisplay');
    emailSaveBtn.disabled = true;
    emailSaveBtn.classList.add('buttonDisabled');
    emailDanger.textContent = '';
    changeEmailDanger.textContent = '';
    AccountInit();
});
////信箱變更送出
//emailSaveBtn.addEventListener('click', function () {
//    //變更欄位隱藏
    
//    emailEditBtn.classList.remove('notDisplay');
//    emailCancelEditBtn.classList.add('notDisplay');
//    emailSaveBtn.disabled = true;
//    emailSaveBtn.classList.add('buttonDisabled');
//    AccountInit();
//    swal("修改成功", '', 'success');
    
//});





//密碼啟動修改
passwordEditBtn.addEventListener('click', function () {
    passwordEditBtn.classList.add('notDisplay');
    passwordCancelEditBtn.classList.remove('notDisplay');
    //passwordSaveBtn.disabled = false;
    //passwordSaveBtn.classList.remove('buttonDisabled');
    PasswordDisplay();
    PasswordClear();
});


//密碼正規表達式
function checkPassword() {
    if (newPassword.value != doubleNewPassword.value) {
        passwordSaveBtn.disabled = true;
        passwordSaveBtn.classList.add('buttonDisabled');
    } else if (newPassword.value === doubleNewPassword.value && newPassword.value != "") {
        passwordSaveBtn.disabled = false;
        passwordSaveBtn.classList.remove('buttonDisabled');
    }
}

newPassword.addEventListener('keyup', checkPassword);
doubleNewPassword.addEventListener('keyup', checkPassword);



//取消密碼修改
passwordCancelEditBtn.addEventListener('click', function () {
    passwordCancelEditBtn.classList.add('notDisplay');
    passwordEditBtn.classList.remove('notDisplay');
    passwordSaveBtn.disabled = true;
    passwordSaveBtn.classList.add('buttonDisabled');
    PasswordInit();
});

//密碼變更送出
passwordSaveBtn.addEventListener('click', function () {
    PasswordInit();
    passwordCancelEditBtn.classList.add('notDisplay');
    passwordEditBtn.classList.remove('notDisplay');
    passwordSaveBtn.classList.add('buttonDisabled');
    swal("修改成功", '自動跳轉..', 'success');
});

