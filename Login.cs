using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModuloEntidad;
using ModuloNegocio;


namespace ModuloPresentacion
{
    public partial class Login : Form
    {
        private int intentosFallidos = 0;
        private int intentosMaximos = 3;
        public Login()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            //aqui lo de la lista que aun no esta
            List<Usuario> TEST = new CN_Usuario().Listar();

            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.DocumentoUsuario == txtDocumento.Text && u.Password == txtPassword.Text).FirstOrDefault();

            if (oUsuario != null)
            {
                intentosFallidos = 0;

                Inicio form = new Inicio(oUsuario);

                form.Show();
                this.Hide();

                form.FormClosing += formClosing;
            }
            else
            {
                intentosFallidos++;  // Incrementa el contador de intentos fallidos

                if (intentosFallidos > intentosMaximos)
                {
                    MessageBox.Show("Ha alcanzado el máximo de intentos. Consulte con el administrador.");
                    BtnIngresar.Enabled = false;  // deshabilitar el botón de ingreso
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario. le quedan " + (--intentosMaximos) +" Intentos");
                }
            }
        }
        
        private void formClosing(object sender, FormClosingEventArgs e)
        {
            txtDocumento.Text = "";
            txtPassword.Text = "";

            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
