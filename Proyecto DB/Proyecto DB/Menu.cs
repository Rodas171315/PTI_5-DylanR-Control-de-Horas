using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_DB
{
    public partial class Menu : Form
    {
        clsBD BD = new clsBD();
        public bool CerrarSesion = false;

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerrarSesion)
            {
                string titulo = "Salir";
                string mensaje = "Desea cerrar la aplicación?";
                MessageBoxButtons btns = MessageBoxButtons.YesNo;
                DialogResult resultado;

                resultado = MessageBox.Show(this, mensaje, titulo, btns,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);

                if (resultado == DialogResult.No)
                {
                    CerrarSesion = false;
                    e.Cancel = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string titulo = "Cerrar Sesión";
            string mensaje = "Desea cerrar la sesión?";
            MessageBoxButtons btns = MessageBoxButtons.YesNo;
            DialogResult resultado;

            resultado = MessageBox.Show(this, mensaje, titulo, btns,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);

            if (resultado == DialogResult.Yes)
            {
                Hide();
                CerrarSesion = true;
                Login FrmLogin = new Login();
                FrmLogin.Show();
            }
            else{}
        }

        public Menu(string Nombres, string Apellidos, string Usuario)
        {
            InitializeComponent();
            lblUserName.Text = Nombres+" "+Apellidos;

            try
            {
                string cadena = " SELECT id_usuario as ID, genero AS Genero FROM usuario " +
                                " WHERE usuario = '"+ Usuario +"' ";
                DataTable DT = new DataTable();
                if (BD.openDT(ref DT, cadena))
                {
                    string G = DT.Rows[0].ItemArray[1].ToString();
                    string ID = DT.Rows[0].ItemArray[0].ToString();
                    if (G == "H")
                    {
                        picboxUser.Image = Properties.Resources.User_man;
                    }
                    else
                    {
                        picboxUser.Image = Properties.Resources.User_woman;
                    }

                    HorasAcd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión " + ex.Message);
            }
        }

        private void HorasAcd()
        {
            try
            {
                string cadena2 = " SELECT ISNULL(SUM(horas_acumuladas),0) as Horas FROM control_de_horas " +
                                 " WHERE id_usuario = '" + clsGlobal.IdUsuario + "' ";
                DataTable DT2 = new DataTable();
                if (BD.openDT(ref DT2, cadena2))
                {
                    double HorasAcd = (double)DT2.Rows[0][0];
                    if (HorasAcd == 0)
                    {
                        lblHoras.Text = "0";
                    }
                    else { lblHoras.Text = HorasAcd.ToString(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión " + ex.Message);
            }
        }

        private void controlDeHorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlHoras FrmControlHoras = new ControlHoras();
            FrmControlHoras.Show();
        }

        private void detallesDelSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info FrmDetalles = new Info();
            FrmDetalles.Show();
        }

        private void btnIngreso_Click(object sender, EventArgs e)
        {
            string titulo = "Registro de Ingreso";
            string mensaje = "Desea registrar su ingreso?";
            MessageBoxButtons btns = MessageBoxButtons.YesNo;
            DialogResult resultado;

            resultado = MessageBox.Show(this, mensaje, titulo, btns,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);

            if (resultado == DialogResult.Yes)
            {
                string cadmax = "SELECT MAX(id_control_de_horas) FROM control_de_horas";
                DataTable DT = new DataTable();

                if (BD.openDT(ref DT, cadmax))
                {
                    int max = (int)DT.Rows[0][0];
                    int ID = max + 1;

                    string fecha = DateTime.Now.ToString("yyyyMMdd");
                    string hora = DateTime.Now.ToString("HH:mm:ss");

                    string cadena = "INSERT INTO control_de_horas (id_control_de_horas, id_usuario, fecha_ingreso, hora_ingreso) " +
                                     "VALUES( "+ ID +" , '"+ clsGlobal.IdUsuario +"', '"+ fecha +"', '"+ hora +"'); ";
                    BD.Xcute(cadena);
                    DataTable DT2 = new DataTable();

                    cadena = "SELECT hora_ingreso FROM control_de_horas " +
                             "WHERE id_control_de_horas = " + ID + "; ";
                    if (BD.openDT(ref DT2, cadena))
                    {
                         string Ingreso = DT2.Rows[0].ItemArray[0].ToString();
                         lblIngreso.Text = Ingreso.ToString();
                    }
                }
            }
            else{}
        }
        
        private void btnSalida_Click(object sender, EventArgs e)
        {
            string titulo = "Registro de Salida";
            string mensaje = "Desea registrar su salida?";
            MessageBoxButtons btns = MessageBoxButtons.YesNo;
            DialogResult resultado;

            resultado = MessageBox.Show(this, mensaje, titulo, btns,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);

            if (resultado == DialogResult.Yes)
            {

                string max = "SELECT MAX(id_control_de_horas) FROM control_de_horas "+
                             "WHERE id_usuario = '"+ clsGlobal.IdUsuario +"' ";
                DataTable DT = new DataTable();

                if (BD.openDT(ref DT, max))
                {
                    if (DT.Rows.Count > 0)
                    {
                        int maximo = (int)DT.Rows[0][0];
                        string fecha = DateTime.Now.ToString("yyyyMMdd");
                        string hora = DateTime.Now.ToString("HH:mm:ss");

                        string cadena = "UPDATE control_de_horas SET fecha_salida = '" + fecha + "', hora_salida = '" + hora + "', " +
                                        "horas_acumuladas = DATEDIFF(HH,convert(NVARCHAR(10),fecha_ingreso) + ' ' + convert(NVARCHAR(10),hora_ingreso), " +
                                        "convert(NVARCHAR(10), '"+fecha+ "') + ' ' + convert(NVARCHAR(10), '" + hora + "'))" +
                                        "WHERE id_control_de_horas = " + maximo + " ; ";
                        BD.Xcute(cadena);

                        DataTable DT2 = new DataTable();

                        cadena = "SELECT hora_salida FROM control_de_horas  " +
                                 "WHERE id_control_de_horas = " + maximo + "; ";
                        if (BD.openDT(ref DT2, cadena))
                        {
                            string Salida = DT2.Rows[0].ItemArray[0].ToString();
                            lblSalida.Text = Salida.ToString();
                            HorasAcd();
                        }
                    }
                }
            }
            else{}
        }

        private string DameFecha()
        {
            string fecha = "", mes, dia;
            DateTime Tiempo = DateTime.Now;

            if (Tiempo.Month.ToString().Length == 1)
            {
                mes = "0" + Tiempo.Month.ToString();
            }
            else
            {
                mes = Tiempo.Month.ToString();
            }

            if (Tiempo.Day.ToString().Length == 1)
            {
                dia = "0" + Tiempo.Day.ToString();
            }
            else
            {
                dia = Tiempo.Day.ToString();
            }

            fecha = Tiempo.Year.ToString() + mes + dia;

            return fecha;
        }
    }
}
