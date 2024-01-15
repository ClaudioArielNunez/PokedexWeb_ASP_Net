using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TraineeNegocio
    {
        public int insertarNuevo(Trainee nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

			try
			{
                datos.setearProcedimiento("insertarNuevo");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);
                return datos.ejecutarAccionScalar();                
            }
			catch (Exception ex)
			{
				throw ex;
			}
            finally
            {
                datos.cerrarConeccion();
            }
        }

        public bool Login(Trainee trainne)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT id, email, pass, admin FROM USERS WHERE email = @email AND pass = @pass");
                datos.setearParametro("@email", trainne.Email);
                datos.setearParametro("@pass", trainne.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read()) //si lo encontro en la BD  
                {
                    //Cargamos id y si es o no Admin
                    trainne.Id = (int)datos.Lector["id"];
                    trainne.Admin = (bool)datos.Lector["admin"];
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConeccion();
            }
        }
    }
}
