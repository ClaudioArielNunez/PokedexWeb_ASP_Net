using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Ejemplo_1
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //llamamos a la clase Estatica
            //volvemos a negar la condicion
            /*
            if ( !Seguridad.sesionActiva(Session["trainne"]))
            {
                Response.Redirect("Login.aspx", false);
            }
            */

            //Operador Ternario
            /*
            Trainee trainee = Session["trainne"] != null ? (Trainee)Session["trainne"] : null;

            if(!(trainee != null && trainee.Id != 0)) 
            {
                Response.Redirect("Login.aspx", false); 
            } 
            */
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //PARA ESCRIBIR:
                //guardamos en ruta la ruta de acceso fisica q corresponde a la ruta virtual
                string ruta = Server.MapPath("./Images/");

                //llamamos a Session para conseguir el id
                Trainee user = (Trainee)Session["trainne"];

                //obtenemos el archivo cargado
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg" );

                //Guardamos imagen SIN la ruta            
                user.ImagenPerfil = "perfil-" + user.Id + ".jpg";

                user.Nombre = txtNombre.Text;

                //necesitamos  llamar al objeto TrainneNegocio y llamamos/creamos metodo actualizar
                TraineeNegocio negocio = new TraineeNegocio();
                negocio.actualizar(user);

                //Actualizar el avatar -vamos a master,ubicamos el control
                //Master.FindControl("imgAvatar");
                //guardamos en un objeto Image
                Image img = (Image)Master.FindControl("imgAvatar");
                //PARA LEER:
                //Necesito sumarle la ruta virtual con la virgulilla + la carpeta
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;


            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}