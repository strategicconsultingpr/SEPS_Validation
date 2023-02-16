using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace SEPS.Constante
{
    public static class TEDS
    {

        public static void UpdateTransaction(int perfil,Guid sesion ,string tipoTransaccion ,int estatus)
        {
            using (var seps = new SEPS_Entities())
            {
                var p = seps.SA_PERFIL.FirstOrDefault(x => x.PK_NR_Perfil == perfil);

                if (p != null)
                {
                    
                        p.FK_ESTATUS_PERFIL_TEDS = estatus;
                        p.TI_Transaccion = tipoTransaccion;
                        p.FK_Sesion = sesion;
                        seps.Entry(p).State = System.Data.Entity.EntityState.Modified;
                }

                    seps.SaveChanges();
                }
               
            }
            
        
      

        public static void UpdateTransactionAllEpisode(int episodio, Guid sesion, string tipoTransaccion , int estatus)
        {
            using (var seps = new SEPS_Entities())
            {
                var perfiles = seps.SA_PERFIL.Where(x => x.FK_Episodio == episodio).ToList();

                if (perfiles.Count > 0)
                {
                    foreach (var p in perfiles)
                    {
                        p.FK_ESTATUS_PERFIL_TEDS = estatus;
                        p.TI_Transaccion = tipoTransaccion;
                        p.FK_Sesion = sesion;
                        seps.Entry(p).State = System.Data.Entity.EntityState.Modified;


                    }

                    seps.SaveChanges();
                }

            }


        }

        public static void DeleteTransaction(int perfil, Guid sesion,  int estatus)
        {
            using (var seps = new SEPS_Entities())
            {
                seps.SPC_PERFILES_ELIMINADOS_POR_PERFIL(perfil, sesion, estatus);
            }

        }




        public static void DeleteTransactionAllEpisode(int episodio, Guid sesion, int estatus)
        {
            using (var seps = new SEPS_Entities())
            {
                seps.SPC_PERFILES_ELIMINADOS_POR_EPISODIO(episodio, sesion, estatus);
            }

        }






    }
}  
