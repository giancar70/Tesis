$(document).ready(function () { //$(document).ready(function(){});
    $('#idDepartamento').change(function () {
        var idDepartamentoSeleccionado = $(this).val();
        if (idDepartamentoSeleccionado != 0) {
            $.getJSON('/Cafeteria/getProvincias', { idDepartamento: idDepartamentoSeleccionado }, function (provincias) {//ver si le puedo cambiar de nombre a la funcion
                var cmbProvincia = $('#idProvincia');
                var cmbDistrito = $('#idDistrito');
                cmbProvincia.empty();
                cmbDistrito.empty();
                cmbProvincia.append($('<option/>').text('Seleccione una Provincia').attr('value', '').attr('selected', 'selected'));
                cmbDistrito.append($('<option/>').text('Seleccione una Provincia primero').attr('value', '').attr('selected', 'selected'));
                $.each(provincias, function (index, provincia) {
                    cmbProvincia.append(
                                $('<option/>')
                                    .attr('value', provincia.ID)
                                    .text(provincia.nombre)
                            );
                });
            });
        }
        else {
            var cmbProvincia = $('#idProvincia');
            var cmbDistrito = $('#idDistrito');
            cmbProvincia.empty();
            cmbDistrito.empty();
            cmbProvincia.append($('<option/>').text('Seleccione un Departamento primero').attr('value', '').attr('selected', 'selected'));
            cmbDistrito.append($('<option/>').text('Seleccione un Departamento primero').attr('value', '').attr('selected', 'selected'));
        }
        $.uniform.update("#idProvincia");
        $.uniform.update("#idDistrito");
    });
});

$(document).ready(function () {
    $('#idProvincia').change(function () {
        var idDepartamentoSeleccionado = $('#idDepartamento').val();
        var idProvinciaSeleccionado = $(this).val();
        if (idProvinciaSeleccionado != 0) {
            $.getJSON('/Cafeteria/getDistritos', { idProvincia: idProvinciaSeleccionado, idDepartamento: idDepartamentoSeleccionado }, function (distritos) {//ver si le puedo cambiar de nombre a la funcion
                var cmbDistrito = $('#idDistrito');
                cmbDistrito.empty();
                cmbDistrito.append($('<option/>').text('Seleccione un Distrito').attr('value', '').attr('selected', 'selected'));
                $.each(distritos, function (index, distrito) {
                    cmbDistrito.append(
                                $('<option/>')
                                    .attr('value', distrito.ID)
                                    .text(distrito.nombre)
                            );
                });
            });
        }
        else {
            var cmbDistrito = $('#idDistrito');
            cmbDistrito.empty();
            cmbDistrito.append($('<option/>').text('Seleccione una Provincia primero').attr('value', '').attr('selected', 'selected'));
        }
        $.uniform.update("#idDistrito");
    });
});