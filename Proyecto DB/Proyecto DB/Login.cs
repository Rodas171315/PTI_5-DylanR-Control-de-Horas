using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_DB
{
    public partial class Login : Form
    {
        string User = SystemInformation.UserName;
        string Domain = SystemInformation.UserDomainName;
        clsBD BD = new clsBD();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnIngreso_Click(object sender, EventArgs e) 
        {
            try
            {
                string cadena = " SELECT id_usuario, nombres, apellidos, usuario FROM usuario " +
                                " WHERE usuario = '"+ txtUser.Text +"'" +
                                " and contraseña = '"+ txtPwd.Text +"'";
                DataTable DT = new DataTable();

                if (BD.openDT(ref DT, cadena))
                {
                    string Nombres = DT.Rows[0].ItemArray[1].ToString();
                    string Apellidos = DT.Rows[0].ItemArray[2].ToString();
                    string Usuario = DT.Rows[0].ItemArray[3].ToString();

                    clsGlobal.IdUsuario =(int) DT.Rows[0][0];

                    Menu FrmMenu = new Menu(Nombres, Apellidos, Usuario);
                    Hide();
                    FrmMenu.ShowDialog();            
                    if (!FrmMenu.CerrarSesion)
                    {
                        Application.Exit();
                    }                   
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al abrir la conexión " + ex.Message);
                MessageBox.Show("Por favor, ingrese un usuario y contraseña válidos");
            }
        }
        
        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUser.Text != "")
                {
                    txtPwd.Focus();
                }
                else
                {
                    MessageBox.Show("Ingrese su Usuario");
                    txtUser.Focus();
                }
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtPwd.Text != "")
                    {
                        btnIngreso_Click(sender,e);
                    }
                    else
                    {
                        MessageBox.Show("Ingrese su Contraseña");
                        txtPwd.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión " + ex.Message);
            }
        }
    }
}
