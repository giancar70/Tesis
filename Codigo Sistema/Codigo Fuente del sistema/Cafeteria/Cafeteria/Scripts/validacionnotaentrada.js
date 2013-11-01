var validacionIrvin;
var iniciandoTodosLosProcesos = $(document);
iniciandoTodosLosProcesos.ready(iniciarValidaciones);

function iniciarValidaciones() {
    //Validacion Irvin :(
    $("#cantidadChit").hide();

    var cantidadIrvin = $("#cantidadChit").text();
    //alert(cantidadIrvin);
    var arreglosId = new Array();

    for (validacionIrvin = 0; validacionIrvin < cantidadIrvin; validacionIrvin++) {

        var idCantidadDeIrvin = '#detalleNotaEntrada_' + validacionIrvin + '__cantidadentrante';
        var idStockActualIrvin = '#detalleNotaEntrada_' + validacionIrvin + '__cantidadfaltante';



        //alert(idCantidadDeIrvin);detalleNotaEntrada_0__cantidadentrante cantidadfaltante
        arreglosId.push(idCantidadDeIrvin);
    }

    console.log(arreglosId);

    arreglosId.forEach(function (elemento) {
        console.log(elemento);
        $(elemento).change(function (event) {
            console.log(elemento);

            var n = elemento.substring(20, 21);
            //alert(n);
            var idStockActualIrvin = '#detalleNotaEntrada_' + n + '__cantidadfaltante';
            //var idStockMaximoIrvin = '#detallenotaentrada_' + n + '__cantidadentrante';

            console.log($(elemento).get(0).value);
            console.log(parseFloat($(idStockActualIrvin).get(0).value));
            //console.log(parseFloat($(idStockMaximoIrvin).get(0).value));

            if ((parseFloat($(elemento).get(0).value) > parseFloat($(idStockActualIrvin).get(0).value))) {
                alert('La cantidad entrante debe ser menor a la faltante');
                $(elemento).attr("value", "");
            }

        });

    });

}