using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Trainee trainee = user != null ? (Trainee)user : null;

            if(trainee != null && trainee.Id != 0)
            {
                return true; //si esta logueado
            }
            else
            {
                return false;
            }
        }
    }
}
