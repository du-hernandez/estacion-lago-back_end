/*
    Muestra los sensores y sus propiedades:
        id_sensor (int), nombre (string), unidad (stirng), val_optimo_min (float), val_optimo_max (float), estado (string[true, false])
*/

using System.Collections.Generic;
using estacion_lago.entidades;
using estacion_lago.Models;
using System.Web.Http;

namespace estacion_lago.Controllers
{
    public class SensoresController : ApiController
    {
        // GET: api/Sensores
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sensores/nombre/estado/date_start/date_end
        // GET: api/Sensores/nombre/estado/date_start/0
        // GET: api/Sensores/nombre/estado/0/date_end
        // GET: api/Sensores/nombre/estado/0/0

        // GET: api/Sensores/5
        public sensor[] Get(int id)
        {
            sensor[] datos = manejador.carga_sensores(id);
            return datos;
        }

        // POST: api/Sensores
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sensores/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sensores/5
        public void Delete(int id)
        {
        }
    }
}
