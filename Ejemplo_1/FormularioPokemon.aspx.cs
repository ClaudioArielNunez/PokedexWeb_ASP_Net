using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.ComponentModel.Design.Serialization;

namespace Ejemplo_1
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        //prop para el check box
        public bool confirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false; //Esta prop es solo lectura
            confirmaEliminacion = false;
            try
            {
                //Configuracion inicial
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataBind();
                }
                //Configuracion si modificamos con id por parametro
                if (Request.QueryString["id"] != null && !IsPostBack) //evitamos el postback, ya q se pisan los valores 
                {                                                    //que modificamos con los datos antiguos en el PageLoad
                    PokemonNegocio negocio = new PokemonNegocio();
                    List<Pokemon> lista = negocio.listar(Request.QueryString["id"].ToString());
                    Pokemon seleccionado = lista[0]; //la lista me trae solo un elemento

                    //Precargamos datos
                    txtNombre.Text = seleccionado.Nombre;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtDescripcion.Text = seleccionado.Descripcion.ToString();

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    //la imagen se trata distinto
                    txtUrlImagen.Text = seleccionado.UrlImagen.ToString();
                    //imgPokemon.ImageUrl = seleccionado.UrlImagen.ToString();
                    //txtUrlImagen_TextChanged(sender, e);

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex); //Agregamos a Session, para usarlo mas adelante
                throw;
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtUrlImagen.Text;

                nuevo.Tipo = new Elemento(); //instanciamos el objeto
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);//accedo a su prop
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]); //necesito el id en este caso 
                    negocio.modificarConSp(nuevo);
                }
                else
                {
                    negocio.agregarConSP(nuevo);
                }

                Response.Redirect("PokemonLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtUrlImagen.Text;
        }

        protected void bbtnEliminar_Click(object sender, EventArgs e)
        {
            confirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if(chkConfirmarEliminacion.Checked)
                {                                    
                    //int id = int.Parse(Request.QueryString["id"]);
                    int id = int.Parse(txtId.Text);
                    PokemonNegocio negocio = new PokemonNegocio(); 
                    negocio.eliminar(id);
                    Response.Redirect("PokemonLista.aspx");
                }
            }
            catch (Exception ex) 
            {
                Session.Add("Error", ex);
            }
        }
    }
}