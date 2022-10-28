let eKataloqBtn = document.getElementsByClassName("eKataloqBtn")[0];
let submenu = document.getElementById("submenu");
let dropdownMenuLink = document.getElementsByClassName("kitabxanaBtn")[0];
let dropBox = document.getElementById("drop-box");

eKataloqBtn.addEventListener("click", (e) => {
    document.getElementById("drop-box").style.display = "block"
    submenu.style.display == "block" ? submenu.style.display = "none" : submenu.style.display = "block"
})
dropdownMenuLink.addEventListener("click", (e) => {
    dropBox.style.display == "block" ? dropBox.style.display = "none" : dropBox.style.display = "block"
})