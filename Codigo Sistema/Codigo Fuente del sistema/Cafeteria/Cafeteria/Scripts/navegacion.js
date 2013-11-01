$("#logout").click(function () {
    localStorage.clear();
});

$(document).ready(function () {
    var token = localStorage.getItem("token");
    var perfil = localStorage.getItem("perfiles");
    // Setear nombre arriba del layout
    var nombre = localStorage.getItem("nombre");
    $("#nombreUsuario").html("Bienvenido " + nombre + "!");

    var idUsuario = localStorage.getItem("idUsuario");
    if (idUsuario != null) {
        // Crear link para redirigir a tu detalle de usuario
        $("#verUsuario").attr("href", "/Usuario/Details/" + idUsuario);
        // Crear el link para el logout
        $("#logout").attr("href", "/Logout/Index/" + idUsuario);
    }

    //Setear menus que puedes ver según tu perfil
    if (token == null) {
        alert("No tienes permiso de entrar aquí");
        arma_menu("000000000000000000000000");
    } else if (token == "") {
        arma_menu("000000000000000000000000");
    } else {
        arma_menu(token);
    }

    function arma_menu(token) {
        var t = localStorage.getItem("token");


        if (perfil == '1') {  //recepcionista

            

            var menu = $('#menu11');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu12');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu13');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu21');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu22');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu23');
            $(menu).attr("style", "display:none;");
            

            var menu = $('#menu5');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu6');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu7');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu24');
            $(menu).attr("style", "display:none;");
        }
        else if (perfil == "2") { //almacen
            var menu = $('#menu1');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu21');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu22');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu23');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu24');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu3');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu4');
            $(menu).attr("style", "display:none;");


            var menu = $('#menu24');
            $(menu).attr("style", "display:none;");
            
            var menu = $('#menu72');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu73');
            $(menu).attr("style", "display:none;");
            
            var menu = $('#menu71');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu70');
            $(menu).attr("style", "display:none;");

        }
        else if (perfil == "3") { //logistica
            var menu = $('#menu1');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu24');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu3');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu4');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu5');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu6');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu71');
            $(menu).attr("style", "display:none;");

            var menu = $('#menu72');
            $(menu).attr("style", "display:none;");

           

            var menu = $('#menu74');
            $(menu).attr("style", "display:none;");
            
        }
        else if (perfil == "0" || perfil == "4") {
            var menu = $('#menu73');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu14');
            $(menu).attr("style", "display:none;");
            var menu = $('#menu70');
            $(menu).attr("style", "display:none;");
            
        }

    }


});