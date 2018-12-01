using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace estacion_lago.entidades
{
    public class Dato
    {
        public int id_dato { get; set; }

        public String valor { get; set; }

        public String fecha { get; set; }

        public int estado { get; set; }

        public int fk_sensor { get; set; }
    }
}