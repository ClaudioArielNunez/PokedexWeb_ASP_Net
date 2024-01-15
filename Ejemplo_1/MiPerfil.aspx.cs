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
    }
}