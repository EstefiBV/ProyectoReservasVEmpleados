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
    public class TurnoLogica
    {
        private static TurnoLogica instancia = null;

        private TurnoLogica() { }

        public static TurnoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TurnoLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Turno oTurno)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTurno", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", oTurno.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oTurno.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Dias", oTurno.Dias);
                    cmd.Parameters.AddWithValue("HoraInicio", oTurno.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", oTurno.HoraFin);
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

        public bool Modificar(Turno oTurno)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarTurno", oConexion);
                    cmd.Parameters.AddWithValue("IdTurno", oTurno.IdTurno);
                    cmd.Parameters.AddWithValue("IdPersona", oTurno.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oTurno.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Dias", oTurno.Dias);
                    cmd.Parameters.AddWithValue("HoraInicio", oTurno.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", oTurno.HoraFin);
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

        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdTurno, IdPersona, NombreEmpleado, NombreEmpleado, Dias, HoraInicio, HoraFin FROM TURNO", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Turno()
                            {
                                IdTurno = Convert.ToInt32(dr["IdTurno"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                Dias = dr["Dias"].ToString(),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Turno>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Turno> ListarTurno(int idPersona)
        {
            List<Turno> lista = new List<Turno>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    // Ajuste de la consulta SQL para filtrar por idPersona
                    SqlCommand cmd = new SqlCommand("SELECT IdTurno, IdPersona, NombreEmpleado, Dias, HoraInicio, HoraFin FROM TURNO WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona); // Agregar el parámetro idPersona
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Turno()
                            {
                                IdTurno = Convert.ToInt32(dr["IdTurno"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Dias = dr["Dias"].ToString(),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Turno>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM TURNO WHERE IdTurno = @id", oConexion);
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