var varLocation;
function showDelete(location) {
    document.getElementById("delete").style.display = "block";
    varLocation = location;
}

function hideDel() {
    document.getElementById("delete").style.display = "none";
}

function del() {
    document.getElementById("delform").setAttribute("action", varLocation);
}