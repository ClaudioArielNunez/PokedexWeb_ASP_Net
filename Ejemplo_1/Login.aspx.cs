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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            TraineeNegocio negocio = new TraineeNegocio();  
            Trainee trainee = new Trainee();            
            try
            {
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPass.Text;
                
                bool logueado = negocio.Login(trainee);
                if(logueado)
                {
                    Session.Add("trainne", trainee);//Si se logeo, guardamos en session
                    Response.Redirect("MiPerfil.aspx", false);
                }
                else
                {
                    Session.Add("error", "User o Pass incorrectos");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}