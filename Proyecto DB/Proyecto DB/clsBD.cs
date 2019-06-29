using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_DB
{
    class clsBD
    {
        SqlConnection Conn;
        SqlCommand Cmd;
        SqlTransaction Trans;
        string CadenaConexion = "";

        public bool AbrirConexion()
        {
            try
            {
                CadenaConexion = "server=PC-DYLAN\\SQLEXPRESS ; database=DW ; integrated security = true";
                Conn = new SqlConnection(CadenaConexion);
                Conn.Open();
                return true;
            }          
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la conexión:"+ ex.Message);
                return false;
            }
        }

        public bool Xcute(string pSQL)
        {
            try
            {
                AbrirConexion();

                Cmd = Conn.CreateCommand();
                Cmd.CommandText = pSQL;
                Cmd.Transaction = Trans;
                Cmd.ExecuteNonQuery();

                Conn.Close();

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el datatable:" + ex.Message);
                return false;
            }
        }

        public bool openDT(ref DataTable DT, string vSQL)
        {
            try
            {
                AbrirConexion();

                SqlDataAdapter DAdapter = new SqlDataAdapter();
                DataSet DSet = new DataSet();

                DAdapter = new SqlDataAdapter(vSQL, Conn);
                DSet = new DataSet();

                DAdapter.Fill(DSet, "Query");
                DT = DSet.Tables["Query"];

                Conn.Close();

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al abrir el datatable:" + ex.Message);
                return false;
            }
        }

    }
}
