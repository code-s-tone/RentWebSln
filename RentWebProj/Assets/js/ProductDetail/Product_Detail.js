let x1 = 0;
let x2 = 0;

let span1 = document.getElementById("span1");
let link1 = document.getElementById("link1");
let collapse_text1 = document.getElementById("collapse_text1");
link1.addEventListener('click', function () {
    span1.classList.add('hide');
    collapse_text1.setAttribute('class', 'collapse.show');
});

let link2 = document.getElementById("link2");
let collapse_textTwo = document.getElementById("collapse_textTwo");
link2.addEventListener('click', function () {
    link2.classList.add('hide');
    x1 = 1;
    arrow1.classList.remove('rotate');
    // collapse_textTwo.setAttribute('class', 'collapse.show');
});


let link4 = document.getElementById("link4");
let collapse_text4 = document.getElementById("collapse_text4");
link4.addEventListener('click', function () {
    link4.classList.add('hide');

    x2 = 1;
    arrow2.classList.remove('rotate');
    // collapse_text4.setAttribute('class', 'collapse.show');

});

let arrow1 = document.getElementById("arrow1");
arrow1.addEventListener('click', function () {
    if (x1 == 0) {
        arrow1.classList.remove('rotate');
        link2.classList.add('hide');
        x1 = 1;
    }

    else if (x1 == 1) {
        arrow1.classList.add('rotate');
        setTimeout(function () { link2.classList.remove('hide') }, 200);
        x1 = 0;
    }
});

let arrow2 = document.getElementById("arrow2");
arrow2.addEventListener('click', function () {
    if (x2 == 0) {
        arrow2.classList.remove('rotate');
        link4.classList.add('hide');
        x2 = 1;
    }
    else if (x2 == 1) {
        arrow2.classList.add('rotate');
        setTimeout(function () { link4.classList.remove('hide') }, 300);
        x2 = 0;
    }
});
