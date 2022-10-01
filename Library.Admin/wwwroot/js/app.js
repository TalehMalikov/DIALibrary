const btn = document.querySelector(".one");
const btn2 = document.querySelector(".two");
const textarea = document.querySelector('.activities');
let i = 1;
const addTextArea = () => {
    ++i;
    let tag = document.createElement('textarea');
    tag.setAttribute('class', "form-control my-2");
    tag.setAttribute('id', "contentNews");
    tag.setAttribute('rows', "2");
    tag.setAttribute('placeholder', "Abzas " + i);
    textarea.appendChild(tag);
}

const removeTextArea = () => {
    textarea.lastChild.remove();
    i--;
}

btn.addEventListener('click', () => {
    addTextArea()
})

btn2.addEventListener('click', () => {
    removeTextArea()
})