const box = document.getElementById("studentSection");

function handleRadioClick() {
    if (document.getElementById('hide').checked) {
        box.style.display = 'none';
    }
    else {
        box.style.display = 'block'
    }
}

const radioButtons = document.querySelectorAll('input[name="section"]');
radioButtons.forEach(radio => {
    radio.addEventListener('click', handleRadioClick);
});
