function SetCulture(selectedValue) {
    var url = window.location.href.split('?')[0];
    var culture = "?culture=" + selectedValue + "&ui-culture=" + selectedValue;
    window.location.href = url + culture;
}