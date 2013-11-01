$(document).ready(function () {
    localStorage.clear();

    $(".logueo").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#loginButton").click();
        }
    });

    $('#loginButton').click(function () {
        var u = $("#usuario").val();
        var p = $("#contrasenia").val();
        var jsonLogin = {
            user: u,
            password: p
        };
        localStorage.setItem("usuario", u);


        $.ajax({
            type: "POST",
            data: JSON.stringify(jsonLogin),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "/Usuario/LoginResult",
            success: function (data) {
                if (data == null) {
                    $("#control").html("Usuario o Clave incorrecto");
                    $("#Usuario").val("");
                    $("#Contrasenia").val("");
                    $("#Usuario").focus();
                } else {

                    var idUsuario = data.ID;
                    var token = data.estado;
                    var perfiles = data.listadeperfil;
                    localStorage.setItem("perfiles", perfiles);

                    if (idUsuario)
                        localStorage.setItem("idUsuario", idUsuario);
                    else
                        localStorage.setItem("idUsuario", "Vale loguearse");

                    if (token)
                        localStorage.setItem("token", token);
                    else
                        localStorage.setItem("token", "00000000000000000000");

                    var nombre = data.nombres + ' ' + data.apPat + ' ' + data.apMat;
                    localStorage.setItem("nombre", nombre);

                    var url = "/Home/Index";
                    window.location.href = url;
                }
            }, error: function () {
                $("#control").html("Usuario o Clave incorrecto");
            }
        });
    });
});