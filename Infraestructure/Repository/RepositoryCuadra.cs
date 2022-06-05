using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryCuadra : IRepositoryCuadra
    {
        public void DeleteCuadra(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CUADRACICLO> GetCuadra()
        {
            IEnumerable<CUADRACICLO> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                lista = ctx.CUADRACICLO.Include(x => x.MARCA).ToList();

            }
            return lista;
        }

        public IEnumerable<CUADRACICLO> GetCuadraByExtra(int idExtra)
        {
            throw new NotImplementedException();
        }

        public CUADRACICLO GetCuadraById(int id)
        {
            CUADRACICLO oCuadra = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oCuadra = ctx.CUADRACICLO.
                    Where(c => c.ID_CUADRACICLO == id).
                    Include(m => m.MARCA).
                    Include(e => e.EXTRA).
                    FirstOrDefault();
            }
            return oCuadra;
        }

        public IEnumerable<CUADRACICLO> GetCuadraByMarca(int idMarca)
        {
            throw new NotImplementedException();
        }

        public CUADRACICLO Save(CUADRACICLO cuadra, string[] selectedExtras)
        {
            int retorno = 0;
            CUADRACICLO oCuadra = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oCuadra = GetCuadraById((int)cuadra.ID_CUADRACICLO);
                IRepositoryExtra _RepositoryExtra = new RepositoryExtra();

                if (oCuadra == null)
                {
                    if (selectedExtras != null)
                    {

                        cuadra.EXTRA = new List<EXTRA>();
                        foreach (var extra in selectedExtras)
                        {
                            var extraToAdd = _RepositoryExtra.GetExtraByID(int.Parse(extra));
                            ctx.EXTRA.Attach(extraToAdd);
                            cuadra.EXTRA.Add(extraToAdd);
                        }
                    }
                    ctx.CUADRACICLO.Add(cuadra);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.CUADRACICLO.Add(cuadra);
                    ctx.Entry(cuadra).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    var selectedExtrasID = new HashSet<string>(selectedExtras);
                    if (selectedExtras != null)
                    {
                        ctx.Entry(cuadra).Collection(e => e.EXTRA).Load();
                        var newExtraForLibro = ctx.EXTRA
                         .Where(x => selectedExtrasID.Contains(x.ID_EXTRA.ToString())).ToList();
                        cuadra.EXTRA = newExtraForLibro;

                        ctx.Entry(cuadra).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if(retorno >= 0)
                oCuadra = GetCuadraById((int)cuadra.ID_CUADRACICLO);

            return oCuadra;

        }
    }
}
