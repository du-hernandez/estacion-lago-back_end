namespace estacion_lago.entidades
{
    public class sensor
    {
        public int id_sensor { get; set; }

        public string nombre { get; set; }

        public string unidad { get; set; }

        public float val_optimo_min { get; set; }

        public float val_optimo_max { get; set; }

        public int estado { get; set; }
    }
}