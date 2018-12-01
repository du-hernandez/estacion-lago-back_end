/*
    Muestra los actuadores y sus propiedades:
        id_actuador (int), nombre (string), marcha (stirng[true, false]), tiem_marcha (int), amp_min (float), amp_max (float), amp_actual (float), estado (string[true, false]), descripcion (string)
*/

using System.Collections.Generic;
using estacion_lago.Models;
using estacion_lago.entidades;
using System.Web.Http;

namespace estacion_lago.Controllers
{
    public class MotoresController : ApiController
    {
        // GET: api/Motores
        public motor[] Get()
        {
            motor[] datos = manejador.carga_motores();
            return datos;
        }

        // GET: api/Motores/5
        public motor[] Get(int id, int id_2, int id_3, int id_4, int id_5, int id_6)
        {
            //motor[] datos = manejador.carga_motores(id);
            return null;
        }

        // POST: api/Motores
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Motores/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Motores/5
        public void Delete(int id)
        {
        }
    }
}
