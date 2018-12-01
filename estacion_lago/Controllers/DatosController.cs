using System.Web.Http;
using estacion_lago.entidades;
using estacion_lago.Models;

namespace estacion_lago.Controllers
{
    public class DatosController : ApiController
    {
        // GET: api/Datos
        // Retorna los valores actuales para cada sensor
        public string[] Get()
        {
            string[] resultado = new string[8];
            resultado[0] = manejador.dato_sensor("tem");
            resultado[1] = manejador.dato_sensor("ph");
            resultado[2] = manejador.dato_sensor("oxd");

            // A continuación agregamos los datos del estado de los motores; encendido o apagado
            // y, el modo actual del controlador respectivo

            // Estado del motor 1
            resultado[3] = manejador.estado_actuador("m1");
            // Estado del motor 2
            resultado[4] = manejador.estado_actuador("m2");
            // Estado del motor 3
            resultado[5] = manejador.estado_actuador("m3");
            // Estado del motor 4
            resultado[6] = manejador.estado_actuador("m4");
            // Modo (automático o manual) del controlador de motores
            resultado[7] = manejador.modo_controlador("motores");

            return resultado;
        }

        // GET: api/Datos/5
        //[Route("api/Datos/{id:int}/{estado:int}/{date_start:string}/{date_end:string}")]
        public Dato[] Get(string cadena, int estado, string date_start, string date_end, string time_start, string time_end)
        {
            Dato[] result = manejador.recupera_datos(cadena, estado, date_start, date_end, time_start, time_end);
            return result;
        }

        // POST: api/Datos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Datos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Datos/5
        public void Delete(int id)
        {
        }
    }
}
