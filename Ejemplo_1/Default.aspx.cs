using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio; //Debemos poner los using correspondientes
using dominio;

namespace Ejemplo_1
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Pokemon> ListaPokemon { get; set; } //creamos una propiedad de tipo lista
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            ListaPokemon = negocio.listarConSP();


        }
    }
}