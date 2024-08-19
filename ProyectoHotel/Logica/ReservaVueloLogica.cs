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
    public class ReservaVueloLogica
    {
        private static ReservaVueloLogica instancia = null;

        public ReservaVueloLogica()
        {

        }

        public static ReservaVueloLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ReservaVueloLogica();
                }

                return instancia;
            }
        }


        public bool Registrar(ReservaVuelo reserva)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarReservaVuelo", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros obligatorios
                    cmd.Parameters.AddWithValue("@IdVuelo", reserva.oVuelo.IdVuelo);
                    cmd.Parameters.AddWithValue("@IdPersona", reserva.oPersona.IdPersona);
                    cmd.Parameters.AddWithValue("@PrecioVuelo", reserva.PrecioVuelo);
                    cmd.Parameters.AddWithValue("@CantidadPasajeros", reserva.CantidadPasajeros); // Nuevo parámetro

                    // Parámetros opcionales
                    cmd.Parameters.AddWithValue("@IdEquipaje", (object)reserva.oEquipaje?.IdEquipaje ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdTipoAsiento", (object)reserva.oTipoAsiento?.IdTipoAsiento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrecioAsiento", (object)reserva.PrecioAsiento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrecioEquipaje", (object)reserva.PrecioEquipaje ?? DBNull.Value);

                    // Calcular y asignar PrecioTotal
                    reserva.PrecioTotal = reserva.CantidadPasajeros * reserva.PrecioVuelo +
                                           reserva.CantidadPasajeros * (reserva.PrecioAsiento ?? 0) +
                                          (reserva.PrecioEquipaje ?? 0);
                    cmd.Parameters.AddWithValue("@PrecioTotal", reserva.PrecioTotal);

                    // Parámetro de salida
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
  
                }
            }
            return respuesta;
        }


        public List<ReservaVuelo> ListarReservas()
        {
            List<ReservaVuelo> Lista = new List<ReservaVuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = @"
            SELECT r.IdReserva, 
                   r.IdVuelo, 
                   v.Origen, 
                   v.Destino, 
                   v.FechaSalida, 
                   v.FechaLlegada, 
                   v.IdAerolínea, 
                   r.IdPersona, 
                   p.Nombre AS NombrePersona, 
                   r.IdEquipaje, 
                   e.Peso AS PesoEquipaje, 
                   e.Precio AS PrecioEquipaje, 
                   r.IdTipoAsiento, 
                   t.Descripcion AS TipoAsientoDescripcion, 
                   t.PrecioExtra AS PrecioAsiento, 
                   r.PrecioVuelo, 
                   r.PrecioAsiento, 
                   r.PrecioEquipaje, 
                   r.PrecioTotal, 
                   r.Estado,
                   r.CantidadPasajeros,  -- Nuevo campo agregado
                   a.Nombre AS NombreAerolinea
            FROM RESERVA_VUELO r
            INNER JOIN VUELO v ON r.IdVuelo = v.IdVuelo
            INNER JOIN PERSONA p ON r.IdPersona = p.IdPersona
            INNER JOIN EQUIPAJE e ON r.IdEquipaje = e.IdEquipaje
            INNER JOIN TIPO_ASIENTO t ON r.IdTipoAsiento = t.IdTipoAsiento
            INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaVuelo reserva = new ReservaVuelo()
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                oVuelo = new Vuelo()
                                {
                                    IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                    Origen = dr["Origen"].ToString(),
                                    Destino = dr["Destino"].ToString(),
                                    FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                    FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                    oAerolinea = new Aerolinea()
                                    {
                                        IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                        Nombre = dr["NombreAerolinea"].ToString()
                                    }
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    Nombre = dr["NombrePersona"].ToString()
                                },
                                oEquipaje = new Equipaje()
                                {
                                    IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                                    Peso = Convert.ToDecimal(dr["PesoEquipaje"]),
                                    Precio = Convert.ToDecimal(dr["PrecioEquipaje"])
                                },
                                oTipoAsiento = new TipoAsiento()
                                {
                                    IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                                    Descripcion = dr["TipoAsientoDescripcion"].ToString(),
                                    PrecioExtra = Convert.ToDecimal(dr["PrecioAsiento"])
                                },
                                PrecioVuelo = Convert.ToDecimal(dr["PrecioVuelo"]),
                                PrecioAsiento = Convert.ToDecimal(dr["PrecioAsiento"]),
                                PrecioEquipaje = Convert.ToDecimal(dr["PrecioEquipaje"]),
                                PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                CantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]) // Asignar el nuevo campo
                            };

                            Lista.Add(reserva);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<ReservaVuelo>(); // Manejo de errores: inicializar la lista en caso de error
                }
            }

            return Lista;
        }


        public List<ReservaVuelo> ListarReservasCanceladas()
        {
            List<ReservaVuelo> Lista = new List<ReservaVuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = @"
                SELECT r.IdReserva, 
                       r.IdVuelo, 
                       v.Origen, 
                       v.Destino, 
                       v.FechaSalida, 
                       v.FechaLlegada, 
                       v.IdAerolínea, 
                       r.IdPersona, 
                       p.Nombre AS NombrePersona, 
                       r.IdEquipaje, 
                       e.Peso AS PesoEquipaje, 
                       e.Precio AS PrecioEquipaje, 
                       r.IdTipoAsiento, 
                       t.Descripcion AS TipoAsientoDescripcion, 
                       t.PrecioExtra AS PrecioAsiento, 
                       r.PrecioVuelo, 
                       r.PrecioAsiento, 
                       r.PrecioEquipaje, 
                       r.PrecioTotal, 
                       r.CantidadPasajeros,  -- Nuevo campo agregado
                       r.Estado,
                       a.Nombre AS NombreAerolinea  -- Ajustar según la estructura de tu base de datos
                FROM RESERVA_VUELO r
                INNER JOIN VUELO v ON r.IdVuelo = v.IdVuelo
                INNER JOIN PERSONA p ON r.IdPersona = p.IdPersona
                INNER JOIN EQUIPAJE e ON r.IdEquipaje = e.IdEquipaje
                INNER JOIN TIPO_ASIENTO t ON r.IdTipoAsiento = t.IdTipoAsiento
                INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea
                WHERE r.Estado = 0";  // Estado FALSE

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaVuelo reserva = new ReservaVuelo()
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                oVuelo = new Vuelo()
                                {
                                    IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                    Origen = dr["Origen"].ToString(),
                                    Destino = dr["Destino"].ToString(),
                                    FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                    FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                    oAerolinea = new Aerolinea()
                                    {
                                        IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                        Nombre = dr["NombreAerolinea"].ToString()
                                    }
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    Nombre = dr["NombrePersona"].ToString()
                                },
                                oEquipaje = new Equipaje()
                                {
                                    IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                                    Peso = Convert.ToDecimal(dr["PesoEquipaje"]),
                                    Precio = Convert.ToDecimal(dr["PrecioEquipaje"])
                                },
                                oTipoAsiento = new TipoAsiento()
                                {
                                    IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                                    Descripcion = dr["TipoAsientoDescripcion"].ToString(),
                                    PrecioExtra = Convert.ToDecimal(dr["PrecioAsiento"])
                                },
                                PrecioVuelo = Convert.ToDecimal(dr["PrecioVuelo"]),
                                PrecioAsiento = Convert.ToDecimal(dr["PrecioAsiento"]),
                                PrecioEquipaje = Convert.ToDecimal(dr["PrecioEquipaje"]),
                                PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                CantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]), // Asignar el nuevo campo
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };

                            Lista.Add(reserva);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<ReservaVuelo>(); // Manejo de errores: inicializar la lista en caso de error
                }
            }

            return Lista;
        }

        public List<ReservaVuelo> ListarReservasPendientes()
        {
            List<ReservaVuelo> Lista = new List<ReservaVuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = @"
                SELECT r.IdReserva, 
                       r.IdVuelo, 
                       v.Origen, 
                       v.Destino, 
                       v.FechaSalida, 
                       v.FechaLlegada, 
                       v.IdAerolínea, 
                       r.IdPersona, 
                       p.Nombre AS NombrePersona, 
                       r.IdEquipaje, 
                       e.Peso AS PesoEquipaje, 
                       e.Precio AS PrecioEquipaje, 
                       r.IdTipoAsiento, 
                       t.Descripcion AS TipoAsientoDescripcion, 
                       t.PrecioExtra AS PrecioAsiento, 
                       r.PrecioVuelo, 
                       r.PrecioAsiento, 
                       r.PrecioEquipaje, 
                       r.PrecioTotal, 
                       r.CantidadPasajeros,  -- Nuevo campo agregado
                       r.Estado,
                       a.Nombre AS NombreAerolinea  -- Ajustar según la estructura de tu base de datos
                FROM RESERVA_VUELO r
                INNER JOIN VUELO v ON r.IdVuelo = v.IdVuelo
                INNER JOIN PERSONA p ON r.IdPersona = p.IdPersona
                INNER JOIN EQUIPAJE e ON r.IdEquipaje = e.IdEquipaje
                INNER JOIN TIPO_ASIENTO t ON r.IdTipoAsiento = t.IdTipoAsiento
                INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea
                WHERE r.Estado = 1";  // Estado FALSE

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaVuelo reserva = new ReservaVuelo()
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                oVuelo = new Vuelo()
                                {
                                    IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                    Origen = dr["Origen"].ToString(),
                                    Destino = dr["Destino"].ToString(),
                                    FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                    FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                    oAerolinea = new Aerolinea()
                                    {
                                        IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                        Nombre = dr["NombreAerolinea"].ToString()
                                    }
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    Nombre = dr["NombrePersona"].ToString()
                                },
                                oEquipaje = new Equipaje()
                                {
                                    IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                                    Peso = Convert.ToDecimal(dr["PesoEquipaje"]),
                                    Precio = Convert.ToDecimal(dr["PrecioEquipaje"])
                                },
                                oTipoAsiento = new TipoAsiento()
                                {
                                    IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                                    Descripcion = dr["TipoAsientoDescripcion"].ToString(),
                                    PrecioExtra = Convert.ToDecimal(dr["PrecioAsiento"])
                                },
                                PrecioVuelo = Convert.ToDecimal(dr["PrecioVuelo"]),
                                PrecioAsiento = Convert.ToDecimal(dr["PrecioAsiento"]),
                                PrecioEquipaje = Convert.ToDecimal(dr["PrecioEquipaje"]),
                                PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                CantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]), // Asignar el nuevo campo
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };

                            Lista.Add(reserva);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<ReservaVuelo>(); // Manejo de errores: inicializar la lista en caso de error
                }
            }

            return Lista;
        }

        public List<ReservaVuelo> ListarReservasCarrito(int idPersona)
        {
            List<ReservaVuelo> Lista = new List<ReservaVuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();

                    string query = @"
            SELECT r.IdReserva, 
                   r.IdVuelo, 
                   v.Origen, 
                   v.Destino, 
                   v.FechaSalida, 
                   v.FechaLlegada, 
                   v.IdAerolínea, 
                   r.IdPersona, 
                   p.Nombre AS NombrePersona, 
                   r.IdEquipaje, 
                   e.Peso AS PesoEquipaje, 
                   e.Precio AS PrecioEquipaje, 
                   r.IdTipoAsiento, 
                   t.Descripcion AS TipoAsientoDescripcion, 
                   t.PrecioExtra AS PrecioAsiento, 
                   r.PrecioVuelo, 
                   r.PrecioAsiento, 
                   r.PrecioEquipaje, 
                   r.PrecioTotal, 
                   r.Estado,
                   r.CantidadPasajeros,  -- Nuevo campo agregado
                   a.Nombre AS NombreAerolinea
            FROM RESERVA_VUELO r
            INNER JOIN VUELO v ON r.IdVuelo = v.IdVuelo
            INNER JOIN PERSONA p ON r.IdPersona = p.IdPersona
            INNER JOIN EQUIPAJE e ON r.IdEquipaje = e.IdEquipaje
            INNER JOIN TIPO_ASIENTO t ON r.IdTipoAsiento = t.IdTipoAsiento
            INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea
            WHERE r.IdPersona = @IdPersona AND r.Estado = 1";  // Estado TRUE

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaVuelo reserva = new ReservaVuelo()
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                oVuelo = new Vuelo()
                                {
                                    IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                    Origen = dr["Origen"].ToString(),
                                    Destino = dr["Destino"].ToString(),
                                    FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                    FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                    oAerolinea = new Aerolinea()
                                    {
                                        IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                        Nombre = dr["NombreAerolinea"].ToString()
                                    }
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    Nombre = dr["NombrePersona"].ToString()
                                },
                                oEquipaje = new Equipaje()
                                {
                                    IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                                    Peso = Convert.ToDecimal(dr["PesoEquipaje"]),
                                    Precio = Convert.ToDecimal(dr["PrecioEquipaje"])
                                },
                                oTipoAsiento = new TipoAsiento()
                                {
                                    IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                                    Descripcion = dr["TipoAsientoDescripcion"].ToString(),
                                    PrecioExtra = Convert.ToDecimal(dr["PrecioAsiento"])
                                },
                                PrecioVuelo = Convert.ToDecimal(dr["PrecioVuelo"]),
                                PrecioAsiento = Convert.ToDecimal(dr["PrecioAsiento"]),
                                PrecioEquipaje = Convert.ToDecimal(dr["PrecioEquipaje"]),
                                PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                CantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]) // Asignar el nuevo campo
                            };

                            Lista.Add(reserva);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<ReservaVuelo>(); // Manejo de errores: inicializar la lista en caso de error
                }
            }

            return Lista;
        }

        public List<ReservaVuelo> ListarReservasUsuario(int idPersona)
        {
            List<ReservaVuelo> Lista = new List<ReservaVuelo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();

                    string query = @"
            SELECT r.IdReserva, 
                   r.IdVuelo, 
                   v.Origen, 
                   v.Destino, 
                   v.FechaSalida, 
                   v.FechaLlegada, 
                   v.IdAerolínea, 
                   r.IdPersona, 
                   p.Nombre AS NombrePersona, 
                   r.IdEquipaje, 
                   e.Peso AS PesoEquipaje, 
                   e.Precio AS PrecioEquipaje, 
                   r.IdTipoAsiento, 
                   t.Descripcion AS TipoAsientoDescripcion, 
                   t.PrecioExtra AS PrecioAsiento, 
                   r.PrecioVuelo, 
                   r.PrecioAsiento, 
                   r.PrecioEquipaje, 
                   r.PrecioTotal, 
                   r.Estado,
                   r.CantidadPasajeros,  -- Nuevo campo agregado
                   a.Nombre AS NombreAerolinea
            FROM RESERVA_VUELO r
            INNER JOIN VUELO v ON r.IdVuelo = v.IdVuelo
            INNER JOIN PERSONA p ON r.IdPersona = p.IdPersona
            INNER JOIN EQUIPAJE e ON r.IdEquipaje = e.IdEquipaje
            INNER JOIN TIPO_ASIENTO t ON r.IdTipoAsiento = t.IdTipoAsiento
            INNER JOIN AEROLINEA a ON v.IdAerolínea = a.IdAerolínea
            WHERE r.IdPersona = @IdPersona";  // Estado TRUE

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdPersona", idPersona);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaVuelo reserva = new ReservaVuelo()
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                oVuelo = new Vuelo()
                                {
                                    IdVuelo = Convert.ToInt32(dr["IdVuelo"]),
                                    Origen = dr["Origen"].ToString(),
                                    Destino = dr["Destino"].ToString(),
                                    FechaHoraSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                    FechaHoraLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                                    oAerolinea = new Aerolinea()
                                    {
                                        IdAerolinea = Convert.ToInt32(dr["IdAerolínea"]),
                                        Nombre = dr["NombreAerolinea"].ToString()
                                    }
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    Nombre = dr["NombrePersona"].ToString()
                                },
                                oEquipaje = new Equipaje()
                                {
                                    IdEquipaje = Convert.ToInt32(dr["IdEquipaje"]),
                                    Peso = Convert.ToDecimal(dr["PesoEquipaje"]),
                                    Precio = Convert.ToDecimal(dr["PrecioEquipaje"])
                                },
                                oTipoAsiento = new TipoAsiento()
                                {
                                    IdTipoAsiento = Convert.ToInt32(dr["IdTipoAsiento"]),
                                    Descripcion = dr["TipoAsientoDescripcion"].ToString(),
                                    PrecioExtra = Convert.ToDecimal(dr["PrecioAsiento"])
                                },
                                PrecioVuelo = Convert.ToDecimal(dr["PrecioVuelo"]),
                                PrecioAsiento = Convert.ToDecimal(dr["PrecioAsiento"]),
                                PrecioEquipaje = Convert.ToDecimal(dr["PrecioEquipaje"]),
                                PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                CantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]) // Asignar el nuevo campo
                            };

                            Lista.Add(reserva);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<ReservaVuelo>(); // Manejo de errores: inicializar la lista en caso de error
                }
            }

            return Lista;
        }

        public bool Modificar(ReservaVuelo reserva)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                oConexion.Open();
                SqlTransaction transaction = oConexion.BeginTransaction();

                try
                {
                    // Obtener la información actual de la reserva
                    int cantidadPasajerosActual;
                    int idVueloActual;

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT IdVuelo, CantidadPasajeros FROM RESERVA_VUELO WHERE IdReserva = @IdReserva",
                        oConexion, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                idVueloActual = Convert.ToInt32(dr["IdVuelo"]);
                                cantidadPasajerosActual = Convert.ToInt32(dr["CantidadPasajeros"]);
                            }
                            else
                            {
                                // No se encontró la reserva
                                respuesta = false;
                                transaction.Rollback();
                                return respuesta;
                            }
                        }
                    }

                    // Actualizar el número de pasajeros en la tabla VUELO
                    using (SqlCommand cmdActualizarPasajeros = new SqlCommand(
                        "UPDATE VUELO SET NumeroPasajeros = NumeroPasajeros - @CantidadPasajerosActual WHERE IdVuelo = @IdVuelo",
                        oConexion, transaction))
                    {
                        cmdActualizarPasajeros.Parameters.AddWithValue("@CantidadPasajerosActual", cantidadPasajerosActual);
                        cmdActualizarPasajeros.Parameters.AddWithValue("@IdVuelo", idVueloActual);

                        cmdActualizarPasajeros.ExecuteNonQuery();
                    }

                    // Calcular y asignar PrecioTotal
                    reserva.PrecioTotal = reserva.CantidadPasajeros * reserva.PrecioVuelo +
                                          reserva.CantidadPasajeros * (reserva.PrecioAsiento ?? 0) +
                                          (reserva.PrecioEquipaje ?? 0);

                    // Proceder con la modificación de la reserva
                    using (SqlCommand cmdModificar = new SqlCommand(
                        @"UPDATE RESERVA_VUELO SET 
                    IdVuelo = @IdVuelo,
                    IdPersona = @IdPersona,
                    IdEquipaje = @IdEquipaje,
                    IdTipoAsiento = @IdTipoAsiento,
                    PrecioVuelo = @PrecioVuelo,
                    PrecioAsiento = @PrecioAsiento,
                    PrecioEquipaje = @PrecioEquipaje,
                    PrecioTotal = @PrecioTotal,
                    CantidadPasajeros = @CantidadPasajeros,
                    Estado = @Estado
                WHERE IdReserva = @IdReserva", oConexion, transaction))
                    {
                        cmdModificar.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                        cmdModificar.Parameters.AddWithValue("@IdVuelo", reserva.oVuelo.IdVuelo);
                        cmdModificar.Parameters.AddWithValue("@IdPersona", reserva.oPersona.IdPersona);
                        cmdModificar.Parameters.AddWithValue("@IdEquipaje", (object)reserva.oEquipaje?.IdEquipaje ?? DBNull.Value);
                        cmdModificar.Parameters.AddWithValue("@IdTipoAsiento", (object)reserva.oTipoAsiento?.IdTipoAsiento ?? DBNull.Value);
                        cmdModificar.Parameters.AddWithValue("@PrecioVuelo", reserva.PrecioVuelo);
                        cmdModificar.Parameters.AddWithValue("@PrecioAsiento", (object)reserva.PrecioAsiento ?? DBNull.Value);
                        cmdModificar.Parameters.AddWithValue("@PrecioEquipaje", (object)reserva.PrecioEquipaje ?? DBNull.Value);
                        cmdModificar.Parameters.AddWithValue("@PrecioTotal", reserva.PrecioTotal);
                        cmdModificar.Parameters.AddWithValue("@CantidadPasajeros", reserva.CantidadPasajeros);
                        cmdModificar.Parameters.AddWithValue("@Estado", true);

                        cmdModificar.ExecuteNonQuery();
                    }

                    // Actualizar el número de pasajeros en la tabla VUELO con la nueva cantidad
                    using (SqlCommand cmdActualizarPasajerosNuevo = new SqlCommand(
                        "UPDATE VUELO SET NumeroPasajeros = NumeroPasajeros + @CantidadPasajeros WHERE IdVuelo = @IdVuelo",
                        oConexion, transaction))
                    {
                        cmdActualizarPasajerosNuevo.Parameters.AddWithValue("@CantidadPasajeros", reserva.CantidadPasajeros);
                        cmdActualizarPasajerosNuevo.Parameters.AddWithValue("@IdVuelo", reserva.oVuelo.IdVuelo);

                        cmdActualizarPasajerosNuevo.ExecuteNonQuery();
                    }

                    // Verificar y actualizar el estado del vuelo si el número de pasajeros es 0
                    using (SqlCommand cmdVerificarEstado = new SqlCommand(
                        "UPDATE VUELO SET Estado = CASE WHEN NumeroPasajeros = 0 THEN 0 ELSE Estado END WHERE IdVuelo = @IdVuelo",
                        oConexion, transaction))
                    {
                        cmdVerificarEstado.Parameters.AddWithValue("@IdVuelo", reserva.oVuelo.IdVuelo);

                        cmdVerificarEstado.ExecuteNonQuery();
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    // Rollback de la transacción en caso de error
                    transaction.Rollback();
                    // Manejar el error (por ejemplo, registrar el error)
                }
                finally
                {
                    oConexion.Close();
                }
            }

            return respuesta;
        }


        public bool EliminarReserva(int idReserva)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();

                    // Recuperar la cantidad de pasajeros de la reserva a eliminar
                    int cantidadPasajeros;
                    int idVuelo;

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT IdVuelo, CantidadPasajeros FROM RESERVA_VUELO WHERE IdReserva = @idReserva",
                        oConexion))
                    {
                        cmd.Parameters.AddWithValue("@idReserva", idReserva);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                idVuelo = Convert.ToInt32(dr["IdVuelo"]);
                                cantidadPasajeros = Convert.ToInt32(dr["CantidadPasajeros"]);
                            }
                            else
                            {
                                // No se encontró la reserva
                                respuesta = false;
                                return respuesta;
                            }
                        }
                    }

                    // Actualizar el número de pasajeros en la tabla VUELO
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE VUELO SET NumeroPasajeros = NumeroPasajeros + @CantidadPasajeros WHERE IdVuelo = @IdVuelo",
                        oConexion))
                    {
                        cmd.Parameters.AddWithValue("@CantidadPasajeros", cantidadPasajeros);
                        cmd.Parameters.AddWithValue("@IdVuelo", idVuelo);
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar la reserva
                    using (SqlCommand cmd = new SqlCommand(
                        "DELETE FROM RESERVA_VUELO WHERE IdReserva = @idReserva",
                        oConexion))
                    {
                        cmd.Parameters.AddWithValue("@idReserva", idReserva);
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    // Manejo del error (opcional)
                }
            }

            return respuesta;
        }
    }
}