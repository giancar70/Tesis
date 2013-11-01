using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Almacen.Notaentrada
{
    public class Notaentradabean
    {
        public string idOrdenCompra { get; set; }
        public string idCafeteria { get; set; }
        public string idProveedor { get; set; }
        public string idGuiaRemision { get; set; }

        [Display(Name = "Proveedor")]
        public string nombreProveedor { get; set; }
        [Display(Name = "Cafeteria")]
        public string nombreCafeteria { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Fecha")]
        public string fechaEmitida { get; set; }

        [Display(Name = "Precio Total")]
        public decimal precioTotal { get; set; }

        [Display(Name = "Fecha")]
        public string fechaRegistradaOrdenCompra { get; set; }
        public List<Notaentrada> detalleNotaEntrada { get; set; }

    }
}