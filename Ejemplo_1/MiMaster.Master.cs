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
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                  
            //Con esto puede ingresar a las paginas basicas
            if( !(Page is Login || Page is Default || Page is Registro || Page is Error))
            {
                //llamamos a la clase Estatica, volvemos a negar la condicion
                if (!Seguridad.sesionActiva(Session["trainne"]))
                {
                    //si no esta logueado mandalo al Login
                    Response.Redirect("Login.aspx", false);
                }
                else //si esta logueado
                {
                    Trainee user = (Trainee)Session["trainne"];
                    
                    if(!string.IsNullOrEmpty(user.ImagenPerfil))
                    {
                        imgAvatar.ImageUrl= "~/Images/" + user.ImagenPerfil ;
                    }
                }
            }
            
            //Trabajamos con el avatar del perfil
            if (Seguridad.sesionActiva(Session["trainne"]))
            {
                if( ((Trainee)Session["trainne"]).ImagenPerfil != null)
                {
                    imgAvatar.ImageUrl = "~/Images/" + (((Trainee)Session["trainne"]).ImagenPerfil) + "?v=" + DateTime.Now.Ticks.ToString();
                }
                else
                {
                    imgAvatar.ImageUrl = "https://www.shutterstock.com/image-vector/blank-avatar-photo-place-holder-600nw-1095249842.jpg";
                }
            }
            else
            {
                imgAvatar.ImageUrl = "https://www.shutterstock.com/image-vector/blank-avatar-photo-place-holder-600nw-1095249842.jpg";
            }

        }

        
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}