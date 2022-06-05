﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        USUARIO GetUsuarioByID(int id);
        USUARIO Save(USUARIO usuario);

        USUARIO GetUsuario(string correo, string contrasena);

        IEnumerable<USUARIO> GetUsuarioReserva();
    }
}
