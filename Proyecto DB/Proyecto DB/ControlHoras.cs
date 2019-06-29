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
using Excel = Microsoft.Office.Interop.Excel;

namespace Proyecto_DB
{
    public partial class ControlHoras : Form
    {
        clsBD BD = new clsBD();
        
        public ControlHoras()
        {
            InitializeComponent();
        }

        private void ControlHoras_Load(object sender, EventArgs e)
        {
            try
            {
                string cadena = "SELECT x.id_control_de_horas as ID, x.id_usuario as ID_Usuario, " +
                                "y.nombres + ' ' + y.apellidos as Usuario, x.Fecha_Ingreso as 'Fecha Ingreso', " +
                                "x.hora_ingreso as 'Hora Ingreso', " +
                                "x.Fecha_Salida as 'Fecha Salida'," +
                                "x.hora_salida as 'Hora Salida', " +
                                "x.horas_acumuladas as 'Horas Acd. Diaria' " +
                                "FROM control_de_horas x inner join usuario y on x.id_usuario = y.id_usuario";
                DataTable DT = new DataTable();

                if (BD.openDT(ref DT, cadena))
                {
                    dgvHoras.DataSource = DT;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Mis Registros")
            {
                try
                {
                    string cadena = "SELECT x.id_control_de_horas as ID, x.id_usuario as ID_Usuario, " +
                                    "y.nombres + ' ' + y.apellidos as Usuario, x.Fecha_Ingreso as 'Fecha Ingreso', " +
                                    "x.hora_ingreso as 'Hora Ingreso', " +
                                    "x.Fecha_Salida as 'Fecha Salida'," +
                                    "x.hora_salida as 'Hora Salida', " +
                                    "x.horas_acumuladas as 'Horas Acd. Diaria' " +
                                    "FROM control_de_horas x inner join usuario y on x.id_usuario = y.id_usuario " +
                                    "WHERE x.id_usuario = '" + clsGlobal.IdUsuario + "' ";
                    DataTable DT = new DataTable();

                    if (BD.openDT(ref DT, cadena))
                    {
                        dgvHoras.DataSource = DT;
                    }
                    btnBuscar.Text = "Todos";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la conexión " + ex.Message);
                }
            }
            else
            {
                try
                {
                    string cadena = "SELECT x.id_control_de_horas as ID, x.id_usuario as ID_Usuario, " +
                                    "y.nombres + ' ' + y.apellidos as Usuario, x.Fecha_Ingreso as 'Fecha Ingreso', " +
                                    "x.hora_ingreso as 'Hora Ingreso', " +
                                    "x.Fecha_Salida as 'Fecha Salida'," +
                                    "x.hora_salida as 'Hora Salida', " +
                                    "x.horas_acumuladas as 'Horas Acd. Diaria' " +
                                    "FROM control_de_horas x inner join usuario y on x.id_usuario = y.id_usuario ";

                    DataTable DT = new DataTable();

                    if (BD.openDT(ref DT, cadena))
                    {
                        dgvHoras.DataSource = DT;
                    }
                    btnBuscar.Text = "Mis Registros";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la conexión " + ex.Message);
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Mis Registros")
            {
                try
                {
                    string cadena = "SELECT x.id_control_de_horas as ID, x.id_usuario as ID_Usuario, " +
                                    "y.nombres + ' ' + y.apellidos as Usuario, x.Fecha_Ingreso as 'Fecha Ingreso', " +
                                    "x.hora_ingreso as 'Hora Ingreso', " +
                                    "x.Fecha_Salida as 'Fecha Salida'," +
                                    "x.hora_salida as 'Hora Salida', " +
                                    "x.horas_acumuladas as 'Horas Acd. Diaria' " +
                                    "FROM control_de_horas x inner join usuario y on x.id_usuario = y.id_usuario " +
                                    "WHERE x.id_usuario = '"+ clsGlobal.IdUsuario +"' ";
                    DataTable DT = new DataTable();

                    if (BD.openDT(ref DT, cadena))
                    {
                        ExportToExcel();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la conexión " + ex.Message);
                }
            }
            else
            {
                try
                {
                    string cadena = "SELECT x.id_control_de_horas as ID, x.id_usuario as ID_Usuario, " +
                                    "y.nombres + ' ' + y.apellidos as Usuario, x.Fecha_Ingreso as 'Fecha Ingreso', " +
                                    "x.hora_ingreso as 'Hora Ingreso', " +
                                    "x.Fecha_Salida as 'Fecha Salida'," +
                                    "x.hora_salida as 'Hora Salida', " +
                                    "x.horas_acumuladas as 'Horas Acd. Diaria' " +
                                    "FROM control_de_horas x inner join usuario y on x.id_usuario = y.id_usuario ";

                    DataTable DT = new DataTable();

                    if (BD.openDT(ref DT, cadena))
                    {
                        ExportToExcel();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la conexión " + ex.Message);
                }
            }
        }
        
        private void ExportToExcel()
        {
            Excel._Application excel = new Excel.Application();
            Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;

            barExcel.Value = 0;
            for (int b = 0; b < 100; b++)
            {
                barExcel.Value += 1;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                worksheet.Name = "Control de Horas";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                for (int i = -1; i < dgvHoras.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvHoras.Columns.Count; j++)
                    {
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvHoras.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvHoras.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FilterIndex = 1;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Descarga Completa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }

    }
}
