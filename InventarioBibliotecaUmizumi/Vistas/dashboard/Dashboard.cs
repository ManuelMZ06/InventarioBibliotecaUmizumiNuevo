using InventarioBibliotecaUmizumi.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioBibliotecaUmizumi.Vistas.dashboard
{
    public partial class Dashboard : Form
    {
        private int idUsuarioActivo;

        // Constructor que recibe el ID del usuario logueado
        public Dashboard(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuario;
            this.StartPosition = FormStartPosition.CenterScreen;


            // Opcional: puedes mostrar el ID o nombre de usuario en algún label
            // lblUsuarioActivo.Text = "ID Usuario: " + idUsuario;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Insertar en SesionTemporal con tipo 'Cierre' (activará el trigger)
            SesionController.RegistrarCierreSesion(idUsuarioActivo);

            // Volver al login
            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
            this.Close(); // Cierra este formulario después de regresar al login
        }
    }
}
