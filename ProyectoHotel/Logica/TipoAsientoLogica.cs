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
   public class TipoAsientoLogica
{
    private static TipoAsientoLogica instancia = null;

    public TipoAsientoLogica()
    {

    }

    public static TipoAsientoLogica Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new TipoAsientoLogica();
            }

            return instancia;
        }
    }

    public bool Registrar(TipoAsiento oTipoAsiento)
    {
        bool respuesta = true;
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarelTipoAsiento", oConexion);
                cmd.Parameters.AddWithValue("Descripcion", oTipoAsiento.Descripcion);
                cmd.Parameters.AddWithValue("PrecioExtra", oTipoAsiento.PrecioExtra);
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

    public bool Modificar(TipoAsiento oTipoAsiento)
    {
        bool respuesta = true;
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarTipoAsiento", oConexion);
                cmd.Parameters.AddWithValue("IdTipoAsiento", oTipoAsiento.IdTipoAsiento);
                cmd.Parameters.AddWithValue("Descripcion", oTipoAsiento.Descripcion);
                cmd.Parameters.AddWithValue("PrecioExtra", oTipoAsiento.PrecioExtra);
                cmd.Parameters.AddWithValue("Estado", oTipoAsiento.Estado);
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

    public List<TipoAsiento> Listar()
    {
        List<TipoAsiento> Lista = new List<TipoAsiento>();
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT IdTipoAsiento, Descripcion, PrecioExtra, Estado FROM TIPO_ASIENTO", oConexion);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new TipoAsiento()
                        {
                            IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            PrecioExtra = Convert.ToDecimal(dr["PrecioExtra"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<TipoAsiento>();
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
                SqlCommand cmd = new SqlCommand("DELETE FROM TIPO_ASIENTO WHERE IdTipoAsiento = @id", oConexion);
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
