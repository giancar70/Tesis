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

        var idCantidadDeIrvin = '#listaProducto_' + validacionIrvin + '__cantidad';
        var idStockActualIrvin = '#listaProducto_' + validacionIrvin + '__stockActual';
        var idStockMaximoIrvin = '#listaProducto_' + validacionIrvin + '__stockMaximo';
        //alert(idCantidadDeIrvin);
        arreglosId.push(idCantidadDeIrvin);
    }

    console.log(arreglosId);

    arreglosId.forEach(function (elemento) {
        //console.log(elemento);
        $(elemento).change(function (event) {
            console.log(elemento);

            var n = elemento.substring(15, 16);
            var idStockActualIrvin = '#listaProducto_' + n + '__stockActual';
            var idStockMaximoIrvin = '#listaProducto_' + n + '__stockMaximo';

            console.log($(elemento).get(0).value);
            console.log(parseFloat($(idStockActualIrvin).get(0).value));
            console.log(parseFloat($(idStockMaximoIrvin).get(0).value));

            if ((parseFloat($(elemento).get(0).value) + parseFloat($(idStockActualIrvin).get(0).value)) > (parseFloat($(idStockMaximoIrvin).get(0).value))) {
                alert('La suma del Stock Actual y la Cantidad debe ser menor al Stock Máximo');
                $(elemento).attr("value", "");
            }

        });

    });

}