using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoHotel.Logica
{
    public class VueloLogica
    {
        private static VueloLogica instancia = null;

        public VueloLogica()
        {

        }

        public static VueloLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VueloLogica();
                }

                return instancia;
            }
        }
        public bool Registrar(Vuelo oVuelo)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVuelo", oConexion);
                    cmd.Parameters.AddWithValue("NumeroVuelo", oVuelo.NumeroVuelo);
                    cmd.Parameters.AddWithValue("IdAerolínea", oVuelo.oAerolinea.IdAerolinea);
                    cmd.Parameters.AddWithValue("Origen", oVuelo.Origen);
                    cmd.Parameters.AddWithValue("Destino", oVuelo.Destino);
                    cmd.Parameters.AddWithValue("FechaSalida", oVuelo.FechaHoraSalida);
                    cmd.Parameters.AddWithValue("FechaLlegada", oVuelo.FechaHoraLlegada);
                    cmd.Parameters.AddWithValue("Precio", oVuelo.Precio);
                    cmd.Parameters.AddWithValue("NumeroPasajeros", oVuelo.NumeroPasajeros); // Nuevo parámetro
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

        public bool Modificar(Vuelo oVuelo)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarVuelo", oConexion);
                    cmd.Parameters.AddWithValue("IdVuelo", oVuelo.IdVuelo);
                    cmd.Parameters.AddWithValue("NumeroVuelo", oVuelo.NumeroVuelo);
                    cmd.Parameters.AddWithValue("Origen", oVuelo.Origen);
                    cmd.Parameters.AddWithValue("Destino", oVuelo.Destino);
                    cmd.Parameters.AddWithValue("FechaSalida", oVuelo.FechaHoraSalida);
                    cmd.Parameters.AddWithValue("FechaLlegada", oVuelo.FechaHoraLlegada);
                    cmd.Parameters.AddWithValue("IdAerolínea", oVuelo.oAerolinea.IdAerolinea);
                    cmd.Parameters.AddWithValue("Estado", oVuelo.Estado);
                    cmd.Parameters.AddWithValue("Precio", oVuelo.Precio);
                    cmd.Parameters.AddWithValue("NumeroPasajeros", oVuelo.NumeroPasajeros); // Nuevo parámetro
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

        public List<Vuelo> Listar()
        {
            List<Vuelo> Lista = new List<Vuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVuelo, v.NumeroVuelo, v.Origen, v.Destino, v.FechaSalida, v.FechaLlegada, a.IdAerolínea, a.Nombre as NombreAerolinea, v.Estado, v.Precio, v.NumeroPasajeros");
                    query.AppendLine("from Vuelo v");
                    query.AppendLine("inner join Aerolinea a on a.IdAerolínea = v.IdAerolínea");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Vuelo()
                            {
                                IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                NumeroVuelo = dr["NumeroVuelo"].ToString(),
                                Origen = dr["Origen"].ToString(),
                                Destino = dr["Destino"].ToString(),
                                FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                oAerolinea = new Aerolinea() { IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]), Nombre = dr["NombreAerolinea"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                NumeroPasajeros = Convert.ToInt32(dr["NumeroPasajeros"]) // Nuevo valor
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Vuelo>();
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
                    SqlCommand cmd = new SqlCommand("delete from Vuelo where IdVuelo = @id", oConexion);
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

        public bool ActualizarEstado(int idVuelo, int estado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Vuelo set Estado = @estado where IdVuelo = @idVuelo", oConexion);
                    cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                    cmd.Parameters.AddWithValue("@estado", estado);
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
