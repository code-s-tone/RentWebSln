const categoriesSwiper = new Swiper('.categories-swiper-container', {
    loop: true,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    spaceBetween: 20,
    //centeredSlides: true,
    slidesPerView: 1,
    breakpoints: {
        // when window width is >=
        600:{
            centeredSlides: true,
            slidesPerView: 1.8
        },
        768: {
            centeredSlides: true,
            slidesPerView: 2.2
        },
        992: {
            centeredSlides: false,
            slidesPerView: 2.6
        },
        1280: {
            centeredSlides: true,
            slidesPerView: 3.5
        }
    }
});

const storiesSwiper = new Swiper('.stories-swiper-container', {
    loop: true,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    spaceBetween: 12,
    slidesPerView: 2,
    breakpoints: {
        // when window width is >=
        768: {
            centeredSlides: true,
            slidesPerView: 2.4
        },
        992: {
            slidesPerView: 3
        },
        1280: {
            centeredSlides: false,
            slidesPerView: 4
        }
    }
});
const reviewsSwiper = new Swiper('.reviews-swiper-container', {
    loop: true,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    spaceBetween: 12,
    centeredSlides: true,
    slidesPerView: 1.3,  //原始大小?
    breakpoints: {
        // when window width is >=
        768: {
            centeredSlides: false,
            slidesPerView: 2
        },
        992: {
            centeredSlides: false,
            slidesPerView: 2
        },
        1280: {
            slidesPerView: 3
        }
    }
});
const blogPostsSwiper = new Swiper('.blogPosts-swiper-container', {
    loop: true,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    spaceBetween: 12,
    centeredSlides: true,
    slidesPerView: 1.2,
    breakpoints: {
        // when window width is >=
        992: {
            centeredSlides: true,
            slidesPerView: 1.6
        },
        1280: {
            centeredSlides: false,
            slidesPerView: 2
        }
    }
});
