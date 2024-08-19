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
  public class EquipajeLogica
{
    private static EquipajeLogica instancia = null;

    public EquipajeLogica()
    {

    }

    public static EquipajeLogica Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new EquipajeLogica();
            }

            return instancia;
        }
    }

    public bool Registrar(Equipaje oEquipaje)
    {
        bool respuesta = true;
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarelEquipaje", oConexion);
                cmd.Parameters.AddWithValue("Peso", oEquipaje.Peso);
                cmd.Parameters.AddWithValue("Precio", oEquipaje.Precio);
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

    public bool Modificar(Equipaje oEquipaje)
    {
        bool respuesta = true;
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarEquipaje", oConexion);
                cmd.Parameters.AddWithValue("IdEquipaje", oEquipaje.IdEquipaje);
                cmd.Parameters.AddWithValue("Peso", oEquipaje.Peso);
                cmd.Parameters.AddWithValue("Precio", oEquipaje.Precio);
                cmd.Parameters.AddWithValue("Estado", oEquipaje.Estado);
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

    public List<Equipaje> Listar()
    {
        List<Equipaje> Lista = new List<Equipaje>();
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT IdEquipaje, Peso, Precio, Estado FROM EQUIPAJE", oConexion);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new Equipaje()
                        {
                            IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                            Peso = Convert.ToDecimal(dr["Peso"]),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Equipaje>();
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
                SqlCommand cmd = new SqlCommand("DELETE FROM EQUIPAJE WHERE IdEquipaje = @id", oConexion);
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
