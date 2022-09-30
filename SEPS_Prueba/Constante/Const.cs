using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SEPS.Constante
{
    public static class Const
    {
        public static DateTime CambiosEnCamposNuevos = new DateTime(2022, 06, 05);

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    

    public static bool TieneTakeHome(int PK_PROGRAMA)
        {
            bool takeHome = false;


            using(var seps = new SEPS_Entities())
            {
                var programa = seps.SA_PROGRAMA.FirstOrDefault(x => x.PK_Programa == PK_PROGRAMA);

                if(programa != null)
                {
                    return (bool)programa.TIENE_TAKE_HOME;
                }
            }

            //switch ((PKPrograma)PK_PROGRAMA)
            //{
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_SAN_JUAN):     // PK_Programa =  1
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_CAGUAS):       // PK_Programa =  2
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_PONCE):        // PK_Programa =  3
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_AGUADILLA):    // PK_Programa =  4
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_BAYAMÓN):      // PK_Programa =  6
            //    case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_CAYEY):        // PK_Programa = 43
            //    case (PKPrograma.CENTRO_DE_TRATAMIENTO_CON_METADONA_FAJARDO):
            //        esProgramaMetadona = true; break;
            //    default: break;
            //}
            return takeHome;
        }

        public static int CalcularEdad(DateTime fechaNacimiento, DateTime fecha)
        {
            // Obtiene la fecha actual:

            // Comprueba que la se haya introducido una fecha válida; si 
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje 
            // de advertencia:
            if (fechaNacimiento > fecha)
            {
                return -1;
            }
            else
            {
                int edad = fecha.Year - fechaNacimiento.Year;

                // Comprueba que el mes de la fecha de nacimiento es mayor 
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fecha.Month)
                {
                    --edad;
                }

                return edad;
            }

        }
    }
}