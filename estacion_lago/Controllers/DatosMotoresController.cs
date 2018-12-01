using System.Web.Http;
using System.Data;
using estacion_lago.Models;
using estacion_lago.entidades;

namespace estacion_lago.Controllers
{
    public class DatosMotoresController : ApiController
    {
        // GET: api/DatosMotores
        public string[] Get()
        {
            string[] resultado = new string[24];
            resultado[0] = manejador.dato_sensor("vol_m1l1");
            resultado[1] = manejador.dato_sensor("vol_m1l2");
            resultado[2] = manejador.dato_sensor("vol_m1l3");
            resultado[3] = manejador.dato_sensor("am_m1l1");
            resultado[4] = manejador.dato_sensor("am_m1l2");
            resultado[5] = manejador.dato_sensor("am_m1l3");
            resultado[6] = manejador.dato_sensor("vol_m2l1");
            resultado[7] = manejador.dato_sensor("vol_m2l2");
            resultado[8] = manejador.dato_sensor("vol_m2l3");
            resultado[9] = manejador.dato_sensor("am_m2l1");
            resultado[10] = manejador.dato_sensor("am_m2l2");
            resultado[11] = manejador.dato_sensor("am_m2l3");
            resultado[12] = manejador.dato_sensor("vol_m3l1");
            resultado[13] = manejador.dato_sensor("vol_m3l2");
            resultado[14] = manejador.dato_sensor("vol_m3l3");
            resultado[15] = manejador.dato_sensor("am_m3l1");
            resultado[16] = manejador.dato_sensor("am_m3l2");
            resultado[17] = manejador.dato_sensor("am_m3l3");
            resultado[18] = manejador.dato_sensor("vol_m4l1");
            resultado[19] = manejador.dato_sensor("vol_m4l2");
            resultado[20] = manejador.dato_sensor("vol_m4l3");
            resultado[21] = manejador.dato_sensor("am_m4l1");
            resultado[22] = manejador.dato_sensor("am_m4l2");
            resultado[23] = manejador.dato_sensor("am_m4l3");

            return resultado;
        }

        // GET: api/DatosMotores/5
        public DataTable[] Get(string cadena, int estado, string date_start, string date_end, string time_start, string time_end)
        {
            DataTable[] resultado = new DataTable[24];
            //Dato[] result = manejador.recupera_datos(id, estado, date_start, date_end, time_start, time_end);
            resultado[0] = manejador.datos_string("vol_m1l1", estado, date_start, date_end, time_start, time_end);
            //resultado[0] = manejador.numero_datos("vol_m1l1").ToString();
            resultado[1] = manejador.datos_string("vol_m1l2", estado, date_start, date_end, time_start, time_end);
            resultado[2] = manejador.datos_string("vol_m1l3", estado, date_start, date_end, time_start, time_end);

            resultado[3] = manejador.datos_string("am_m1l1", estado, date_start, date_end, time_start, time_end);
            resultado[4] = manejador.datos_string("am_m1l2", estado, date_start, date_end, time_start, time_end);
            resultado[5] = manejador.datos_string("am_m1l3", estado, date_start, date_end, time_start, time_end);



            resultado[6] = manejador.datos_string("vol_m2l1", estado, date_start, date_end, time_start, time_end);
            resultado[7] = manejador.datos_string("vol_m2l2", estado, date_start, date_end, time_start, time_end);
            resultado[8] = manejador.datos_string("vol_m2l3", estado, date_start, date_end, time_start, time_end);

            resultado[9] = manejador.datos_string("am_m2l1", estado, date_start, date_end, time_start, time_end);
            resultado[10] = manejador.datos_string("am_m2l2", estado, date_start, date_end, time_start, time_end);
            resultado[11] = manejador.datos_string("am_m2l3", estado, date_start, date_end, time_start, time_end);
            

            resultado[12] = manejador.datos_string("vol_m3l1", estado, date_start, date_end, time_start, time_end);
            resultado[13] = manejador.datos_string("vol_m3l2", estado, date_start, date_end, time_start, time_end);
            resultado[14] = manejador.datos_string("vol_m3l3", estado, date_start, date_end, time_start, time_end);

            resultado[15] = manejador.datos_string("am_m3l1", estado, date_start, date_end, time_start, time_end);
            resultado[16] = manejador.datos_string("am_m3l2", estado, date_start, date_end, time_start, time_end);
            resultado[17] = manejador.datos_string("am_m3l3", estado, date_start, date_end, time_start, time_end);



            resultado[18] = manejador.datos_string("vol_m4l1", estado, date_start, date_end, time_start, time_end);
            resultado[19] = manejador.datos_string("vol_m4l2", estado, date_start, date_end, time_start, time_end);
            resultado[20] = manejador.datos_string("vol_m4l3", estado, date_start, date_end, time_start, time_end);

            resultado[21] = manejador.datos_string("am_m4l1", estado, date_start, date_end, time_start, time_end);
            resultado[22] = manejador.datos_string("am_m4l2", estado, date_start, date_end, time_start, time_end);
            resultado[23] = manejador.datos_string("am_m4l3", estado, date_start, date_end, time_start, time_end);

            return resultado;
        }

        // Creamos nuestro propio método Get
        /* Retorna los parametros actuales de funcionamiento de los motores y del sensor de oxígeno disuelto */
        [HttpGet]
        [Route("api/DatosMotores/{personalizado}")]
        public string[] motores_parametros(string personalizado)
        {
            string[] arreglo = new string[7];

            string[] srtOxd = manejador.valores_parametro_oxd();

            arreglo[0] = manejador.amp_max_motor("m1"); // Amperaje máximo motor 1
            arreglo[1] = manejador.amp_max_motor("m2"); ; // Amperaje máximo motor 2
            arreglo[2] = manejador.amp_max_motor("m3"); ; // Amperaje máximo motor 3
            arreglo[3] = manejador.amp_max_motor("m4"); ; // Amperaje máximo motor 4
            arreglo[4] = srtOxd[0]; // Oxígeno disuelto mínimo tolerable
            arreglo[5] = srtOxd[1]; // Oxígeno disuelto máximo ideal
            arreglo[6] = manejador.valor_calibracio_sonda(); // Valor actual de calibración de la sonda de oxígeno disuelto
            return arreglo;
        }


        // Actualiza los parametros de los motores y del sensor de oxígeno disuelto
        // POST: api/DatosMotores
        //public string Post([FromBody]PostMtrsOxd value)
        //{
            //return manejador.update_parameters(value);
        //}

        // PUT: api/DatosMotores/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DatosMotores/5
        public void Delete(int id)
        {
        }
    }
}
