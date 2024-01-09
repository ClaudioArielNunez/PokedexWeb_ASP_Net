using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.ComponentModel.Design.Serialization;
using System.Web.Configuration;

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

                    //Guardamos en Session 
                    Session.Add("pokeSeleccionado", seleccionado);

                    //Precargamos datos
                    txtNombre.Text = seleccionado.Nombre;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtDescripcion.Text = seleccionado.Descripcion.ToString();

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    //la imagen se trata distinto
                    txtUrlImagen.Text = seleccionado.UrlImagen.ToString();
                    imgPokemon.ImageUrl = txtUrlImagen.Text;
                    //txtUrlImagen_TextChanged(sender, e);

                    //configuramos acciones del boton
                    if (!seleccionado.Activo)
                    {
                        
                        btnDesactivar.Text = "Activar";
                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex); //Agregamos a Session, para usarlo mas adelante                
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

                    PokemonNegocio negocio = new PokemonNegocio();
                    //Como lo usabamos antes
                    //int id = int.Parse(Request.QueryString["id"].ToString());                                    
                    //negocio.eliminar(id);

                    //usando Session
                    Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];
                    negocio.eliminar(seleccionado.Id);

                    Response.Redirect("PokemonLista.aspx");
                }
            }
            catch (Exception ex) 
            {
                Session.Add("Error", ex);
            }
        }
        
        protected void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                //Quiero mas q solo el id por eso guardo el pokemon q me traje con Session
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];

                //int id = int.Parse(Request.QueryString["id"]);//Esta linea ya no la usamos

                PokemonNegocio negocio = new PokemonNegocio();
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                //En la linea de arriba, si queremos reactivar, necesitamos enviar el estado opuesto por eso !
                Response.Redirect("PokemonLista.aspx");

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }

        }
    }
}
