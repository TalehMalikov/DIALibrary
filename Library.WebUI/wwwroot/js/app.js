const btnTop = document.querySelector('.top-btn');

//Kitab Swiper
const swiper = new Swiper('.swiper', {
    direction: 'horizontal',
    loop: true,

    breakpoints: {
        300: {
            slidesPerView: 2,
            spaceBetween: 40,
        },

        1024: {
            slidesPerView: 4,
            spaceBetween: 50,
        },
    },
    navigation: {
        nextEl: '.next-btn',
        prevEl: '.prev-btn',
    }
});

//SCROLL TOP
window.addEventListener('scroll',()=>{
    if(window.scrollY > 50){
        btnTop.classList.add('active')
    }else{
        btnTop.classList.remove('active')
    }
});

btnTop.addEventListener('click',()=>{
    window.scrollTo(0,0);
});

//Carousel Library
$('.owl-carousel').owlCarousel({
    responsive: {
        0: {
            items: 1
        },

        600: {
            items: 2
        },

        1200: {
            items: 4
        }
    },
    loop: true,
    margin: 10,
    autoplay: true,
    autoplayTimeout: 2000,
    autoplaySpeed: 2000,
    autoplayHoverPause: false,
});






