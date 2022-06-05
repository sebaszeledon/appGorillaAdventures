using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceExtra : IServiceExtra
    {
        public IEnumerable<EXTRA> GetExtra()
        {
            IRepositoryExtra repository = new RepositoryExtra();
            return repository.GetExtra();
        }

        public EXTRA GetExtraByID(int id)
        {
            IRepositoryExtra repository = new RepositoryExtra();
            return repository.GetExtraByID(id);
        }
    }
}
