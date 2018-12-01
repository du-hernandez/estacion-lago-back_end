using System.Web.Http;

namespace estacion_lago
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Habilitamos los CORS en Asp.Net
            config.EnableCors();

            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{cadena}/{estado}/{date_start}/{date_end}/{time_start}/{time_end}/{end}",
                defaults: new { cadena = RouteParameter.Optional, estado = RouteParameter.Optional, date_start = RouteParameter.Optional, date_end = RouteParameter.Optional, time_start = RouteParameter.Optional, time_end = RouteParameter.Optional, end = RouteParameter.Optional }
                /* Agregamos el parámetro {end} con el objetivo de que acepte el último parátro de la función '// GET: api/Actualiza/5' como número decimal, ejemplo:
                 * http://localhost:53196/api/Actualiza/{1.2}(cadena)/{2.2}(estado)/{3.3}(date_start)/{4.4}(date_end)/{3.4}(time_start)/{3.1}(time_end)/{1}(end)
                 * 
                 */
            );
        }
    }
}
