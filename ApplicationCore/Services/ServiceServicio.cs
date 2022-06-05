using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceServicio : IServiceServicio
    {

        public IEnumerable<SERVICIO> GetServicio()
        {
            IRepositoryServicio repository = new RepositoryServicio();
            return repository.GetServicio();
        }

        public SERVICIO GetServicioByID(int id)
        {
            IRepositoryServicio repository = new RepositoryServicio();
            return repository.GetServicioByID(id);
        }
        public SERVICIO Save(SERVICIO servicio)
        {
            IRepositoryServicio repository = new RepositoryServicio();
            return repository.Save(servicio);
        }
        public void DeleteServicio(int id)
        {
            throw new NotImplementedException();
        }
    }
}
