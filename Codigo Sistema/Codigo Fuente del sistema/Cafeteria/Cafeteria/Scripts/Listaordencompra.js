$(document).ready(function () {
    var idUsuario = localStorage.getItem("idUsuario");
    var nombre = localStorage.getItem("nombre");

    $('#menu73').click(function () {
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
        var url = "/Ordencompra/listadeordenes/" + data;
        window.location.href = url;
    }

    $('#menu74').click(function () {
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
            success: finllamada2

        });

        function finllamada2(data) {
            // alert(data);
            console.log(data);
            var url = "/Ordencompra/listadeordenes2/" + data;
            window.location.href = url;
        }

    });

    $('#menu70').click(function () {
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
            success: finllamada3

        });

        function finllamada3(data) {
            // alert(data);
            console.log(data);
            var url = "/Ordencompra/Registrar/" + data;
            window.location.href = url;
        }

    });




});