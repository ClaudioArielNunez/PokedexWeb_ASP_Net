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

            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["trainne"]))
                    {
                        Trainee user = (Trainee)Session["trainne"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;
                        txtFecha.Text = user.FechaNacimiento.ToString("yyyy-MM-dd");
                        if (!string.IsNullOrEmpty(user.ImagenPerfil))
                        {
                            imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil + "?v=" + DateTime.Now.Ticks.ToString(); 
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Trainee user = (Trainee)Session["trainne"];
                TraineeNegocio negocio = new TraineeNegocio();

                //Escribir imagen solo si se cargo algo
                if(txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");
                    user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                }               


                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.FechaNacimiento = DateTime.Parse(txtFecha.Text);

                negocio.actualizar(user);

                Image imagen = (Image)Master.FindControl("imgAvatar");
                imagen.ImageUrl = "~/Images/" + user.ImagenPerfil;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}