using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoHotel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contrasena()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            Session["Usuario"] = ousuario;
            Session["IdPersona"] = ousuario.IdPersona;
            Session["CorreoUsuario"] = ousuario.Correo;
            // Guardar en una cookie
            HttpCookie cookie = new HttpCookie("IdPersona", ousuario.IdPersona.ToString());
            Response.Cookies.Add(cookie);
            HttpCookie cookiecorreo = new HttpCookie("CorreoUsuario", ousuario.Correo.ToString());
            Response.Cookies.Add(cookiecorreo);

            return RedirectToAction("Index", "Inicio");
        }

        [HttpPost]
        public ActionResult CambiarClave(string correo, string clave)
        {
            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo).FirstOrDefault();
            int id = ousuario.IdPersona; 
            bool resultado =PersonaLogica.Instancia.ActualizarClave(id, clave);
            if (resultado)
            {
                // Clave actualizada con éxito
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // Error al actualizar la clave
                ViewBag.Error = "No se pudo actualizar la contraseña. Por favor, intente nuevamente.";
                return RedirectToAction("Contrasena", "Login");
            }
        }

    }
}