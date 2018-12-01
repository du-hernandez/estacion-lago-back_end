using System.Collections.Generic;
using System.Web.Http;
using estacion_lago.Models;

namespace estacion_lago.Controllers
{
    public class CalibraController : ApiController
    {
        // Retorna el valor actual de calibración de la sonda de oxígeno disuelto
        // GET: api/Calibra
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Inserta el valor que se considere necesario para calibrar la sonda de oxígeno disuelto
        // GET: api/Calibra/5
        public string Get(string cadena)
        {
            manejador.calibra_sonda_oxd(cadena);
            return "Calibración exitosa";
        }

        // POST: api/Calibra
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Calibra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Calibra/5
        public void Delete(int id)
        {
        }
    }
}
