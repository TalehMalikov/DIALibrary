const en = document.querySelector('.en');
const az = document.querySelector('.az');
const h2 = document.querySelector('h2');
const user = document.querySelector('.user');
const pass =document.querySelector('.pass');
const btn = document.querySelector('.btn');
const lang = document.querySelector('.lang');

const changeEn = () =>{
    h2.innerHTML = "Member Login";
    user.setAttribute('placeholder','Username');
    pass.setAttribute('placeholder','Password');
    en.classList.add('outline');
    az.classList.remove('outline');
    btn.innerHTML = "Log In";
    document.querySelector('span').innerHTML = "EN";
}

const changeAz = () =>{
    h2.innerHTML = "İstifadeçi Girişi";
    user.setAttribute('placeholder','İstıfadeçi Adı');
    pass.setAttribute('placeholder','Şifreniz');
    az.classList.add('outline');
    en.classList.remove('outline');
    btn.innerHTML = 'Daxil Ol';
    document.querySelector('span').innerHTML = "AZ";
}
        

lang.addEventListener('click',function(){
    document.querySelector('ul').classList.toggle('show');
    if(document.querySelector('span').innerHTML == "AZ"){
        az.style.background = '#000';
        az.style.color="#fff";
        en.style.background = "#fff";
        en.style.color = "#000"
    }else{
        en.style.background = "#000";
        en.style.color = '#fff';
        az.style.background = "#fff";
        az.style.color = '#000'
    }
});


en.addEventListener('click',changeEn);
az.addEventListener('click',changeAz);

