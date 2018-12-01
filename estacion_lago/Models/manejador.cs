using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using estacion_lago.entidades;

namespace estacion_lago.Models
{
    public class manejador
    {
        public static motor[] carga_motores()
        {
            motor[] data;
            string query_sql = "SELECT id_actuador, nombre, marcha, tiem_marcha, amp_min, amp_max, estado, descripcion FROM actuador WHERE estado='1';";
            DataTable resutlado = conexion.EjecutarConsulta(query_sql);
            data = new motor[resutlado.Rows.Count];
            try
            {
                for (int i = 0; i < resutlado.Rows.Count; i++)
                {
                    data[i] = new motor();
                    data[i].id_motor = Int32.Parse(resutlado.Rows[i][0].ToString());
                    data[i].nombre = resutlado.Rows[i][1].ToString();
                    if (resutlado.Rows[i][2].ToString() == "true")
                    {
                        data[i].marcha = 1;
                    }
                    else
                    {
                        data[i].marcha = 0;
                    }
                    data[i].tiempo_marcha = Int32.Parse(resutlado.Rows[i][3].ToString());
                    data[i].amp_min = float.Parse(resutlado.Rows[i][4].ToString());
                    data[i].amp_max = float.Parse(resutlado.Rows[i][5].ToString());
                    //data[i].amp_actual = float.Parse(resutlado.Rows[i][6].ToString());
                    if (resutlado.Rows[i][6].ToString() == "true")
                    {
                        data[i].estado = 1;
                    }
                    else
                    {
                        data[i].estado = 0;
                    }
                    data[i].descripcion = resutlado.Rows[i][7].ToString();
                }
            }
            catch (NullReferenceException) { return null; }
            catch (IndexOutOfRangeException) { return null; }
            return data;
        }

        /*
            Carga los sensores que actualmente se han implementado (GET)
        */
        public static sensor[] carga_sensores(int id_sensor)
        {
            String query_sql = "";
            query_sql = "SELECT id_sensor, nombre, unidad, val_optimo_min, val_optimo_max, estado FROM sensor WHERE id_sensor ='" + id_sensor + "' AND estado = '1'";


            sensor[] data;
            DataTable resutlado = conexion.EjecutarConsulta(query_sql);
            data = new sensor[resutlado.Rows.Count];
            try
            {
                for (int i = 0; i < resutlado.Rows.Count; i++)
                {
                    data[i] = new sensor();
                    data[i].id_sensor = Int32.Parse(resutlado.Rows[i][0].ToString());
                    data[i].nombre = resutlado.Rows[i][1].ToString();
                    data[i].unidad = resutlado.Rows[i][2].ToString();
                    data[i].val_optimo_min = float.Parse(resutlado.Rows[i][3].ToString());
                    data[i].val_optimo_max = float.Parse(resutlado.Rows[i][4].ToString());
                    if (resutlado.Rows[i][5].ToString() == "true")
                    {
                        data[i].estado = 1;
                    }
                    else
                    {
                        data[i].estado = 0;
                    }
                }
            }
            catch (NullReferenceException) { return null; }
            catch (IndexOutOfRangeException) { return null; }
            return data;
        }

        /*
            Carga las estaciones que actualmente se han creado (GET)
        */
        public static estacion[] carga_estacion(int estado)
        {
            estacion[] data;
            string query_sql = "SELECT id_estacion, nombre, estado FROM estacion WHERE estado ='" + estado + "';";
            DataTable resutlado = conexion.EjecutarConsulta(query_sql);
            data = new estacion[resutlado.Rows.Count];
            try
            {
                for (int i = 0; i < resutlado.Rows.Count; i++)
                {
                    data[i] = new estacion();
                    data[i].id_estacion = Int32.Parse(resutlado.Rows[i][0].ToString());
                    data[i].nombre = resutlado.Rows[i][1].ToString();
                    if (resutlado.Rows[i][2].ToString() == "true")
                    {
                        data[i].estado = 1;
                    }
                    else
                    {
                        data[i].estado = 0;
                    }
                }
            }
            catch (NullReferenceException) { return null; }
            catch (IndexOutOfRangeException) { return null; }
            return data;
        }

        /*
            Crea una nueva estación (POST)
        */

        public static void crea_estacion(estacion new_estacion)
        {
            String sql = "INSERT INTO estacion (nombre, lugar) VALUES ('" + new_estacion.nombre + "','" + new_estacion.lugar + "')";
            conexion.EjecutarOperacionDB(sql);
        }

        // Actualiza el valor que se considera necesario para calibrar la sonda de oxígeno disuelto
        public static void calibra_sonda_oxd(string valor)
        {
            string sql = "UPDATE sensor SET calibracion = '" + valor + "' WHERE nombre = 'oxd';";
            conexion.EjecutarOperacionDB(sql);
        }

        // Actualiza el estado del actuador especificado por parámetros con el valor indicado también
        public static void on_off_motor(string motor, string on_of)
        {
            string sql = "UPDATE actuador SET marcha = '" + on_of + "' WHERE nombre = '"+ motor +"';";
            conexion.EjecutarOperacionDB(sql);
        }

        // Actualiza el modo (manual/automático) del controlador especificado por parámetros con el valor también indicado
        public static void modo_controlador(string controlador, string modo)
        {
            string sql = "UPDATE controlador SET modo = '" + modo + "' WHERE nombre = '" + controlador + "';";
            conexion.EjecutarOperacionDB(sql);
        }

        public static Dato[] recupera_datos(string sensor, int estado, string date_start, string date_end, string time_start, string time_end)
        {
            String query_sql = "";
            time_start = time_start.Replace("-", ":");
            time_end = time_end.Replace("-", ":");
            string[] results = valida_fecha(date_start, date_end, time_start, time_end);

            date_start = results[0];
            date_end = results[1];

            if (date_start.Equals("0") && date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado sin considerer una fecha inicial o final
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '"+ sensor +"') AND estado = '" + estado + "' ORDER BY fecha DESC LIMIT 30";
            }
            else if (!date_start.Equals("0") && date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y a partir de una fecha inicial
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha > '" + date_start + "' ORDER BY fecha DESC";
            }
            else if (date_start.Equals("0") && !date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y a partir de una fecha final, es decir, todos los registros anteriores
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha < '" + date_end + "' ORDER BY fecha DESC";
            }
            else if (!date_start.Equals("0") && !date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y entre las fechas indicadas
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha BETWEEN '" + date_start + "' AND '" + date_end + "' ORDER BY fecha DESC";
            }

            Dato[] data;
            DataTable resultado = conexion.EjecutarConsulta(query_sql);
            data = new Dato[resultado.Rows.Count];
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new Dato();
                    data[i].id_dato = Int32.Parse(resultado.Rows[i][0].ToString());
                    data[i].valor = resultado.Rows[i][1].ToString();
                    data[i].fecha = resultado.Rows[i][2].ToString();
                    if (resultado.Rows[i][3].ToString() == "1")
                    {
                        data[i].estado = 1;
                    }
                    else
                    {
                        data[i].estado = 0;
                    }
                    data[i].fk_sensor = Int32.Parse(resultado.Rows[i][4].ToString());
                }
            }
            catch (NullReferenceException) { return null; }
            catch (IndexOutOfRangeException) { return null; }
            return data;
        }

        // Recupera el número de registros de un sensor
        public static int numero_datos(string sensor)
        {
            string sql = "SELECT COUNT(*) FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '"+ sensor +"')";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return Int32.Parse(resultado.Rows[0][0].ToString());
        }


        // Recupera los primeros 30 datos del sensor indicado ordenados de forma descendente de acuerdo a la fecha de registro
        public static DataTable datos_string(string sensor, int estado, string date_start, string date_end, string time_start, string time_end)
        {
            String query_sql = "";
            time_start = time_start.Replace("-", ":");
            time_end = time_end.Replace("-", ":");
            string[] results = valida_fecha(date_start, date_end, time_start, time_end);

            date_start = results[0];
            date_end = results[1];

            if (date_start.Equals("0") && date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado sin considerer una fecha inicial o final
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado = '" + estado + "' ORDER BY fecha DESC LIMIT 30";
            }
            else if (!date_start.Equals("0") && date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y a partir de una fecha inicial
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha > '" + date_start + "' ORDER BY fecha DESC";
            }
            else if (date_start.Equals("0") && !date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y a partir de una fecha final, es decir, todos los registros anteriores
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha < '" + date_end + "' ORDER BY fecha DESC";
            }
            else if (!date_start.Equals("0") && !date_end.Equals("0"))
            {
                //Consulta todos lo datos asociados al sensor indicado y entre las fechas indicadas
                query_sql = "SELECT id_datos, valor, fecha, estado, fk_sensor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + sensor + "') AND estado ='" + estado + "' AND fecha BETWEEN '" + date_start + "' AND '" + date_end + "' ORDER BY fecha DESC";
            }

            Dato[] data;
            DataTable resultado = conexion.EjecutarConsulta(query_sql);
            data = new Dato[resultado.Rows.Count];
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new Dato();
                    data[i].id_dato = Int32.Parse(resultado.Rows[i][0].ToString());
                    data[i].valor = resultado.Rows[i][1].ToString();
                    data[i].fecha = resultado.Rows[i][2].ToString();
                    if (resultado.Rows[i][3].ToString() == "1")
                    {
                        data[i].estado = 1;
                    }
                    else
                    {
                        data[i].estado = 0;
                    }
                    data[i].fk_sensor = Int32.Parse(resultado.Rows[i][4].ToString());
                }
            }
            catch (NullReferenceException) { return null; }
            catch (IndexOutOfRangeException) { return null; }
            return resultado;
        }


        // Recupera el valor actual del sensor que se indique como parámetro
        public static string dato_sensor(string nombre)
        {
            string sql = "SELECT valor FROM datos WHERE fk_sensor = (SELECT id_sensor FROM sensor WHERE nombre = '" + nombre + "') ORDER BY fecha DESC LIMIT 1;";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return resultado.Rows[0][0].ToString();
        }

        // Recupera el estado de un actuador/motor específico (encendido o apagado)
        public static string estado_actuador(string actuador)
        {
            string sql = "SELECT marcha FROM actuador WHERE nombre = '"+actuador+"';";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return resultado.Rows[0][0].ToString();
        }

        // Recupera el estado de un controlador (manuel/automático)
        public static string modo_controlador(string controlador)
        {
            string sql = "SELECT modo FROM controlador WHERE nombre = '" + controlador + "';";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return resultado.Rows[0][0].ToString();
        }

        // Recupera el valor actual de calibració de la sonda de oxígeno disuelto
        public static string valor_calibracio_sonda()
        {
            string sql = "SELECT calibracion FROM sensor WHERE nombre = 'oxd';";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return resultado.Rows[0][0].ToString();
        }

        // Recupera los valores 'val_optimo_min' y 'val_optimo_max' del sensor de oxígeno disuelto
        public static string[] valores_parametro_oxd()
        {
            string sql = "SELECT snr.val_optimo_min, snr.val_optimo_max FROM sensor snr WHERE nombre = 'oxd';";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            string[] strResult = new string[2];
            strResult[0] = resultado.Rows[0][0].ToString();
            strResult[1] = resultado.Rows[0][1].ToString();
            return strResult;
        }

        // Recupera el amperaje máximo soportado por el motor indicado por párametro
        public static string amp_max_motor(string motor)
        {
            string sql = "SELECT mtrs.amp_max FROM actuador mtrs WHERE mtrs.nombre = '" + motor + "';";
            DataTable resultado = conexion.EjecutarConsulta(sql);
            return resultado.Rows[0][0].ToString();
        }

        /* Recibe un objeto PostMtrsOxd al que se asocian los valores:
            Amperaje máximo del motor 1,
            Amperaje máximo del motor 2,
            Amperaje máximo del motor 3,
            Amperaje máximo del motor 4,
            Valor mínimo aceptable de oxígeno disuelto,
            Valor máximo ideal de oxígeno disuelto
        */
        public static string update_parameters(string ampM1, string ampM2, string ampM3, string ampM4, string oxdmin, string oxdmax)
        {
            string sql = "UPDATE actuador SET amp_max = '" + ampM1 + "' WHERE nombre = 'm1';";
            conexion.EjecutarOperacionDB(sql);
            sql = "UPDATE actuador SET amp_max = '" + ampM2 + "' WHERE nombre = 'm2';";
            conexion.EjecutarOperacionDB(sql);
            sql = "UPDATE actuador SET amp_max = '" + ampM3 + "' WHERE nombre = 'm3';";
            conexion.EjecutarOperacionDB(sql);
            sql = "UPDATE actuador SET amp_max = '" + ampM4 + "' WHERE nombre = 'm4';";
            conexion.EjecutarOperacionDB(sql);

            sql = "UPDATE sensor SET val_optimo_min = '" + oxdmin + "', val_optimo_max = '" + oxdmax + "' WHERE nombre = 'oxd';";
            conexion.EjecutarOperacionDB(sql);

            return "Llegó";
        }

        // Valida las fechas que se reciben a través del método GET
        public static string[] valida_fecha(string date_start, string date_end, string time_start, string time_end)
        {
            string[] results = new string[2];
            if (!time_start.Equals("0") && !time_end.Equals("0"))
            {
                // El usuario elige una hora de inicio y un hora de fin

                if (!date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio y una fecha de fin

                    time_start = " " + time_start;
                    time_end = " " + time_end;

                    date_start += time_start;
                    date_end += time_end;
                }
                else if (!date_start.Equals("0") && date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio pero no una fecha de fin

                    time_start = " " + time_start;
                    time_end = " " + time_end;

                    date_end = date_start + time_end;
                    date_start += time_start;
                }
                else if (date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de fin pero no una fecha de inicio

                    time_start = " " + time_start;
                    time_end = " " + time_end;

                    date_start = date_end + time_start;
                    date_end += time_end;
                } else
                {
                    // Si el usuario no elige una fecha de inicio o una fecha de fin, no hay nada qué hacer
                }
            } else if(!time_start.Equals("0") && time_end.Equals("0"))
            {
                // El usuario elige una hora de inicio pero no una hora de fin

                if (!date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio y una fecha de fin

                    time_start = " " + time_start;

                    date_start += time_start;
                    date_end += " 00:00:00";
                }
                else if (!date_start.Equals("0") && date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio pero no una fecha de fin

                    time_start = " " + time_start;

                    //date_end = date_start + time_end;
                    date_start += time_start;
                }
                else if (date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de fin pero no una fecha de inicio

                    time_start = " " + time_start;

                    //date_start = date_end + time_start;
                    date_end += time_start;
                }
            }
            else if (time_start.Equals("0") && !time_end.Equals("0"))
            {
                // El usuario elige una hora de fin pero no una hora de inicio

                if (!date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio y una fecha de fin
                    
                    time_end = " " + time_end;

                    //date_start += time_start;
                    date_end += time_end;
                }
                else if (!date_start.Equals("0") && date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio pero no una fecha de fin
                    
                    time_end = " " + time_end;

                    //date_end = date_start + time_end;
                    date_start += time_end;
                }
                else if (date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de fin pero no una fecha de inicio
                    
                    time_end = " " + time_end;

                    //date_start = date_end + time_start;
                    date_end += time_end;
                }
            }
            else if (time_start.Equals("0") && time_end.Equals("0"))
            {
                // El usuario no elige una hora de fin y tampoco una hora de inicio

                if (!date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio y una fecha de fin

                    date_start += " 00:00:00";
                    date_end += " 00:00:00";
                }
                else if (!date_start.Equals("0") && date_end.Equals("0"))
                {
                    // El usuario elige una fecha de inicio pero no una fecha de fin

                    //date_end = date_start + time_end;
                    date_start += " 00:00:00";
                }
                else if (date_start.Equals("0") && !date_end.Equals("0"))
                {
                    // El usuario elige una fecha de fin pero no una fecha de inicio

                    //date_start = date_end + time_start;
                    date_end += " 00:00:00";
                }
            }
            results[0] = date_start;
            results[1] = date_end;
            return results;
        }
    }
}