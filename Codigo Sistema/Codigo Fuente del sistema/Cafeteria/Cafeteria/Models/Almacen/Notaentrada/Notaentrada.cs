using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Almacen.Ingrediente;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Almacen.Notaentrada
{
    public class Notaentrada:IngredienteBean

    {
        public int cantidadrecibida { get; set; }
        public int cantidadsolicitada { get; set; }
        public int cantidadfaltante { get; set; }

        //[Range(0, 999, ErrorMessage = "El número mínimo de días es 1")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo números")]
        [Required(ErrorMessage = "Es necesario ingresar la cantidad entrante")]
        public int cantidadentrante { get; set; }
        public Boolean estadonota { get; set; }

    }
}