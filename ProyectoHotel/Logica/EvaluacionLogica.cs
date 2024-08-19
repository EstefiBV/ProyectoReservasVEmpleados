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
    public class EvaluacionLogica
    {
        private static EvaluacionLogica instancia = null;

        private EvaluacionLogica() { }

        public static EvaluacionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new EvaluacionLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Evaluacion oEvaluacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarEvaluacion", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", oEvaluacion.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oEvaluacion.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oEvaluacion.Fecha);
                    cmd.Parameters.AddWithValue("Objetivos", oEvaluacion.Objetivos);
                    cmd.Parameters.AddWithValue("Comentarios", oEvaluacion.Comentarios);
                    cmd.Parameters.AddWithValue("Puntaje", oEvaluacion.Puntaje);
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

        public bool Modificar(Evaluacion oEvaluacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarEvaluacion", oConexion);
                    cmd.Parameters.AddWithValue("IdEvaluacion", oEvaluacion.IdEvaluacion);
                    cmd.Parameters.AddWithValue("IdPersona", oEvaluacion.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oEvaluacion.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oEvaluacion.Fecha);
                    cmd.Parameters.AddWithValue("Objetivos", oEvaluacion.Objetivos);
                    cmd.Parameters.AddWithValue("Comentarios", oEvaluacion.Comentarios);
                    cmd.Parameters.AddWithValue("Puntaje", oEvaluacion.Puntaje);
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

        public List<Evaluacion> Listar()
        {
            List<Evaluacion> lista = new List<Evaluacion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdEvaluacion, IdPersona, NombreEmpleado, Fecha, Objetivos, Comentarios, Puntaje FROM EVALUACION", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Evaluacion()
                            {
                                IdEvaluacion = Convert.ToInt32(dr["IdEvaluacion"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                Objetivos = dr["Objetivos"].ToString(),
                                Comentarios = dr["Comentarios"].ToString(),
                                Puntaje = Convert.ToInt32(dr["Puntaje"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Evaluacion>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Evaluacion> ListarEvaluacion(int idPersona)
        {
            List<Evaluacion> lista = new List<Evaluacion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    // Ajuste de la consulta SQL para filtrar por idPersona
                    SqlCommand cmd = new SqlCommand("SELECT IdEvaluacion, IdPersona, NombreEmpleado, Fecha, Objetivos, Comentarios, Puntaje FROM EVALUACION WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona); // Agregar el parámetro idPersona
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Evaluacion()
                            {
                                IdEvaluacion = Convert.ToInt32(dr["IdEvaluacion"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                Objetivos = dr["Objetivos"].ToString(),
                                Comentarios = dr["Comentarios"].ToString(),
                                Puntaje = Convert.ToInt32(dr["Puntaje"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Evaluacion>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM EVALUACION WHERE IdEvaluacion = @id", oConexion);
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
