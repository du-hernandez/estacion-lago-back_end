
using System.Collections.Generic;
using estacion_lago.Models;
using estacion_lago.entidades;
using System.Web.Http;

namespace estacion_lago.Controllers
{
    public class EstacionesController : ApiController
    {
        // GET: api/Estaciones
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Estaciones/5
        public estacion[] Get(int id)
        {
            estacion[] data = manejador.carga_estacion(id);
            return data;
        }

        // POST: api/Estaciones
        public void Post([FromBody]estacion value)
        {
            manejador.crea_estacion(value);
        }

        // PUT: api/Estaciones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Estaciones/5
        public void Delete(int id)
        {
        }
    }
}
