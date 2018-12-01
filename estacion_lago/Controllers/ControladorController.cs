using System.Collections.Generic;
using System.Web.Http;
using estacion_lago.Models;

namespace estacion_lago.Controllers
{
    public class ControladorController : ApiController
    {
        // GET: api/Controlador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Controlador/5
        public string Get(string cadena, string estado, string date_start, string date_end, string time_start) // time_end = requisito
        {
            // cadena = m1
            // estado = m2
            // date_start = m3
            // date_end = m4
            // time_start = modo
            manejador.on_off_motor("m1", cadena);
            manejador.on_off_motor("m2", estado);
            manejador.on_off_motor("m3", date_start);
            manejador.on_off_motor("m4", date_end);
            manejador.modo_controlador("motores", time_start);

            return "Ok";
        }

        // POST: api/Controlador
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Controlador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Controlador/5
        public void Delete(int id)
        {
        }
    }
}
