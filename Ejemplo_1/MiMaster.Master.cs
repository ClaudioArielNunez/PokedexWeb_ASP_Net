using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo_1
{
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //llamamos a la clase Estatica, volvemos a negar la condicion
            //Con esto puede ingresar a las paginas basicas
            if( !(Page is Login || Page is Default || Page is Registro))
            {
                if (!Seguridad.sesionActiva(Session["trainne"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            
        }

        
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}