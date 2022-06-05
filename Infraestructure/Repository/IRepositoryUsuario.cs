using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        USUARIO GetUsuarioByID(int id);
        USUARIO Save(USUARIO usuario);

        USUARIO GetUsuario(string correo, string contrasena);

        IEnumerable<USUARIO> GetUsuarioReserva();
    }
}
