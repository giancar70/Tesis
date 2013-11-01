using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Administracion.Ubigeo
{
    public class Ubigeo
    {
        public class Departamento
        {
            public string ID { get; set; }
            public string nombre { get; set; }
        }

        public class Distrito
        {
            public string ID { get; set; }
            public string nombre { get; set; }
            public string idDepartamento { get; set; }
            public string idProvincia { get; set; }
        }

        public class Provincia
        {
            public string ID { get; set; }
            public string nombre { get; set; }
            public string idDepartamento { get; set; }
        }

    }
}