using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Almacen.Ingrediente
{
    public class IngredienteBean
    {
        public string id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre de Producto")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        public string estado { get; set; }
    }
}