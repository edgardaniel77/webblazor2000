using Modelos;

namespace Blazor.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<Usuario> GetPorCodigoAsync(string codigo);
        Task<bool> NuevoAsync(Usuario usuario);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<Usuario>> GetListaAsync();
    }
}
