using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ProyectoHotel.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginApiController : ApiController
    {
        [HttpGet]
        [Route("index")]
        public IHttpActionResult Index()
        {
            return Ok("Login API is running");
        }

        [HttpPost]
        [Route("index")]
        public IHttpActionResult Index([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Correo) || string.IsNullOrEmpty(request.Clave))
            {
                return BadRequest("Correo y clave son requeridos.");
            }

            Persona ousuario = PersonaLogica.Instancia.Listar()
                                    .Where(u => u.Correo == request.Correo && u.Clave == request.Clave)
                                    .FirstOrDefault();

            if (ousuario == null)
            {
                return Ok(new { respuesta = false, mensaje = "Usuario o contraseña no correcta" });
            }

            // Guardar en una cookie
            var cookieIdPersona = new HttpCookie("IdPersona", ousuario.IdPersona.ToString());
            var cookieCorreoUsuario = new HttpCookie("CorreoUsuario", ousuario.Correo.ToString());
            HttpContext.Current.Response.Cookies.Add(cookieIdPersona);
            HttpContext.Current.Response.Cookies.Add(cookieCorreoUsuario);

            return Ok(new { respuesta = true, redirectUrl = "/inicio/index" });
        }
    

    [HttpPost]
        [Route("cambiarclave")]
        public IHttpActionResult CambiarClave(string correo, string clave)
        {
            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo).FirstOrDefault();

            if (ousuario == null)
            {
                return BadRequest("User not found");
            }

            int id = ousuario.IdPersona;
            bool resultado = PersonaLogica.Instancia.ActualizarClave(id, clave);

            if (resultado)
            {
                return Ok(new { Message = "Password updated successfully" });
            }
            else
            {
                return BadRequest("Failed to update the password. Please try again.");
            }
        }
    }
}