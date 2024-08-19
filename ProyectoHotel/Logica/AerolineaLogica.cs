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
    public class AerolineaLogica
    {
        private static AerolineaLogica instancia = null;

        public AerolineaLogica()
        {

        }

        public static AerolineaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new AerolineaLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Aerolinea oAerolinea)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarAerolinea", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oAerolinea.Nombre);
                    cmd.Parameters.AddWithValue("Codigo", oAerolinea.Codigo);
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

        public bool Modificar(Aerolinea oAerolinea)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarAerolinea", oConexion);
                    cmd.Parameters.AddWithValue("IdAerolínea", oAerolinea.IdAerolinea);
                    cmd.Parameters.AddWithValue("Nombre", oAerolinea.Nombre);
                    cmd.Parameters.AddWithValue("Codigo", oAerolinea.Codigo);
                    cmd.Parameters.AddWithValue("Estado", oAerolinea.Estado);
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

        public List<Aerolinea> Listar()
        {
            List<Aerolinea> Lista = new List<Aerolinea>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdAerolínea, Nombre, Codigo, Estado FROM AEROLINEA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Aerolinea()
                            {
                                IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                Nombre = dr["Nombre"].ToString(),
                                Codigo = dr["Codigo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Aerolinea>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM AEROLINEA WHERE IdAerolínea = @id", oConexion);
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
