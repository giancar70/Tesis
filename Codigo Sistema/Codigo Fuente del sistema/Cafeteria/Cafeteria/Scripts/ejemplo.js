$(document).ready(function () {
    var idUsuario = localStorage.getItem("idUsuario");
    var nombre = localStorage.getItem("nombre");

    $('#venta').click(function () {
       // alert(idUsuario);
        //var idUsuario = localStorage.getItem("idUsuario");
        var p = $("#nombreUsuario").val();
        var jsonventa = {
            user: idUsuario,
            pass: p
        };
        //alert(p);

        $.ajax({
            type: "POST",
            data: JSON.stringify(jsonventa),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "/Venta/ventasolicitudad",
            success: finllamada

        });

    });

    function finllamada(data) {
        console.log(data);
        var url = "/Venta/Create/"+data;
        window.location.href = url;
    }


});