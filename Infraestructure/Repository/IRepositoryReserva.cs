using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryReserva
    {
        RESERVA GetReservaByID(int id);
        IEnumerable<RESERVA> GetReserva();
        RESERVA Save(RESERVA reserva);
        IEnumerable<RESERVA> GetReservaByUser(int? id);
    }
}
