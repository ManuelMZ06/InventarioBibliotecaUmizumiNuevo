using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InventarioBibliotecaUmizumi.Modelo;


namespace InventarioBibliotecaUmizumi.Controlador
{
    public class UsuarioController
    {
        public static string RegistrarUsuario(string usuario, string password)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    // IMPORTANTE: el idRol se obtiene automáticamente de la tabla Roles para "Empleado"
                    string query = @"
                        INSERT INTO Usuarios (usuario, password, idRol)
                        VALUES (
                            @usuario, 
                            dbo.protect(@password), 
                            (SELECT TOP 1 idRol FROM Roles WHERE NombreRol = 'Empleado')
                        );";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@password", password); // Texto plano, se encripta en la función
                        comando.ExecuteNonQuery();
                    }

                    return "✅ Usuario registrado correctamente.";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UNIQUE") || ex.Message.Contains("duplicate"))
                    return "⚠️ Este nombre de usuario ya está registrado.";
                else if (ex.Message.Contains("valor devuelto por protect"))
                    return "❌ La contraseña no cumple con los requisitos de seguridad.";
                return "❌ Error de SQL: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "❌ Error general: " + ex.Message;
            }
        }

        public static bool ValidarLogin(string usuario, string password)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT dbo.validar_login(@usuario, @password)";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@password", password);

                        int resultado = Convert.ToInt32(comando.ExecuteScalar());
                        return resultado == 1;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static int ObtenerIdSiLoginValido(string usuario, string password)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdUsuario FROM Usuarios WHERE usuario = @usuario AND dbo.validar_login(@usuario, @password) = 1";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@password", password);

                    object result = comando.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }


    }
}

