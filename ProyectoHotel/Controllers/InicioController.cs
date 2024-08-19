using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoHotel.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            var apiConfig = ConfigReader.ReadAmadeusAPIConfig();
            ViewBag.ApiKey = apiConfig.ApiKey;
            ViewBag.ApiSecret = apiConfig.ApiSecret;
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Inicio
        public ActionResult Usuario()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        // GET: Inicio/Registro
        public ActionResult Registro()
        {
            return View();
        }

        // GET: Inicio/Registro
        public ActionResult Perfil()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }
        // GET: Inicio
        public ActionResult Cliente()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }


        [HttpGet]
        public JsonResult ListarUsuario()
        {
            int id = Convert.ToInt32(Session["IdPersona"]);
            List<Persona> oLista = new List<Persona>();
            oLista = PersonaLogica.Instancia.ListarUsuario(id);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPersona()
        {
            List<Persona> oLista = new List<Persona>();

            oLista = PersonaLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPersona(Persona objeto)
        {
            bool respuesta = false;
            objeto.Clave = objeto.Clave == null ? "" : objeto.Clave;
            objeto.TipoDocumento = objeto.TipoDocumento == null ? "" : objeto.TipoDocumento;
            respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ModificarPersona(Persona objeto)
        {
            bool respuesta = false;
            // Acceder al ID de persona desde la sesión
            respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPersona(int id)
        {
            bool respuesta = false;
            respuesta = PersonaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPersonaPerfil(int id)
        {
            bool respuesta = PersonaLogica.Instancia.Eliminar(id);
            if (respuesta)
            {
                return Json(new { resultado = true, redireccionar = Url.Action("Index", "Login") });
            }
            return Json(new { resultado = false });
        }


        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}