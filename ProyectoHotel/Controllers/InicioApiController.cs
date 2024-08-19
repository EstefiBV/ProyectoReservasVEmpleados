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
    [RoutePrefix("api/Inicio")]
    public class InicioApiController : ApiController
    {
        // GET: api/Inicio/ListarTipoPersona
        [HttpGet]
        [Route("ListarTipoPersona")]
        public IHttpActionResult ListarTipoPersona()
        {
            List<TipoPersona> oLista = PersonaLogica.Instancia.ListarTipoPersona();
            return Ok(new { data = oLista });
        }

        // GET: api/Inicio/ListarUsuario
        [HttpGet]
        [Route("ListarUsuario")]
        public IHttpActionResult ListarUsuario(int idPersona)
        {
            List<Persona> oLista = PersonaLogica.Instancia.ListarUsuario(idPersona);
            return Ok(new { data = oLista });
        }


        // GET: api/Inicio/ListarPersona
        [HttpGet]
        [Route("ListarPersona")]
        public IHttpActionResult ListarPersona()
        {
            List<Persona> oLista = PersonaLogica.Instancia.Listar();
            return Ok(new { data = oLista });
        }

        // POST: api/Inicio/GuardarPersona
        [HttpPost]
        [Route("GuardarPersona")]
        public IHttpActionResult GuardarPersona(Persona objeto)
        {
            objeto.Clave = objeto.Clave ?? "";
            objeto.TipoDocumento = objeto.TipoDocumento ?? "";
            bool respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/ModificarPersona
        [HttpPost]
        [Route("ModificarPersona")]
        public IHttpActionResult ModificarPersona(Persona objeto)
        {
            bool respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarPersona/{id}")]
        public IHttpActionResult EliminarPersona(int id)
        {
            bool respuesta = PersonaLogica.Instancia.Eliminar(id);
            return Ok(new { resultado = respuesta });
        }

        // POST: api/Inicio/EliminarPersona
        [HttpDelete]
        [Route("EliminarPersonaPerfil/{id}")]
        public IHttpActionResult EliminarPersonaPerfil(int id)
        {
            bool respuesta = PersonaLogica.Instancia.Eliminar(id);
            if (respuesta)
            {
                return Ok(new { resultado = true, redireccionar = Url.Link("Default", new { controller = "Login", action = "Index" }) });
            }
            return Ok(new { resultado = false });
        }
    }
}

