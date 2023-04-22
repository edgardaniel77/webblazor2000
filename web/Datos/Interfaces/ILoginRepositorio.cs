using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Datos.Interfaces
{
    public interface ILoginRepositorio
    {
        Task<bool> ValidarUsuarioAsync(Login login);
        
    }
}
