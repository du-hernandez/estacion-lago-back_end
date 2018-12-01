namespace estacion_lago.entidades
{
    public class motor
    {
        public int id_motor { get; set; }

        public string nombre { get; set; }

        public int marcha { get; set; }

        public int tiempo_marcha { get; set; }

        public float amp_min { get; set; }

        public float amp_max { get; set; }

        public float amp_actual { get; set; }

        public int estado { get; set; }

        public string descripcion { get; set; }
    }
}