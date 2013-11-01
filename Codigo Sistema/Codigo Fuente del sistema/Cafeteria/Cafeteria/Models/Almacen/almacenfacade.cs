using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Almacen.Ingrediente;
using Cafeteria.Models.Almacen.Notaentrada;

namespace Cafeteria.Models.Almacen
{
    public class almacenfacade
    {
        IngredienteService Ingredienteservice = new IngredienteService();
        NotaentradaService notservice = new NotaentradaService();

        #region Ingrediente
        public List<IngredienteBean> ListarIngrediente(string nombre)
        {
            List<IngredienteBean> prod = new List<IngredienteBean>();
            prod = Ingredienteservice.ListarIngrediente(nombre);

            return prod;
        }
        public void RegistrarIngrediente(IngredienteBean prod)
        {
            Ingredienteservice.RegistrarIngrediente(prod);
        }
        public IngredienteBean buscaringrediente(string id)
        {
            IngredienteBean ingre = Ingredienteservice.buscaringre(id);
            return ingre;
        }
        public void actualizaringre(IngredienteBean ingre)
        {
            Ingredienteservice.actualizaringre(ingre);
        }
        public void eliminarIngrediente(string id)
        {
            Ingredienteservice.EliminarIngrediente(id);
        }
        #endregion

        #region IngredientexAlmacen 

        public IngredienteXalmacenBean obtenerlistadAlmacen(string Idalmacen)
        {
            IngredienteXalmacenBean gg = new IngredienteXalmacenBean();
            gg = Ingredienteservice.obtenerlistadAlmacen(Idalmacen);
            return gg;
        }

        public string getnombreingrediente(string id)
        {
            string gg = "gg";
            gg = Ingredienteservice.getnombreingrediente(id);
            return gg;
        }
        #endregion


        #region notaentrada

        public void guardarnotaentrada(Notaentradabean not, string stado)
        {
            notservice.guardarnotaentrada(not, stado);

        }

        public void actualizarstock(Notaentradabean not)
        {
            notservice.actualizarstock(not);

        }


        #endregion

    }
}