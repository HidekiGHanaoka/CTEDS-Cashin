using Cashin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cashin
{
    /// <summary>
    /// Interaction logic for AlterarSaldo.xaml
    /// </summary>
    public partial class AlterarSaldo : Window
    {
        public AlterarSaldo()
        {
            InitializeComponent();
        }

        private void CadastrarSaldo_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                float newSaldo = Convert.ToSingle(SaldoForm.Text);
                User.Saldo = newSaldo;

                string query = "UPDATE [dbo].[Users] SET Balance=@Balance WHERE ID=@Id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Balance", Convert.ToSingle(SaldoForm.Text));
                sqlCmd.Parameters.AddWithValue("@ID", User.Id);
                int count = 0;
                try
                {
                    sqlCmd.ExecuteScalar();
                    count = 1;
                }
                catch
                {
                    count = 0;
                }
                if (count == 1)
                {
                    SucessBalance.IsOpen = true;
                }
                else
                {
                    SucessBalance.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void AlterarSaldo_Close_Click(object sender, RoutedEventArgs e)
        {
            if (SucessBalance.IsOpen)
            {
                this.Close();
            }
            SucessBalance.IsOpen = false;
            this.Opacity = 1;

        }
    }
}
