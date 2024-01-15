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
            
            if( !(Page is Login))
            {
                if (!Seguridad.sesionActiva(Session["trainne"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            
        }
    }
}