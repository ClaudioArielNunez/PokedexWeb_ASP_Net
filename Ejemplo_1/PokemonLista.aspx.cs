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
            //si es Admin
            if (!Seguridad.esAdmin(Session["trainne"]))
            {
                Session.Add("error", "Se requiere permisos de Admin para entrar a esta pantalla");
                Response.Redirect("Error.aspx", false);
            }


            filtroAvanzado = false;
            if (!IsPostBack)
            {
                PokemonNegocio negocio = new PokemonNegocio();
                //dgvPokemon.DataSource = negocio.listarInactivosConSp();

                Session.Add("PokemonLista", negocio.listarInactivosConSp());
                dgvPokemon.DataSource = Session["PokemonLista"];
                dgvPokemon.DataBind();
            }
            
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
            //filtroAvanzado = chkAvanzado.Checked;
            if (chkAvanzado.Checked)
            {
                txtFiltro.Enabled = false;
            }
            else
            {
                txtFiltro.Enabled = true;
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();//para q no acumule los items

            if(ddlCampo.SelectedItem.ToString() == "Número")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                dgvPokemon.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text, ddlEstado.SelectedItem.ToString());
                dgvPokemon.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtFiltroAvanzado.Text = "";

                PokemonNegocio negocio = new PokemonNegocio();
                dgvPokemon.DataSource = negocio.listarInactivosConSp();
                dgvPokemon.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }
    }
}