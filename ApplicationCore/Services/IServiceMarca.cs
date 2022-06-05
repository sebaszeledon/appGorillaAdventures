using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMarca
    {
        IEnumerable<MARCA> GetMarca();
        MARCA GetMarcaByID(int id);
        void DeleteMarca(int id);
        MARCA Save(MARCA marca);
    }
}
