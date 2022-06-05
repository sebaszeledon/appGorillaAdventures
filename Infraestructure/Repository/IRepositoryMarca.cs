using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryMarca
    {
        IEnumerable<MARCA> GetMarca();
        MARCA GetMarcaByID(int id);
        void DeleteMarca(int id);
        MARCA Save(MARCA marca);
    }
}
