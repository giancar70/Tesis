using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Administracion.Login
{
    public class loginBean
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Es necesario que ingrese el Usuario y su Contraseña")]
        public string usuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Es necesario que ingrese el Usuario y su Contraseña")]
        public string contrasenia { get; set; }
    }
}