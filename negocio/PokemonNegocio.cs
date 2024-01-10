using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.ComponentModel.Design;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar(string id = "") //Reutilizazmos este metodo, ponemos un parametro por omision
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
              
            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS TIPO, D.Descripcion AS DEBILIDAD, " +
                              "P.IdTipo, P.IdDebilidad, P.Id, P.Activo FROM POKEMONS P , ELEMENTOS E, ELEMENTOS D WHERE P.IdTipo = E.Id AND P.IdDebilidad = D.Id ";
                if(id != "")
                {
                    //si se envio un parametro agregamos a la consulta ese id
                    comando.CommandText += " and P.Id = " + id; 
                }
                
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = lector.GetString(1);
                    aux.Descripcion = (string)lector["Descripcion"];

                    //validacion de null
                    if (!(lector["UrlImagen"] is DBNull))
                    aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                conexion.Close();
            }   
            
        }

        //copiamos el codigo del metodo filtrar en listarconSP(StoreProcedures)
        //borramos la parte del filtro porque no queremos buscar un pokemon en especial

        //Este metodo lo usamos para la vista de las cards donde solo vemos los activos
        public List<Pokemon> listarConSP()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //string consulta = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS TIPO, D.Descripcion AS DEBILIDAD, P.IdTipo, P.IdDebilidad, P.Id  FROM POKEMONS P , ELEMENTOS E, ELEMENTOS D WHERE P.IdTipo = E.Id AND P.IdDebilidad = D.Id AND P.Activo = 1 ";
                //datos.setearConsulta(consulta);

                //como ahora necesito leer un sp necesito crear otro metodo en AccesoDatos
                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Este metodo es para la vista de pokemon lista ya q necesito traiga activos e inactivos
        public List<Pokemon> listarInactivosConSp()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearProcedimiento("storedListarInactivos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void agregarConSP(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedAltaPokemon");//llamamos al storedProcedure
                datos.setearParametro("@numero",nuevo.Numero);
                datos.setearParametro("@nombre",nuevo.Nombre);
                datos.setearParametro("@descripcion",nuevo.Descripcion);
                datos.setearParametro("@urlImagen",nuevo.UrlImagen);
                datos.setearParametro("@idTipo",nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad",nuevo.Debilidad.Id);
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

        public void modificarConSp(Pokemon nuevo)
        {
            //copiamos metodo modificar
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarPokemon");
                datos.setearParametro("@numero", nuevo.Numero);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@urlImagen", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("@id", nuevo.Id);

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
        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos(); //conecto DB
            try
            {
                datos.setearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion,Activo, IdTipo, IdDebilidad, UrlImagen) values("+nuevo.Numero+",'" +nuevo.Nombre+"','"+nuevo.Descripcion +"',1, @idTipo, @idDebilidad, @UrlImagen)");
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("@UrlImagen", nuevo.UrlImagen);
                
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
        
        public void modificar(Pokemon poke) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE POKEMONS SET Numero = @numero, Nombre=@nombre, Descripcion=@desc ,UrlImagen=@img ,IdTipo=@idTipo ,IdDebilidad=@idDebilidad WHERE Id=@id");
                datos.setearParametro("@numero", poke.Numero);
                datos.setearParametro("@nombre", poke.Nombre);
                datos.setearParametro("@desc", poke.Descripcion);
                datos.setearParametro("@img", poke.UrlImagen);
                datos.setearParametro("@idTipo", poke.Tipo.Id);
                datos.setearParametro("@idDebilidad", poke.Debilidad.Id);
                datos.setearParametro("@id", poke.Id);

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
        
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM POKEMONS WHERE ID =@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void eliminarLogico(int id, bool activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE POKEMONS SET Activo= @activo WHERE Id=@id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //usamos la misma query que arriba, agregamos un espacio para concatenar filtros
                string consulta = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS TIPO, D.Descripcion AS DEBILIDAD, P.IdTipo, P.IdDebilidad, P.Id, P.Activo  FROM POKEMONS P , ELEMENTOS E, ELEMENTOS D WHERE P.IdTipo = E.Id AND P.IdDebilidad = D.Id AND ";

                if (campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro +"'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                if(estado == "Activo")
                {
                    consulta += " AND P.Activo = 1";
                }else if(estado == "Inactivo")
                {
                    consulta += " AND P.Activo = 0";
                }                

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }                      

                return lista;
            }                          
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}
