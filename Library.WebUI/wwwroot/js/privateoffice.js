const header = document.querySelector('#header');
const leftBar = document.querySelector('.left-nav');
const btn = document.querySelector('.toggle');
const list = document.querySelectorAll('.list');
const rList = document.querySelector('.r-list');
const rDrop = document.querySelector('.r-drop');
const account = document.querySelector('#account');


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

