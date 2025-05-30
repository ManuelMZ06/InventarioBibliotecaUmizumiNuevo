using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBibliotecaUmizumi.Modelo
{
    public class Conexion
    {
        // Cadena de conexión a SQL Server
       
        private static string cadenaConexion = "Server=PC-HUGO\\SQLEXPRESS;Database=bd_inventario;User Id=laraveluser;Password=Laravel2025$;";


        // Método para obtener la conexión
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        // Método para cerrar conexión (opcional pero útil para mantener buenas prácticas)
        public static void CerrarConexion(SqlConnection conexion)
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}