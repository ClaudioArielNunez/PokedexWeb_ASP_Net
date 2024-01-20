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
        public void actualizar(Trainee user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USERS SET fechaNacimiento = @fecha, apellido = @apellido, nombre = @nombre, imagenPerfil = @imagen WHERE id = @id");
                //operador ternario para evitar nulos
                datos.setearParametro("@imagen", user.ImagenPerfil);
                datos.setearParametro("@id", user.Id);
                datos.setearParametro("@nombre", user.Nombre);
                datos.setearParametro("@apellido", user.Apellido);
                datos.setearParametro("@fecha", user.FechaNacimiento);
                datos.ejecutarAccion();
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
                datos.setearConsulta("SELECT id, email, pass, admin, nombre, apellido, imagenPerfil, fechaNacimiento FROM USERS WHERE email = @email AND pass = @pass");
                datos.setearParametro("@email", trainne.Email);
                datos.setearParametro("@pass", trainne.Pass);                
                datos.ejecutarLectura();

                if (datos.Lector.Read()) //si lo encontro en la BD  
                {
                    //Cargamos id y si es o no Admin
                    trainne.Id = (int)datos.Lector["id"];                    
                    trainne.Admin = (bool)datos.Lector["admin"];
                    
                    //Validamos que estos datos no sean Null
                    if ( !(datos.Lector["imagenPerfil"] is DBNull))
                    {
                        trainne.ImagenPerfil = (string)datos.Lector["imagenPerfil"];
                    }
                    if ( !(datos.Lector["nombre"] is DBNull))
                    {
                        trainne.Nombre = (string)datos.Lector["nombre"];
                    }
                    if (!(datos.Lector["apellido"] is DBNull) )
                    {
                        trainne.Apellido = datos.Lector["apellido"].ToString();
                    }
                    if (!(datos.Lector["fechaNacimiento"] is DBNull))
                    {
                        trainne.FechaNacimiento = DateTime.Parse(datos.Lector["fechaNacimiento"].ToString());
                    }
                    
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
