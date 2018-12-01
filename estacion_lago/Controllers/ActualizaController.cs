using System.Collections.Generic;
using System.Web.Http;
using estacion_lago.entidades;
using estacion_lago.Models;

namespace estacion_lago.Controllers
{
    public class ActualizaController : ApiController
    {
        // GET: api/Actualiza
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // Actualiza los parametros de los motores y del sensor de oxígeno disuelto
        // Hacemos la siguiente trampa:
        /* Partiendo que no pudimos con el método post, enviarémos los datos a través del método get y así los
         * insertamos.
         */
        // GET: api/Actualiza/5
        public string Get(string cadena, string estado, string date_start, string date_end, string time_start, string time_end, string end)
        {
            // cadena = ampM1
            // estado = ampM2
            // date_start = ampM3
            // date_end = ampM4
            // time_start = oxdmin
            // time_end = oxdmax

            return manejador.update_parameters(cadena, estado, date_start, date_end, time_start, time_end);
        }

        // POST: api/Actualiza
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Actualiza/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Actualiza/5
        public void Delete(int id)
        {
        }
    }
}
