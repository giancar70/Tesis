$(document).ready(function () {
    var idUsuario = localStorage.getItem("idUsuario");
    var nombre = localStorage.getItem("nombre");

    $('#menu25').click(function () {
        // alert(idUsuario);
        //var idUsuario = localStorage.getItem("idUsuario");
        var p = $("#nombreUsuario").val();
        var jsonventa = {
            user: idUsuario,
            pass: p
        };
        finllamada();
    });

    function finllamada() {
        var url = "/Usuario/verhorario2/" + idUsuario;
        window.location.href = url;
    }


});