const eye = document.querySelectorAll('.eye');

eye.forEach(e => {
    e.addEventListener('click', () => {
        const passwd = document.querySelector('.show-hide');

        if (e.nextElementSibling.type == 'password') {
            e.nextElementSibling.type = 'text';
            e.classList.remove('fa-eye-slash');
            e.classList.add('fa-eye')
        } else {
            e.nextElementSibling.type = 'password';
            e.classList.add('fa-eye-slash');
            e.classList.remove('fa-eye')
        }
    })
})