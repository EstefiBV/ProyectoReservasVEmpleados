using ClosedXML.Excel;
using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace ProyectoHotel.Controllers
{
    [RoutePrefix("api/reporte")]
    public class ReporteApiController : ApiController
    {
  
        [HttpGet]
        [Route("ListarReservas")]
        public IHttpActionResult ListarReservas()
        {
            List<ReservaVuelo> oLista = ReservaVueloLogica.Instancia.ListarReservas();
            return Ok(new { data = oLista });
        }

        [HttpGet]
        [Route("ListarReservasCanceladas")]
        public IHttpActionResult ListarReservasCanceladas()
        {
            List<ReservaVuelo> oLista = ReservaVueloLogica.Instancia.ListarReservasCanceladas();
            return Ok(new { data = oLista });
        }

        [HttpGet]
        [Route("ListarReservasPendientes")]
        public IHttpActionResult ListarReservasPendientes()
        {
            List<ReservaVuelo> oLista = ReservaVueloLogica.Instancia.ListarReservasPendientes();
            return Ok(new { data = oLista });
        }

        [HttpGet]
        [Route("ListarReservasCarrito")]
        public IHttpActionResult ListarReservasCarrito([FromUri] int userId)
        {
            // Verificar que el userId sea válido
            if (userId <= 0)
            {
                return BadRequest("El ID del usuario no es válido.");
            }

            // Obtener las reservas del carrito para el usuario
            List<ReservaVuelo> reservas = new ReservaVueloLogica().ListarReservasCarrito(userId);

            // Verificar si se encontraron reservas
            if (reservas == null || !reservas.Any())
            {
                return NotFound(); // Devuelve un 404 si no hay reservas
            }

            // Retornar las reservas en formato JSON
            return Ok(new { data = reservas });
        }

        [HttpGet]
        [Route("ListarReservasUsuario")]
        public IHttpActionResult ListarReservasUsuario(int userId)
        {
            List<ReservaVuelo> reservas = new ReservaVueloLogica().ListarReservasUsuario(userId);
            return Ok(new { data = reservas });
        }


        [HttpPost]
        [Route("RegistrarVuelo")]
        public IHttpActionResult RegistrarVuelo(ReservaVuelo objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al registrar la reserva";
            
            respuesta = (objeto.IdReserva == 0) ? ReservaVueloLogica.Instancia.Registrar(objeto) : ReservaVueloLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Reserva guardada exitosamente";
                string correo = objeto.oPersona.Correo;

                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo);
            }
            else
            {
                mensaje = "No se pudo guardar la reserva";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }

        private void EnviarCorreoConfirmacion(string correoUsuario)
        {
            try
            {
                var correo = new MailMessage();
                correo.From = new MailAddress("estefaniabozavillalobos@gmail.com", "Estefanía Boza");
                correo.To.Add(new MailAddress(correoUsuario));
                correo.Subject = "Confirmación de Reserva";
                correo.Body = "Su reserva ha sido registrada exitosamente.";
                correo.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("estefaniabozavillalobos@gmail.com", "rcvbfoiceukusdds");
                smtp.EnableSsl = true;

                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo de confirmación: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarVuelos")]
        public IHttpActionResult ListarVuelos()
        {
            var listaVuelos = VueloLogica.Instancia.Listar().Select(v => new {
                v.IdVuelo,
                v.Destino,
                Aerolinea = v.oAerolinea.Nombre,
                FechaHoraSalida = v.FechaHoraSalida.ToString("o"),
                FechaHoraLlegada = v.FechaHoraLlegada.ToString("o"),
                v.Precio,
                v.NumeroPasajeros
            }).ToList();

            return Ok(listaVuelos);
        }

        [HttpGet]
        [Route("ListarEquipajes")]
        public IHttpActionResult ListarEquipajes()
        {
            List<Equipaje> listaEquipajes = EquipajeLogica.Instancia.Listar();
            return Ok(listaEquipajes);
        }

        [HttpGet]
        [Route("ListarTiposAsiento")]
        public IHttpActionResult ListarTiposAsiento()
        {
            List<TipoAsiento> listaTiposAsiento = TipoAsientoLogica.Instancia.Listar();
            return Ok(listaTiposAsiento);
        }

        [HttpPost]
        [Route("RegistrarReserva")]
        public IHttpActionResult RegistrarReserva(ReservaVuelo reserva)
        {
            bool respuesta = false;
            respuesta = (reserva.IdReserva == 0) ? ReservaVueloLogica.Instancia.Registrar(reserva) : ReservaVueloLogica.Instancia.Modificar(reserva);
            return Ok(new { resultado = respuesta });
        }

 
        [HttpDelete]
        [Route("EliminarReserva/{id}")]
        public IHttpActionResult EliminarReserva(int id)
        {
            bool resultado = new ReservaVueloLogica().EliminarReserva(id);
            return Ok(new { resultado });
        }

        [HttpGet]
        [Route("ContarReservasUsuario")]
        public IHttpActionResult ContarReservasUsuario()
        {
            Persona objeto = (Persona)HttpContext.Current.Session["Usuario"];
            int contador = 0;

            using (SqlConnection connection = new SqlConnection(Conexion.CN))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM RESERVA_VUELO WHERE IdPersona = @IdPersona AND Estado = 1", connection))
                {
                    command.Parameters.AddWithValue("@IdPersona", objeto.IdPersona);
                    contador = (int)command.ExecuteScalar();
                }
            }

            return Ok(new { contador });
        }

        [HttpPost]
        [Route("CancelarReserva/{id}")]
        public IHttpActionResult CancelarReserva(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                con.Open();
                string query = "UPDATE RESERVA_VUELO SET Estado = 0 WHERE IdReserva = @IdReserva";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { resultado = true });
                    }
                    else
                    {
                        return Ok(new { resultado = false });
                    }
                }
            }
        }
    }
}