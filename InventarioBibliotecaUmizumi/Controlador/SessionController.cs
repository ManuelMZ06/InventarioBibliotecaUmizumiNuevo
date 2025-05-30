using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InventarioBibliotecaUmizumi.Modelo;

namespace InventarioBibliotecaUmizumi.Controlador
{
    public class SesionController
    {
        public static void RegistrarInicioSesion(int idUsuario)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "INSERT INTO SesionTemporal (IdUsuario, TipoAccion) VALUES (@IdUsuario, 'Inicio')";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void RegistrarCierreSesion(int idUsuario)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = "INSERT INTO SesionTemporal (IdUsuario, TipoAccion) VALUES (@IdUsuario, 'Cierre')";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}

