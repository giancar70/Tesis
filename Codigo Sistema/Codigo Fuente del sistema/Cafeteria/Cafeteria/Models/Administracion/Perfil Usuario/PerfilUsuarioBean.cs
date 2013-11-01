using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Administracion.Perfil_Usuario
{
    public class PerfilUsuarioBean
    {
        
        public string id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre para el perfil de usuario")]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        public string token { get; set; }
        public Boolean estado { get; set; }
    }

}