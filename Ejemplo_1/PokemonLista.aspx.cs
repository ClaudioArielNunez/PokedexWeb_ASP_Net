using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Ejemplo_1
{
    public partial class PokemonLista : System.Web.UI.Page
    {
        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            filtroAvanzado = false;
            PokemonNegocio negocio = new PokemonNegocio();
            //dgvPokemon.DataSource = negocio.listarInactivosConSp();

            Session.Add("PokemonLista", negocio.listarInactivosConSp());
            dgvPokemon.DataSource = Session["PokemonLista"];
            dgvPokemon.DataBind();
        }

        protected void dgvPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPokemon.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPokemon.aspx?id=" + id);
            
        }

        protected void dgvPokemon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPokemon.PageIndex = e.NewPageIndex;
            dgvPokemon.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> lista = (List<Pokemon>)Session["PokemonLista"];
            List<Pokemon> listaFiltrada = lista.FindAll(x=>x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvPokemon.DataSource = listaFiltrada;
            dgvPokemon.DataBind(); 
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkAvanzado.Checked;
            if (filtroAvanzado)
            {
                txtFiltro.Enabled = false;
            }
            else
            {
                txtFiltro.Enabled = true;
            }
        }
    }
}