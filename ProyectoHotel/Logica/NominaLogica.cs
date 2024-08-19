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
    public class NominaLogica
    {
        private static NominaLogica instancia = null;

        private NominaLogica() { }

        public static NominaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new NominaLogica();
                }
                return instancia;
            }
        }

        public bool Registrar(Nomina oNomina)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarNomina", oConexion);
                    cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Value = oNomina.IdPersona;
                    cmd.Parameters.Add("@NombreEmpleado", SqlDbType.VarChar, 100).Value = oNomina.NombreEmpleado;
                    cmd.Parameters.Add("@NombreRol", SqlDbType.VarChar, 100).Value = oNomina.NombreRol;
                    cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = oNomina.Fecha;
                    cmd.Parameters.Add("@Salario", SqlDbType.Decimal).Value = oNomina.SalarioBase;
                    cmd.Parameters.Add("@Deducciones", SqlDbType.Decimal).Value = oNomina.Deducciones;
                    cmd.Parameters.Add("@Bonificaciones", SqlDbType.Decimal).Value = oNomina.Bonificaciones;

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
                    Console.WriteLine(ex.Message); // Considera usar un sistema de logging en lugar de Console.WriteLine.
                }
            }
            return respuesta;
        }


        public bool Modificar(Nomina oNomina)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarNomina", oConexion);
                    cmd.Parameters.Add("@IdNomina", SqlDbType.Int).Value = oNomina.IdNomina;
                    cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Value = oNomina.IdPersona;
                    cmd.Parameters.Add("@NombreEmpleado", SqlDbType.VarChar, 100).Value = oNomina.NombreEmpleado;
                    cmd.Parameters.Add("@NombreRol", SqlDbType.VarChar, 100).Value = oNomina.NombreRol;
                    cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = oNomina.Fecha;
                    cmd.Parameters.Add("@Salario", SqlDbType.Decimal).Value = oNomina.SalarioBase;
                    cmd.Parameters.Add("@Deducciones", SqlDbType.Decimal).Value = oNomina.Deducciones;
                    cmd.Parameters.Add("@Bonificaciones", SqlDbType.Decimal).Value = oNomina.Bonificaciones;
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

        public List<Nomina> Listar()
        {
            List<Nomina> lista = new List<Nomina>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdNomina, IdPersona, NombreEmpleado, NombreRol, Fecha, Salario, Deducciones, Bonificaciones, SalarioNeto FROM NOMINA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Nomina()
                            {
                                IdNomina = Convert.ToInt32(dr["IdNomina"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                NombreRol = dr["NombreRol"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                SalarioBase = Convert.ToDecimal(dr["Salario"]),
                                Deducciones = Convert.ToDecimal(dr["Deducciones"]),
                                Bonificaciones = Convert.ToDecimal(dr["Bonificaciones"]),
                                SalarioNeto = Convert.ToDecimal(dr["SalarioNeto"])
                         
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Nomina>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Nomina> ListarNominaEmpleado(int idPersona)
        {
            List<Nomina> lista = new List<Nomina>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdNomina, IdPersona, NombreEmpleado, NombreRol, Fecha, Salario, Deducciones, Bonificaciones, SalarioNeto FROM NOMINA WHERE IdPersona = @IdPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Nomina()
                            {
                                IdNomina = Convert.ToInt32(dr["IdNomina"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                NombreEmpleado = dr["NombreEmpleado"].ToString(),
                                NombreRol = dr["NombreRol"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                SalarioBase = Convert.ToDecimal(dr["Salario"]),
                                Deducciones = Convert.ToDecimal(dr["Deducciones"]),
                                Bonificaciones = Convert.ToDecimal(dr["Bonificaciones"]),
                                SalarioNeto = Convert.ToDecimal(dr["SalarioNeto"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Nomina>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }


        public List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdRol, NombreRol, Descripcion, SalarioBase FROM ROL", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                NombreRol = dr["NombreRol"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                SalarioBase = Convert.ToDecimal(dr["SalarioBase"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Deduccion> ListarDeducciones()
        {
            List<Deduccion> lista = new List<Deduccion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdDeduccion, Descripcion, Porcentaje FROM DEDUCCION", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Deduccion()
                            {
                                IdDeduccion = Convert.ToInt32(dr["IdDeduccion"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Porcentaje = Convert.ToDecimal(dr["Porcentaje"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Deduccion>();
                    // Puedes registrar la excepción para depuración si es necesario.
                }
            }
            return lista;
        }
        public List<Bonificacion> ListarBonificaciones()
        {
            List<Bonificacion> lista = new List<Bonificacion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdBonificacion, IdRol, Descripcion, Monto FROM BONIFICACION", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Bonificacion()
                            {
                                IdBonificacion = Convert.ToInt32(dr["IdBonificacion"]),
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Monto = Convert.ToDecimal(dr["Monto"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Bonificacion>();
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM NOMINA WHERE IdNomina = @id", oConexion);
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
