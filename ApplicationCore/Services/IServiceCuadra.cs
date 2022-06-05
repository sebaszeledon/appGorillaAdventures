using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCuadra
    {
        IEnumerable<CUADRACICLO> GetCuadra();
        IEnumerable<CUADRACICLO> GetCuadraByMarca(int idMarca);
        IEnumerable<CUADRACICLO> GetCuadraByExtra(int idExtra);
        CUADRACICLO GetCuadraById(int id);
        void DeleteCuadra(int id);
        CUADRACICLO Save(CUADRACICLO cuadra, string[] selectedExtras);
    }
}
