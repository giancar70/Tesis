using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Almacen.Notaentrada
{
    public class NotaentradaService
    {

        NotaentradaDao notadao = new NotaentradaDao();
        public void guardarnotaentrada(Notaentradabean not, string stado)
        {
            notadao.guardarnotaentrada(not, stado);

        }

        public void actualizarstock(Notaentradabean not)
        {
            notadao.actualizarstock(not);

        }




    }
}