$(document).ready(function () {
    var idUsuario = localStorage.getItem("idUsuario");
    var nombre = localStorage.getItem("nombre");

    $('#menu14').click(function () {
        //alert(idUsuario);
        var p = $("#nombreUsuario").val();
        var jsonventa = {
            user: idUsuario,
            pass: p
        };
        $.ajax({
            type: "POST",
            data: JSON.stringify(jsonventa),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "/Ordencompra/listarordenes",
            success: finllamada

        });

    });

    function finllamada(data) {
        // alert(data);
        console.log(data);
        var url = "/Sucursal/Listaproductos/" + data;
        window.location.href = url;
    }

});