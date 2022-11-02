const modalBackdrop = document.querySelector('.modalbackdrop');
const loginModal = document.querySelector('.modal-bg');
const modalBtn = document.querySelector('.modal-btn');
const closeBtn = document.querySelector('.closed');

const openModal = () =>{
    loginModal.classList.add('flex');
    modalBackdrop.classList.add('block');
    document.body.classList.add('hide');
}

const closeModal = ()=>{
    loginModal.classList.remove('flex');
    modalBackdrop.classList.remove('block');
    document.body.classList.remove('hide');
}

modalBtn.addEventListener('click',()=>{
    openModal()
});

closeBtn.addEventListener('click',()=>{
    closeModal()
})




