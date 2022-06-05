using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMarca : IServiceMarca
    {
        
        public IEnumerable<MARCA> GetMarca()
        {
            IRepositoryMarca repository = new RepositoryMarca();
            return repository.GetMarca();
        }

        public MARCA GetMarcaByID(int id)
        {
            IRepositoryMarca repository = new RepositoryMarca();
            return repository.GetMarcaByID(id);
        }

        public MARCA Save(MARCA marca)
        {
            IRepositoryMarca repository = new RepositoryMarca();
            return repository.Save(marca);
        }

        public void DeleteMarca(int id)
        {
            throw new NotImplementedException();
        }
    }
}
