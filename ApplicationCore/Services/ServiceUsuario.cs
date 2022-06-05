using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public USUARIO GetUsuario(string correo, string contrasena)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(contrasena);

            return repository.GetUsuario(correo, crytpPasswd);
        }

        public USUARIO GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            USUARIO oUsuario = repository.GetUsuarioByID(id);
            oUsuario.CONTRASENA = Cryptography.DecrypthAES(oUsuario.CONTRASENA);
            return oUsuario;
        }

        public USUARIO Save(USUARIO usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.CONTRASENA = Cryptography.EncrypthAES(usuario.CONTRASENA);
            return repository.Save(usuario);
        }

        public IEnumerable<USUARIO> GetUsuarioReserva()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioReserva();
        }
    }
}
