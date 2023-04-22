using Modelos;

namespace Blazor.Interfaces
{
    public interface ILoginServicio
    {
        Task<bool> ValidarUsuarioAsync(Login login);
    }
}
