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
    public class RepositoryReserva : IRepositoryReserva
    {
        public IEnumerable<RESERVA> GetReserva()
        {
            List<RESERVA> reservas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    reservas = ctx.RESERVA.
                               Include("USUARIO").
                               Include("DETALLE_CUADRACICLO").
                               Include("DETALLE_CUADRACICLO.CUADRACICLO.MARCA").
                               ToList<RESERVA>();

                }
                return reservas;

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
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<RESERVA> GetReservaByUser(int? id)
        {
            List<RESERVA> reservas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    reservas = ctx.RESERVA.
                               Include("USUARIO").
                               Include("DETALLE_CUADRACICLO").
                               Include("DETALLE_CUADRACICLO.CUADRACICLO.MARCA").
                               Include("DETALLE_SERVICIO").
                               Include("DETALLE_SERVICIO.SERVICIO").
                               Where(p => p.ID_USUARIO == id).
                               ToList<RESERVA>();

                }
                return reservas;

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
                throw new Exception(mensaje);
            }
        }

        public RESERVA GetReservaByID(int id)
        {
            RESERVA reserva = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    reserva = ctx.RESERVA.
                               Include("USUARIO").
                               Include("DETALLE_CUADRACICLO").
                               Include("DETALLE_CUADRACICLO.CUADRACICLO.MARCA").
                               Include("DETALLE_SERVICIO").
                               Include("DETALLE_SERVICIO.SERVICIO").
                               Where(p => p.ID_RESERVA == id).
                               FirstOrDefault<RESERVA>();

                }
                return reserva;

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
                throw new Exception(mensaje);
            }
        }

        public RESERVA Save(RESERVA pReserva)
        {
            int resultado = 0;
            RESERVA reserva = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Orden
                // 2- OrdenDetalle 
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.RESERVA.Add(pReserva);
                        resultado = ctx.SaveChanges();
                        foreach (var detalle in pReserva.DETALLE_CUADRACICLO)
                        {
                            detalle.ID_RESERVA = pReserva.ID_RESERVA;
                        }
                        foreach (var item in pReserva.DETALLE_CUADRACICLO)
                        {
                            // Busco el producto que está en el detalle por IdLibro
                            CUADRACICLO oCuadra = ctx.CUADRACICLO.Find(item.ID_CUADRACICLO);

                            // Se indica que se alteró
                            ctx.Entry(oCuadra).State = EntityState.Modified;
                            // Guardar                         
                            resultado = ctx.SaveChanges();
                        }

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    reserva = GetReservaByID(pReserva.ID_RESERVA);


                return reserva;
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
                throw new Exception(mensaje);
            }
        }
    }
}
