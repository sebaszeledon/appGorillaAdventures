using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCuadra : IServiceCuadra
    {
        public void DeleteCuadra(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CUADRACICLO> GetCuadra()
        {
            IRepositoryCuadra repository = new RepositoryCuadra();
            return repository.GetCuadra();
        }

        public IEnumerable<CUADRACICLO> GetCuadraByExtra(int idExtra)
        {
            throw new NotImplementedException();
        }

        public CUADRACICLO GetCuadraById(int id)
        {
            IRepositoryCuadra repository = new RepositoryCuadra();
            return repository.GetCuadraById(id);
        }

        public IEnumerable<CUADRACICLO> GetCuadraByMarca(int idMarca)
        {
            throw new NotImplementedException();
        }

        public CUADRACICLO Save(CUADRACICLO cuadra, string[] selectedExtras)
        {
            IRepositoryCuadra repository = new RepositoryCuadra();
            return repository.Save(cuadra, selectedExtras);
        }
    }
}
