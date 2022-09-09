const header = document.querySelector('#header');
const leftBar = document.querySelector('.left-nav');
const btn = document.querySelector('.toggle');
const list = document.querySelectorAll('.list');
const rList = document.querySelector('.r-list');
const rDrop = document.querySelector('.r-drop');
const account = document.querySelector('#account');
const btnAcnt = document.querySelector('.profile');
const accountDrop = document.querySelector('.account-drop');

btn.addEventListener('click', () => {
    header.classList.toggle('pad');
    leftBar.classList.toggle('show');
    account.classList.toggle('pad')
});

list.forEach(link => {
    link.addEventListener('click', () => {
        link.nextElementSibling.classList.toggle('height')
    })
})

rList.addEventListener('click', () => {
    rDrop.classList.toggle('height')
})

btnAcnt.addEventListener('click', () => {
    accountDrop.classList.toggle('opacity')
})

const btnTop = document.querySelector('.top-btn');
window.addEventListener('scroll', () => {
    if (window.scrollY > 50) {
        btnTop.classList.add('active')
    } else {
        btnTop.classList.remove('active')
    }
});

btnTop.addEventListener('click', () => {
    window.scrollTo(0, 0);
});