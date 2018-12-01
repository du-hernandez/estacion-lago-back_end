using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace estacion_lago.Models
{
    public class conexion
    {
        private static MySqlConnection ConexionDB = new MySqlConnection("server = 127.0.0.1; database = modelo_rel; Uid = root; pwd = root");
        private static bool Conectar()
        {
            try
            {
                ConexionDB.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void DesconectorDB()
        {
            ConexionDB.Close();
        }

        public static string EjecutarOperacionDB(string Sentencia)
        {
            string Anomalia = "";
            if (Conectar())
            {
                MySqlCommand Comando = new MySqlCommand();
                Comando.CommandText = Sentencia;
                Comando.Connection = ConexionDB;
                try
                {
                    Comando.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Anomalia = e.Message.ToString();
                    DesconectorDB();
                }
                DesconectorDB();
            }
            return Anomalia;
        }
        public static DataTable EjecutarConsulta(string Sentencia)
        {
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            Adaptador.SelectCommand = new MySqlCommand(Sentencia, ConexionDB);
            DataSet Resultado = new DataSet();
            try
            {
                Adaptador.Fill(Resultado);
                return Resultado.Tables[0];
            }
            catch (Exception) { }
            return null;
        }

    }
}