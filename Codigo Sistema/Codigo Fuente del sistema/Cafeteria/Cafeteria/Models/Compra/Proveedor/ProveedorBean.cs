using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Compra.Proveedor
{
    public class ProveedorBean
    {
        public string id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre de Producto")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string razonSocial { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }
        [Display(Name = "Nombre Contacto")]
        public string contacto { get; set; }
        [Display(Name = "Email Contacto")]
        public string emailContacto { get; set; }
        [Display(Name = "Direccion")]
        public string direccion { get; set; }
        [Display(Name = "Ruc")]
        public string ruc { get; set; }
        [Display(Name = "Telefono 1")]
        public string telefono1 { get; set; }
        [Display(Name = "Telefono 2")]
        public string telefono2 { get; set; }
        [Display(Name = "Telefono Contacto")]
        public string telefonoContacto { get; set; }
        [Display(Name = "Observacion")]
        public string Observacion { get; set; }
        [Display(Name = "Cargo Contacto")]
        public string CargoContacto { get; set; }
        [Display(Name = "Web")]
        public string web { get; set; }


    }
}