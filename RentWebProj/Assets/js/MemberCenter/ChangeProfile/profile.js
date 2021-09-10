

//編輯
let personEditBtn = document.querySelector('.memberperson-button-edit');
let emailEditBtn = document.querySelector('.memberemail-button-edit');
let passwordEditBtn = document.querySelector('.memberpassword-button-edit');
//取消編輯
let personCancelEditBtn = document.querySelector('.memberperson-cancel-edit');
let emailCancelEditBtn = document.querySelector('.memberemail-cancel-edit');
let passwordCancelEditBtn = document.querySelector('.memberpassword-cancel-edit');

//儲存
let personSaveBtn = document.querySelector('.memberperson-button-save');
let emailSaveBtn = document.querySelector('.memberemail-button-save');
let passwordSaveBtn = document.querySelector('.memberpassword-button-save');

//姓氏 //生日 //電話
let personNameInp = document.querySelectorAll('.member-person-input');

//生日value
let changeYearContent = document.querySelector('.member-display-year');
let changeMonthContext = document.querySelector('.member-display-month');
let changeDateContext = document.querySelector('.member-display-date');

//性別 暫

//門市  暫

//信箱
let changeEmailDisplay = document.querySelectorAll('.member-changeEmail-item');
let emailClear = document.querySelectorAll('.member-email-clear');
let newEmail = document.querySelector('.member-display-new-email');
let doubleNewEmail = document.querySelector('.member-display-doublecheck-email');
let emailDanger = document.querySelector('.email-danger');
let changeEmailDanger = document.querySelector('.checkemail-danger');


//密碼
let changePasswordDisplay = document.querySelectorAll('.member-changePassword-item');
let passwordClear = document.querySelectorAll('.member-password-clear');
let newPassword = document.querySelector('.member-display-new-password');
let doubleNewPassword = document.querySelector('.member-display-doublecheck-password');



//網頁載入初始化
window.onload = function () {
    personCancelEditBtn.classList.add('notDisplay');
    emailCancelEditBtn.classList.add('notDisplay');
    passwordCancelEditBtn.classList.add('notDisplay');
    personSaveBtn.disabled = true;
    personSaveBtn.classList.add('buttonDisabled');
    emailSaveBtn.disabled = true;
    emailSaveBtn.classList.add('buttonDisabled');
    passwordSaveBtn.disabled = true;
    passwordSaveBtn.classList.add('buttonDisabled');
    inputInit();
    AccountInit();
    PasswordInit();

};

//初始化表格狀態
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
});

//取消個資修改
personCancelEditBtn.addEventListener('click', function () {
    personEditBtn.classList.remove('notDisplay');
    personCancelEditBtn.classList.add('notDisplay');
    personSaveBtn.disabled = true;
    personSaveBtn.classList.add('buttonDisabled');
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
    swal("修改成功", '自動跳轉..', 'success');
});


//信箱啟動修改
emailEditBtn.addEventListener('click', function () {
    emailCancelEditBtn.classList.remove('notDisplay');
    emailEditBtn.classList.add('notDisplay');
    //emailSaveBtn.disabled = false;
    //emailSaveBtn.classList.remove('buttonDisabled');

    AccountDisplay();
    EmailClear();
});

//信箱正規表達式
let regx = /\S+@\S+.\S+/;
function checkEmail() {
    if (newEmail.value != doubleNewEmail.value) {
        emailSaveBtn.disabled = true;
        emailSaveBtn.classList.add('buttonDisabled');
    } else if (regx.test(doubleNewEmail.value) === false) {
        emailSaveBtn.disabled = true;
        emailSaveBtn.classList.add('buttonDisabled');
    }
    else {
        emailSaveBtn.disabled = false;
        emailSaveBtn.classList.remove('buttonDisabled');
    }
}



newEmail.addEventListener('keyup', checkEmail);
doubleNewEmail.addEventListener('keyup', checkEmail);
//emailDanger.addEventListener('blur', notedEmail);
//changeEmailDanger.addEventListener('keyup', notedEmail);



//取消信箱修改
emailCancelEditBtn.addEventListener('click', function () {
    emailCancelEditBtn.classList.add('notDisplay');
    emailEditBtn.classList.remove('notDisplay');
    emailSaveBtn.disabled = true;
    emailSaveBtn.classList.add('buttonDisabled');
    AccountInit();
});
//信箱變更送出
emailSaveBtn.addEventListener('click', function () {
    emailEditBtn.disabled = false;
    emailEditBtn.classList.remove('buttonDisabled');
    swal("修改成功", '自動跳轉..', 'success');
});


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
    } else if (newPassword.value === doubleNewPassword.value) {
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