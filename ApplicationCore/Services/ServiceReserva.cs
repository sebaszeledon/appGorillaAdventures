using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReserva : IServiceReserva
    {
        public IEnumerable<RESERVA> GetReserva()
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReserva();
        }

        public RESERVA GetReservaByID(int id)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByID(id);
        }

        public RESERVA Save(RESERVA reserva)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.Save(reserva);
        }
        public IEnumerable<RESERVA> GetReservaByUser(int? id)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByUser(id);
        }
    }
}
