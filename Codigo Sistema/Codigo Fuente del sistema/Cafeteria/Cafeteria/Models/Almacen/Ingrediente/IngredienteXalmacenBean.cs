using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Almacen.Ingrediente
{
    public class IngredienteXalmacenBean
    {
        [Display(Name = "Cafeteria")]
        public String Cafeteria { get; set; }
        public String idAlmacen { get; set; }
        public String idCafeteria { get; set; }
        public bool estado { get; set; }
        public List<IngredienteAlmacen> listProdAlmacen { get; set; }

    }
}