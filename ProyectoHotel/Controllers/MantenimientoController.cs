using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoHotel.Controllers
{
    public class MantenimientoController : Controller
    {

        // GET: Mantenimiento
        public ActionResult Aerolinea()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        public ActionResult LVuelos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarAerolinea()
        {
            List<Aerolinea> oLista = new List<Aerolinea>();
            oLista = AerolineaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAerolinea(Aerolinea objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdAerolinea == 0) ? AerolineaLogica.Instancia.Registrar(objeto) : AerolineaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarAerolinea(int id)
        {
            bool respuesta = false;
            respuesta = AerolineaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        // GET: Mantenimiento
        public ActionResult Aeropuertos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarAeropuerto()
        {
            List<Aeropuerto> oLista = new List<Aeropuerto>();
            oLista = AeropuertoLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAeropuerto(Aeropuerto objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdAeropuerto == 0) ? AeropuertoLogica.Instancia.Registrar(objeto) : AeropuertoLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarAeropuerto(int id)
        {
            bool respuesta = false;
            respuesta = AeropuertoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // GET: Mantenimiento
        public ActionResult Vuelos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarVuelo()
        {
            List<Vuelo> oLista = new List<Vuelo>();
            oLista = VueloLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarVuelo(Vuelo objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdVuelo == 0) ? VueloLogica.Instancia.Registrar(objeto) : VueloLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        private readonly AmadeusService _amadeusService;

        public MantenimientoController()
        {
            _amadeusService = new AmadeusService();
        }


        public async Task<JsonResult> ObtenerOpcionesDestino(string origen, decimal maxPrecio)
        {
            try
            {
                var travelAPI = new TravelAPI();
                await travelAPI.ConnectOAuth();
                var opciones = await travelAPI.ObtenerOpcionesDestino(origen, maxPrecio);

                return Json(new { success = true, options = opciones }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    [HttpPost]
        public JsonResult EliminarVuelo(int id)
        {
            bool respuesta = false;
            respuesta = VueloLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // GET: Mantenimiento
        public ActionResult TipoAsiento()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoAsiento()
        {
            List<TipoAsiento> oLista = new List<TipoAsiento>();
            oLista = TipoAsientoLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoAsiento(TipoAsiento objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdTipoAsiento == 0) ? TipoAsientoLogica.Instancia.Registrar(objeto) : TipoAsientoLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoAsiento(int id)
        {
            bool respuesta = false;
            respuesta = TipoAsientoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // GET: Mantenimiento
        public ActionResult Equipaje()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarEquipaje()
        {
            List<Equipaje> oLista = new List<Equipaje>();
            oLista = EquipajeLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEquipaje(Equipaje objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdEquipaje == 0) ? EquipajeLogica.Instancia.Registrar(objeto) : EquipajeLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarEquipaje(int id)
        {
            bool respuesta = false;
            respuesta = EquipajeLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}