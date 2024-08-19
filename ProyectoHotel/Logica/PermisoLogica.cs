using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoHotel.Logica
{
    public class PermisoLogica
    {
        private static PermisoLogica instancia = null;

        private PermisoLogica() { }

        public static PermisoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PermisoLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Permiso oPermiso)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPermiso", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", oPermiso.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oPermiso.NombreEmpleado);
                    cmd.Parameters.AddWithValue("PermisoDescripcion", oPermiso.PermisoDescripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return respuesta;
        }

        public bool Modificar(Permiso oPermiso)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarPermiso", oConexion);
                    cmd.Parameters.AddWithValue("IdPermiso", oPermiso.IdPermiso);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oPermiso.NombreEmpleado);
                    cmd.Parameters.AddWithValue("IdPersona", oPermiso.IdPersona);
                    cmd.Parameters.AddWithValue("PermisoDescripcion", oPermiso.PermisoDescripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return respuesta;
        }

        public List<Permiso> Listar()
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdPermiso, IdPersona, NombreEmpleado, PermisoDescripcion FROM PERMISO", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                PermisoDescripcion = dr["PermisoDescripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Permiso> ListarPermiso(int idPersona)
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    // Ajuste de la consulta SQL para filtrar por idPersona
                    SqlCommand cmd = new SqlCommand("SELECT IdPermiso, IdPersona, NombreEmpleado, PermisoDescripcion FROM PERMISO WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona); // Agregar el parámetro idPersona
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                PermisoDescripcion = dr["PermisoDescripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM PERMISO WHERE IdPermiso = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return respuesta;
        }
    }
}