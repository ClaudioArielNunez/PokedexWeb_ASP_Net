
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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Trainee user = new Trainee();
                TraineeNegocio negocio = new TraineeNegocio();
                //instanciamos
                EmailService emailService = new EmailService();
                user.Email = txtEmail.Text;
                user.Pass = txtPass.Text;
                int id = negocio.insertarNuevo(user);
                user.Id = id;
                Session.Add("trainne", user); //Esto nos deja la Session abierta cuando nos registramos


                //Probando email
                emailService.armarCorreo(user.Email, "Bienvenida Trainner", "Hola, te damos la bienvenida a la app");
                emailService.enviarEmail();
                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}