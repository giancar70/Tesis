using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Almacen.Ingrediente
{
    public class IngredienteService
    {
        Ingredientedao Ingredientedao = new Ingredientedao();

        #region Ingrediente
        public List<IngredienteBean> ListarIngrediente(string nombre) 
        {
            List<IngredienteBean> prod = new List<IngredienteBean>();
            prod=Ingredientedao.ListarIngrediente(nombre);

            return prod;
        }

        public void RegistrarIngrediente(IngredienteBean prod)
        {
            Ingredientedao.registraringrediente(prod);
        }
        public IngredienteBean buscaringre(string id)
        {
            return Ingredientedao.BuscarIngre(id);
        }
        public void actualizaringre(IngredienteBean ingre)
        {
            Ingredientedao.ActualizarIngre(ingre);
        }
        public void EliminarIngrediente(string ID)
        {
            Ingredientedao.EliminarIngrediente(ID);
        }
        #endregion


        #region Ordendecompra

        public IngredienteXalmacenBean obtenerlistadAlmacen(string Idalmacen)
        {
            IngredienteXalmacenBean gg = new IngredienteXalmacenBean();
            gg = Ingredientedao.obtenerlistadAlmacen(Idalmacen);
            return gg;
        }

        public string getnombreingrediente(string id)
        {
            string gg = "gg";
            gg = Ingredientedao.getnombreingrediente(id);
            return gg;
        }

        #endregion



    }
}