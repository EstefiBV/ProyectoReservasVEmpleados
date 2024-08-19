using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Logica
{
    public class AeropuertoLogica
    {
        private static AeropuertoLogica instancia = null;

        public AeropuertoLogica()
        {
        }

        public static AeropuertoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new AeropuertoLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Aeropuerto oAeropuerto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarAeropuerto", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oAeropuerto.Nombre);
                    cmd.Parameters.AddWithValue("Ciudad", oAeropuerto.Ciudad);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Aeropuerto oAeropuerto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarAeropuerto", oConexion);
                    cmd.Parameters.AddWithValue("IdAeropuerto", oAeropuerto.IdAeropuerto);
                    cmd.Parameters.AddWithValue("Nombre", oAeropuerto.Nombre);
                    cmd.Parameters.AddWithValue("Ciudad", oAeropuerto.Ciudad);
                    cmd.Parameters.AddWithValue("Estado", oAeropuerto.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public List<Aeropuerto> Listar()
        {
            List<Aeropuerto> Lista = new List<Aeropuerto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdAeropuerto, Nombre, Ciudad, Estado from Aeropuerto", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Aeropuerto()
                            {
                                IdAeropuerto = Convert.ToInt32(dr["IdAeropuerto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Aeropuerto>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Aeropuerto where IdAeropuerto = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}