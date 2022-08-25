const btnTop = document.querySelector('.top-btn');
//SCROOL TOP
window.addEventListener('scroll', () => {
    if (window.scrollY > 50) {
        btnTop.classList.add('active');
    } else {
        btnTop.classList.remove('active');
    }
})

btnTop.addEventListener('click', () => {
    window.scrollTo(0, 0);
});

//SWIPER BOOKS
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
    },
});

window.addEventListener("load", function(){
    document.querySelector(".spinner").style.display = "none";
});
