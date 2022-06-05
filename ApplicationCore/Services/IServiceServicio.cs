using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceServicio
    {
        IEnumerable<SERVICIO> GetServicio();
        SERVICIO GetServicioByID(int id);
        void DeleteServicio(int id);
        SERVICIO Save(SERVICIO servicio);
    }
    
}
