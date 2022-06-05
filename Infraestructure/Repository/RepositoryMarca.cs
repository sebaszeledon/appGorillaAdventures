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
    public class RepositoryMarca : IRepositoryMarca
    {

        public IEnumerable<MARCA> GetMarca()
        {
            try
            {

                IEnumerable<MARCA> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from MARCA
                    lista = ctx.MARCA.ToList<MARCA>();
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

        public MARCA GetMarcaByID(int id)
        {
            MARCA oMarca = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oMarca = ctx.MARCA.Find(id);
            }
            return oMarca;
        }

        public MARCA Save(MARCA marca)
        {
            int retorno = 0;
            MARCA oMarca = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oMarca = GetMarcaByID((int)marca.ID_MARCA);

                if (oMarca == null)
                {
                    ctx.MARCA.Add(marca);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.MARCA.Add(marca);
                    ctx.Entry(marca).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oMarca = GetMarcaByID((int)marca.ID_MARCA);

            return oMarca;
        }
        public void DeleteMarca(int id)
        {
            throw new NotImplementedException();
        }
    }
}
