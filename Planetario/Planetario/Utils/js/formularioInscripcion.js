main();

function main() {
    
}

function validarCorreo() {
    document.getElementById("formularioCorreo").style.display = "none";
    document.getElementById("formularioDatosPersonales").style.display = "contents";
}

function validarInfoPersonal() {
    document.getElementById("formularioDatosPersonales").style.display = "none";
    document.getElementById("formularioDatosPago").style.display = "contents";
}