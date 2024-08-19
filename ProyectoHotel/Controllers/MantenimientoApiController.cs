using ProyectoHotel.Logica;
using ProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProyectoHotel.Controllers
{
    [RoutePrefix("api/Mantenimiento")]
    public class MantenimientoApiController : ApiController
    {
        [HttpGet]
        [Route("ListarAerolinea")]
        public IHttpActionResult ListarAerolinea()
        {
            List<Aerolinea> oLista = AerolineaLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        [HttpPost]
        [Route("GuardarAerolinea")]
        public IHttpActionResult GuardarAerolinea(Aerolinea objeto)
        {
            bool respuesta = (objeto.IdAerolinea == 0) ? AerolineaLogica.Instancia.Registrar(objeto) : AerolineaLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarAerolinea/{id}")]
        public IHttpActionResult EliminarAerolinea(int id)
        {
            bool respuesta = AerolineaLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ListarAeropuerto")]
        public IHttpActionResult ListarAeropuerto()
        {
            List<Aeropuerto> oLista = AeropuertoLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        [HttpPost]
        [Route("GuardarAeropuerto")]
        public IHttpActionResult GuardarAeropuerto(Aeropuerto objeto)
        {
            bool respuesta = (objeto.IdAeropuerto == 0) ? AeropuertoLogica.Instancia.Registrar(objeto) : AeropuertoLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarAeropuerto/{id}")]
        public IHttpActionResult EliminarAeropuerto(int id)
        {
            bool respuesta = AeropuertoLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ListarVuelo")]
        public IHttpActionResult ListarVuelo()
        {
            List<Vuelo> oLista = VueloLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        [HttpPost]
        [Route("GuardarVuelo")]
        public IHttpActionResult GuardarVuelo(Vuelo objeto)
        {
            bool respuesta = (objeto.IdVuelo == 0) ? VueloLogica.Instancia.Registrar(objeto) : VueloLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }
        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarVuelo/{id}")]
        public IHttpActionResult EliminarVuelo(int id)
        {
            bool respuesta = VueloLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ListarTipoAsiento")]
        public IHttpActionResult ListarTipoAsiento()
        {
            List<TipoAsiento> oLista = TipoAsientoLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        [HttpPost]
        [Route("GuardarTipoAsiento")]
        public IHttpActionResult GuardarTipoAsiento(TipoAsiento objeto)
        {
            bool respuesta = (objeto.IdTipoAsiento == 0) ? TipoAsientoLogica.Instancia.Registrar(objeto) : TipoAsientoLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarTipoAsiento/{id}")]
        public IHttpActionResult EliminarTipoAsiento(int id)
        {
            bool respuesta = TipoAsientoLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ListarEquipaje")]
        public IHttpActionResult ListarEquipaje()
        {
            List<Equipaje> oLista = EquipajeLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        [HttpPost]
        [Route("GuardarEquipaje")]
        public IHttpActionResult GuardarEquipaje(Equipaje objeto)
        {
            bool respuesta = (objeto.IdEquipaje == 0) ? EquipajeLogica.Instancia.Registrar(objeto) : EquipajeLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarEquipaje/{id}")]
        public IHttpActionResult EliminarEquipaje(int id)
        {
            bool respuesta = EquipajeLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        [HttpGet]
        [Route("ObtenerOpcionesDestino")]
        public async Task<IHttpActionResult> ObtenerOpcionesDestino(string origen, decimal maxPrecio)
        {
            try
            {
                var travelAPI = new TravelAPI();
                await travelAPI.ConnectOAuth();
                var opciones = await travelAPI.ObtenerOpcionesDestino(origen, maxPrecio);

                return Ok(new { success = true, options = opciones });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}