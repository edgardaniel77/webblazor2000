using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private string CadenaConexion;
        public LoginRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public async Task<bool> ValidarUsuraioAsync(Login login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Contrasena = @Contrasena;";
                valido = await _conexion.ExecuteScalarAsync<bool>(sql, login);



            }
            catch (Exception)
            {

            
            }
            return valido;

        }

        public Task<bool> ValidarUsuarioAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
