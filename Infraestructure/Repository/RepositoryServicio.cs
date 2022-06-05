using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryServicio : IRepositoryServicio
    {

        public IEnumerable<SERVICIO> GetServicio()
        {
            try
            {

                IEnumerable<SERVICIO> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from SERVICIO
                    lista = ctx.SERVICIO.ToList<SERVICIO>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public SERVICIO GetServicioByID(int id)
        {
            SERVICIO oSERVICIO = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oSERVICIO = ctx.SERVICIO.Find(id);
            }
            return oSERVICIO;
        }

        public SERVICIO Save(SERVICIO servicio)
        {
            int retorno = 0;
            SERVICIO oServicio = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oServicio = GetServicioByID((int)servicio.ID_SERVICIO);

                if (oServicio == null)
                {
                    ctx.SERVICIO.Add(servicio);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.SERVICIO.Add(servicio);
                    ctx.Entry(servicio).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oServicio = GetServicioByID((int)servicio.ID_SERVICIO);

            return oServicio;

        }
        public void DeleteServicio(int id)
        {
            throw new NotImplementedException();
        }
    }
}
