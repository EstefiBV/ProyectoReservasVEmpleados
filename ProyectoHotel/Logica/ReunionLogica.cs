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
    public class ReunionLogica
    {

        private static ReunionLogica instancia = null;

        private ReunionLogica() { }

        public static ReunionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ReunionLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Reunion oReunion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarReunion", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", oReunion.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oReunion.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oReunion.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", oReunion.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", oReunion.HoraFin);
                    cmd.Parameters.AddWithValue("Lugar", oReunion.Lugar);
                    cmd.Parameters.AddWithValue("Descripcion", oReunion.Descripcion);
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

        public bool Modificar(Reunion oReunion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarReunion", oConexion);
                    cmd.Parameters.AddWithValue("IdReunion", oReunion.IdReunion);
                    cmd.Parameters.AddWithValue("IdPersona", oReunion.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oReunion.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oReunion.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", oReunion.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", oReunion.HoraFin);
                    cmd.Parameters.AddWithValue("Lugar", oReunion.Lugar);
                    cmd.Parameters.AddWithValue("Descripcion", oReunion.Descripcion);
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

        public List<Reunion> Listar()
        {
            List<Reunion> lista = new List<Reunion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdReunion, IdPersona, NombreEmpleado, NombreEmpleado, Fecha, HoraInicio, HoraFin, Lugar, Descripcion FROM REUNION", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reunion()
                            {
                                IdReunion = Convert.ToInt32(dr["IdReunion"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Lugar = dr["Lugar"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Reunion>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Reunion> ListarReunion(int idPersona)
        {
            List<Reunion> lista = new List<Reunion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    // Ajuste de la consulta SQL para filtrar por idPersona
                    SqlCommand cmd = new SqlCommand("SELECT IdReunion, IdPersona, NombreEmpleado, Fecha, HoraInicio, HoraFin, Lugar, Descripcion FROM REUNION WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona); // Agregar el parámetro idPersona
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reunion()
                            {
                                IdReunion = Convert.ToInt32(dr["IdReunion"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Lugar = dr["Lugar"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Reunion>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM REUNION WHERE IdReunion = @id", oConexion);
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