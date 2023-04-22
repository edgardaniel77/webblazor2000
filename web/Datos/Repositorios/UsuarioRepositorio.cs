﻿using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string CadenaConexion;
        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"UPDATE usuario SET Nombre = @Nombre, Contrasena = @Contrasena, Correo = @Correo, Rol = @Rol, Foto = @Foto, EstaActivo = @EstaActivo
                                WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
                bool resultado = false;
                try
                {
                    using MySqlConnection _conexion = Conexion();
                    await _conexion.OpenAsync();
                    string sql = "DELETE FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                    resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { codigo }));
                }
                catch (Exception)
                {
                }
                return resultado;
            
        }

        public async Task<IEnumerable<Usuario>> GetListaAsync()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                lista = await _conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public async Task<Usuario> GetPorCodigoAsync(string codigo)
        {
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                user = await _conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
            }
            catch (Exception)
            {
            }
            return user;
        }

        public async Task<bool> NuevoAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO usuario (CodigoUsuario,Nombre,Contrasena,Correo,Rol,Foto,FechaCreacion,EstaActivo)
                                 VALUES (@CodigoUsuario,@Nombre,@Contrasena,@Correo,@Rol,@Foto,@FechaCreacion,@EstaActivo);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }
    }
}
