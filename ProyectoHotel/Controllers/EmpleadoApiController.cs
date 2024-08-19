using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace ProyectoHotel.Controllers
{
    [RoutePrefix("api/Empleado")]
    public class EmpleadoApiController : ApiController
    {

        // GET: api/Mantenimiento/ListarTurnos
        [HttpGet]
        [Route("ListarTurnos")]
        public IHttpActionResult ListarTurnos()
        {
            List<Turno> oLista = TurnoLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarTurnoEmpleado
        [HttpGet]
        [Route("ListarTurnoEmpleado")]
        public IHttpActionResult ListarTurnoEmpleado(int idPersona)
        {
            List<Turno> oLista = TurnoLogica.Instancia.ListarTurno(idPersona);
            return Ok(new { data = oLista });
        }

        // POST: api/Mantenimiento/GuardarTurno
        [HttpPost]
        [Route("GuardarTurno")]
        public IHttpActionResult GuardarTurno(Turno objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar el turno";

            respuesta = (objeto.IdTurno == 0) ? TurnoLogica.Instancia.Registrar(objeto) : TurnoLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Turno guardado exitosamente";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Su horario de trabajo será el siguiente.<br/><br/>" +
                              "Días: " + objeto.Dias + "<br/>" +
                              "Hora de entrada: " + objeto.HoraInicio + "<br/>" +
                              "Hora de salida: " + objeto.HoraFin;
                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar el turno";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }


        // DELETE: api/Mantenimiento/EliminarTurno/{id}
        [HttpDelete]
        [Route("EliminarTurno/{id}")]
        public IHttpActionResult EliminarTurno(int id)
        {
            bool respuesta = TurnoLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ListarEvaluaciones")]
        public IHttpActionResult ListarEvaluaciones()
        {
            List<Evaluacion> oLista = EvaluacionLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarEvaluacionEmplempleado
        [HttpGet]
        [Route("ListarEvaluacionEmpleado")]
        public IHttpActionResult ListarEvaluacionEmpleado(int idPersona)
        {
            List<Evaluacion> oLista = EvaluacionLogica.Instancia.ListarEvaluacion(idPersona);
            return Ok(new { data = oLista });
        }
        // POST: api/Evaluacion/GuardarEvaluacion
        [HttpPost]
        [Route("GuardarEvaluacion")]
        public IHttpActionResult GuardarEvaluacion(Evaluacion objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar la evaluación";

            respuesta = (objeto.IdEvaluacion == 0) ? EvaluacionLogica.Instancia.Registrar(objeto) : EvaluacionLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Los resultados de su evaluación son los siguientes.";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Su evaluación ha sido registrada exitosamente.<br/><br/>" +
                              "Puntaje: " + objeto.Puntaje + "<br/>" +
                              "Comentarios: " + objeto.Comentarios;

                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar la evaluación";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }

        // DELETE: api/Evaluacion/EliminarEvaluacion/{id}
        [HttpDelete]
        [Route("EliminarEvaluacion/{id}")]
        public IHttpActionResult EliminarEvaluacion(int id)
        {
            bool respuesta = EvaluacionLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        // GET: api/Reunion/ListarReuniones
        [HttpGet]
        [Route("ListarReuniones")]
        public IHttpActionResult ListarReuniones()
        {
            List<Reunion> oLista = ReunionLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarReunionEmpleado
        [HttpGet]
        [Route("ListarReunionEmpleado")]
        public IHttpActionResult ListarReunionEmpleado(int idPersona)
        {
            List<Reunion> oLista = ReunionLogica.Instancia.ListarReunion(idPersona);
            return Ok(new { data = oLista });
        }
        // POST: api/Reunion/GuardarReunion
        [HttpPost]
        [Route("GuardarReunion")]
        public IHttpActionResult GuardarReunion(Reunion objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar la reunión";

            respuesta = (objeto.IdReunion == 0) ? ReunionLogica.Instancia.Registrar(objeto) : ReunionLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Reunión guardada exitosamente";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Se le convoca a la siguiente reunión.<br/><br/>" +
                              "Fecha: " + objeto.Fecha + "<br/>" +
                              "Hora de inicio: " + objeto.HoraInicio + "<br/>" +
                              "Hora de fin: " + objeto.HoraFin + "<br/>" +
                              "Lugar: " + objeto.Lugar;

                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar la reunión";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }

        // DELETE: api/Reunion/EliminarReunion/{id}
        [HttpDelete]
        [Route("EliminarReunion/{id}")]
        public IHttpActionResult EliminarReunion(int id)
        {
            bool respuesta = ReunionLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }
        // GET: api/DiaLibre/ListarDiasLibres
        [HttpGet]
        [Route("ListarDiasLibres")]
        public IHttpActionResult ListarDiasLibres()
        {
            List<DiaLibre> oLista = DiasLibresLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarDiaLibreEmpleado
        [HttpGet]
        [Route("ListarDiaLibreEmpleado")]
        public IHttpActionResult ListarDiaLibreEmpleado(int idPersona)
        {
            List<DiaLibre> oLista = DiasLibresLogica.Instancia.ListarDiaLibre(idPersona);
            return Ok(new { data = oLista });
        }
        // POST: api/DiaLibre/GuardarDiaLibre
        [HttpPost]
        [Route("GuardarDiaLibre")]
        public IHttpActionResult GuardarDiaLibre(DiaLibre objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar el día libre";

            respuesta = (objeto.IdDiaLibre == 0) ? DiasLibresLogica.Instancia.Registrar(objeto) : DiasLibresLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Día libre guardado exitosamente";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Su día libre ha sido registrado exitosamente.<br/><br/>" +
                              "Fecha: " + objeto.Fecha + "<br/>" +
                              "Tipo de día libre: " + objeto.TipoDiaLibre + "<br/>" +
                              "Motivo: " + objeto.Motivo;

                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar el día libre";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }


        // DELETE: api/DiaLibre/EliminarDiaLibre/{id}
        [HttpDelete]
        [Route("EliminarDiaLibre/{id}")]
        public IHttpActionResult EliminarDiaLibre(int id)
        {
            bool respuesta = DiasLibresLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        // GET: api/Permiso/ListarPermisos
        [HttpGet]
        [Route("ListarPermisos")]
        public IHttpActionResult ListarPermisos()
        {
            List<Permiso> oLista = PermisoLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarPermisoEmpleado
        [HttpGet]
        [Route("ListarPermisoEmpleado")]
        public IHttpActionResult ListarPermisoEmpleado(int idPersona)
        {
            List<Permiso> oLista = PermisoLogica.Instancia.ListarPermiso(idPersona);
            return Ok(new { data = oLista });
        }

        // POST: api/Permiso/GuardarPermiso
        [Route("GuardarPermiso")]
        public IHttpActionResult GuardarPermiso(Permiso objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar el permiso";

            respuesta = (objeto.IdPermiso == 0) ? PermisoLogica.Instancia.Registrar(objeto) : PermisoLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Permiso guardado exitosamente";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Su permiso ha sido registrado exitosamente.<br/><br/>" +
                              "Número de permiso: " + objeto.IdPermiso + "<br/>" +
                              "Descripción del permiso: " + objeto.PermisoDescripcion + "<br/>";

                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar el permiso";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }

        // DELETE: api/Permiso/EliminarPermiso/{id}
        [HttpDelete]
        [Route("EliminarPermiso/{id}")]
        public IHttpActionResult EliminarPermiso(int id)
        {
            bool respuesta = PermisoLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        // GET: api/Nomina/ListarNominas
        [HttpGet]
        [Route("ListarNominas")]
        public IHttpActionResult ListarNominas()
        {
            List<Nomina> oLista = NominaLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }
        // GET: api/Inicio/ListarNominaEmpleado
        [HttpGet]
        [Route("ListarNominaEmpleado")]
        public IHttpActionResult ListarNominaEmpleado(int idPersona)
        {
            List<Nomina> oLista = NominaLogica.Instancia.ListarNominaEmpleado(idPersona);
            return Ok(new { data = oLista });
        }
        // GET: api/Nomina/ListarRoles
        [HttpGet]
        [Route("ListarRoles")]
        public IHttpActionResult ListarRoles()
        {
            List<Rol> oLista = NominaLogica.Instancia.ListarRoles();
            return Ok(new { data = oLista });
        }
        // GET: api/Nomina/ListarDeducciones
        [HttpGet]
        [Route("ListarDeducciones")]
        public IHttpActionResult ListarDeducciones()
        {
            List<Deduccion> oLista = NominaLogica.Instancia.ListarDeducciones();
            return Ok(new { data = oLista });
        }
        // GET: api/Nomina/ListarBonificaciones
        [HttpGet]
        [Route("ListarBonificaciones")]
        public IHttpActionResult ListarBonificaciones()
        {
            List<Bonificacion> oLista = NominaLogica.Instancia.ListarBonificaciones();
            return Ok(new { data = oLista });
        }

        // GET: api/Nomina/CargarDatos
        [HttpGet]
        [Route("CargarDatos")]
        public IHttpActionResult CargarDatos()
        {
            var roles = NominaLogica.Instancia.ListarRoles();
            var deducciones = NominaLogica.Instancia.ListarDeducciones();
            var bonificaciones = NominaLogica.Instancia.ListarBonificaciones();

            return Ok(new { roles = roles, deducciones = deducciones, bonificaciones = bonificaciones });
        }
        // POST: api/Nomina/GuardarNomina
        [HttpPost]
        [Route("GuardarNomina")]
        public IHttpActionResult GuardarNomina(Nomina objeto)
        {
            bool respuesta = false;
            string mensaje = "Ocurrió un error al guardar la nómina";

            respuesta = (objeto.IdNomina == 0) ? NominaLogica.Instancia.Registrar(objeto) : NominaLogica.Instancia.Modificar(objeto);

            if (respuesta)
            {
                mensaje = "Nómina guardada exitosamente";
                string correo = objeto.Correo;

                // Formateo del cuerpo del correo
                string body = "Su nómina ha sido registrada exitosamente.<br/><br/>" +
                              "Empleado: " + objeto.NombreEmpleado + "<br/>" +
                              "Salario: " + objeto.SalarioBase + "<br/>" +
                              "Deducciones: " + objeto.Deducciones + "<br/>" +
                              "Bonificaciones: " + objeto.Bonificaciones + "<br/>" +
                              "Salario Neto: " + objeto.SalarioNeto;
                if (correo == null)
                {
                    return Unauthorized();
                }

                EnviarCorreoConfirmacion(correo, body);
            }
            else
            {
                mensaje = "No se pudo guardar la nómina";
            }

            return Ok(new { respuesta = respuesta, mensaje = mensaje });
        }

        // DELETE: api/Nomina/EliminarNomina/{id}
        [HttpDelete]
        [Route("EliminarNomina/{id}")]
        public IHttpActionResult EliminarNomina(int id)
        {
            bool respuesta = NominaLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }


        private void EnviarCorreoConfirmacion(string correoUsuario, string body)
        {
            try
            {
                var correo = new MailMessage();
                correo.From = new MailAddress("estefaniabozavillalobos@gmail.com", "Estefanía Boza");
                correo.To.Add(new MailAddress(correoUsuario));
                correo.Subject = "Confirmación de Turno";
                correo.Body = body;
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
    }
}

