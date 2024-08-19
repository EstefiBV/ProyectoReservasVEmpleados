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
    public class DiasLibresLogica
    {

        private static DiasLibresLogica instancia = null;

        private DiasLibresLogica() { }

        public static DiasLibresLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DiasLibresLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(DiaLibre oDiaLibre)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDiaLibre", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", oDiaLibre.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oDiaLibre.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oDiaLibre.Fecha);
                    cmd.Parameters.AddWithValue("TipoDiaLibre", oDiaLibre.TipoDiaLibre);
                    cmd.Parameters.AddWithValue("Motivo", oDiaLibre.Motivo);
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

        public bool Modificar(DiaLibre oDiaLibre)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarDiaLibre", oConexion);
                    cmd.Parameters.AddWithValue("IdDiaLibre", oDiaLibre.IdDiaLibre);
                    cmd.Parameters.AddWithValue("IdPersona", oDiaLibre.IdPersona);
                    cmd.Parameters.AddWithValue("NombreEmpleado", oDiaLibre.NombreEmpleado);
                    cmd.Parameters.AddWithValue("Fecha", oDiaLibre.Fecha);
                    cmd.Parameters.AddWithValue("TipoDiaLibre", oDiaLibre.TipoDiaLibre);
                    cmd.Parameters.AddWithValue("Motivo", oDiaLibre.Motivo);
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

        public List<DiaLibre> Listar()
        {
            List<DiaLibre> lista = new List<DiaLibre>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdDiaLibre, IdPersona, NombreEmpleado, Fecha, TipoDiaLibre, Motivo FROM DIAS_LIBRES", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DiaLibre()
                            {
                                IdDiaLibre = Convert.ToInt32(dr["IdDiaLibre"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                TipoDiaLibre = dr["TipoDiaLibre"].ToString(),
                                Motivo = dr["Motivo"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<DiaLibre>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<DiaLibre> ListarDiaLibre(int idPersona)
        {
            List<DiaLibre> lista = new List<DiaLibre>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    // Ajuste de la consulta SQL para filtrar por idPersona
                    SqlCommand cmd = new SqlCommand("SELECT IdDiaLibre, IdPersona, NombreEmpleado, Fecha, TipoDiaLibre, Motivo FROM DIAS_LIBRES WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona); // Agregar el parámetro idPersona
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DiaLibre()
                            {
                                IdDiaLibre = Convert.ToInt32(dr["IdDiaLibre"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                TipoDiaLibre = dr["TipoDiaLibre"].ToString(),
                                Motivo = dr["Motivo"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<DiaLibre>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM DIAS_LIBRES WHERE IdDiaLibre = @id", oConexion);
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