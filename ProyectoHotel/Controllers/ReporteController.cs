using ClosedXML.Excel;
using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ProyectoHotel.Controllers
{
    public class ReporteController : Controller
    {

        // GET: Reporte
        public ActionResult Productos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Reservas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        public ActionResult ReservasUsuario()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult LReservasC()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Carrito()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        } 

        [HttpGet]
        public JsonResult ListarReservas()
        {
            List<ReservaVuelo> oLista = new List<ReservaVuelo>();
            oLista = ReservaVueloLogica.Instancia.ListarReservas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarReservasCanceladas()
        {
            List<ReservaVuelo> oLista = new List<ReservaVuelo>();
            oLista = ReservaVueloLogica.Instancia.ListarReservasCanceladas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarReservasPendientes()
        {
            List<ReservaVuelo> oLista = new List<ReservaVuelo>();
            oLista = ReservaVueloLogica.Instancia.ListarReservasPendientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListarReservasCarrito()
        {
            // Obtener el ID del usuario desde la sesión
            int userId = Convert.ToInt32(Session["IdPersona"]);
            List<ReservaVuelo> reservas = new ReservaVueloLogica().ListarReservasCarrito(userId);
            return Json(new { data = reservas }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarReservasUsuario()
        {
            // Obtener el ID del usuario desde la sesión
            int userId = Convert.ToInt32(Session["IdPersona"]);
            List<ReservaVuelo> reservas = new ReservaVueloLogica().ListarReservasUsuario(userId);
            return Json(new { data = reservas }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Modificar()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

       
        //[HttpPost]
        // Método para registrar una reserva (HTTP POST)
        [HttpPost]
        public JsonResult RegistrarVuelo(ReservaVuelo objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al registrar la reserva";

            // Acceder al ID de persona desde la sesión
            int idPersona = Convert.ToInt32(Session["IdPersona"]);

            // Verificar y asignar el ID de persona a la reserva
            if (objeto.oPersona == null)
            {
                objeto.oPersona = new Persona();
            }
            objeto.oPersona.IdPersona = idPersona;

            // Lógica para registrar la reserva
            respuesta = (objeto.IdReserva == 0) ? ReservaVueloLogica.Instancia.Registrar(objeto) : ReservaVueloLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Reserva guardada exitosamente";
                string correo = Convert.ToString(Session["CorreoUsuario"]); // Convert.ToString(Session["CorreoUsuario"]);
                                                       // Enviar correo electrónico de confirmación
                EnviarCorreoConfirmacion(correo);
            }
            else
            {
                mensaje = "No se pudo guardar la reserva";
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Método para enviar correo de confirmación
        private void EnviarCorreoConfirmacion(string correoUsuario)
        {
            try
            {
                // Configurar el mensaje de correo
                var correo = new System.Net.Mail.MailMessage();
                correo.From = new System.Net.Mail.MailAddress("estefaniabozavillalobos@gmail.com", "Estefanía Boza");
                correo.To.Add(new System.Net.Mail.MailAddress(correoUsuario));
                correo.Subject = "Confirmación de Reserva";
                correo.Body = "Su reserva ha sido registrada exitosamente.";
                correo.IsBodyHtml = true;

                // Configurar el cliente SMTP (aquí se usa Gmail)
                var smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587; // Puerto SMTP de Gmail
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("estefaniabozavillalobos@gmail.com", "rcvbfoiceukusdds");
                smtp.EnableSsl = true;

                // Enviar el correo
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                // Manejar el error al enviar el correo
                Console.WriteLine("Error al enviar el correo de confirmación: " + ex.Message);
            }
        }

        // Método para listar vuelos
        public JsonResult ListarVuelos()
        {
            var listaVuelos = VueloLogica.Instancia.Listar().Select(v => new {
                v.IdVuelo,
                v.Destino,
                Aerolinea = v.oAerolinea.Nombre,
                FechaHoraSalida = v.FechaHoraSalida.ToString("o"), // ISO 8601 format
                FechaHoraLlegada = v.FechaHoraLlegada.ToString("o"),
                v.Precio,
                v.NumeroPasajeros
            }).ToList();

            return Json(listaVuelos, JsonRequestBehavior.AllowGet);
        }


        // Método para listar equipajes
        public JsonResult ListarEquipajes()
        {
            List<Equipaje> listaEquipajes = EquipajeLogica.Instancia.Listar();
            return Json(listaEquipajes, JsonRequestBehavior.AllowGet);
        }

        //Método para listar tipos de asiento
        public JsonResult ListarTiposAsiento()
        {
            List<TipoAsiento> listaTiposAsiento = TipoAsientoLogica.Instancia.Listar();
            return Json(listaTiposAsiento, JsonRequestBehavior.AllowGet);
        }

        // Método para registrar una reserva
        [HttpPost]
        public ActionResult RegistrarReserva(ReservaVuelo reserva)
        {
            bool respuesta = false;
            respuesta = (reserva.IdReserva == 0) ? ReservaVueloLogica.Instancia.Registrar(reserva) : ReservaVueloLogica.Instancia.Modificar(reserva);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarReserva(int id)
        {
            bool resultado = new ReservaVueloLogica().EliminarReserva(id);
            return Json(new { resultado });
        }

        // Agrega esta acción para contar las reservas del usuario actual
        [HttpGet]
        public JsonResult ContarReservasUsuario()
        {
            ProyectoHotel.Models.Persona objeto = (ProyectoHotel.Models.Persona)Session["Usuario"];
            int contador = 0;

            // Lógica para contar las reservas del usuario actual
            using (SqlConnection connection = new SqlConnection(Conexion.CN))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM RESERVA_VUELO WHERE IdPersona = @IdPersona AND Estado = 1", connection))
                {
                    command.Parameters.AddWithValue("@IdPersona", objeto.IdPersona);
                    contador = (int)command.ExecuteScalar();
                }
            }

            return Json(new { contador = contador }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CancelarReserva(int id)
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
                        return Json(new { resultado = true });
                    }
                    else
                    {
                        return Json(new { resultado = false });
                    }
                }
            }
        } 
    }

}
