using InventarioBibliotecaUmizumi.Controlador;
using InventarioBibliotecaUmizumi.Modelo;
using InventarioBibliotecaUmizumi.Vistas.Autenticacion;
using InventarioBibliotecaUmizumi.Vistas.dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioBibliotecaUmizumi
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LinkRegistrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormRegistro formregistro = new FormRegistro();
            formregistro.ShowDialog();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor llena todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esValido = UsuarioController.ValidarLogin(usuario, password);

            if (esValido)
            {
                MessageBox.Show("✅ Bienvenido, acceso concedido.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Obtener el ID del usuario
                int idUsuario = UsuarioController.ObtenerIdSiLoginValido(usuario, password);

                if (idUsuario > 0)
                {
                    // Registrar inicio en la bitácora a través del trigger
                    SesionController.RegistrarInicioSesion(idUsuario);

                    // Redireccionar al dashboard
                    this.Hide();
                    Dashboard tableroPrincipal = new Dashboard(idUsuario); // Se pasa el ID del usuario al Dashboard
                    tableroPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error inesperado al recuperar el ID del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("❌ Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
