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
    /// Interaction logic for AlterarLimite.xaml
    /// </summary>
    public partial class AlterarLimite : Window
    {
        public AlterarLimite()
        {
            InitializeComponent();
        }

        private void CadastrarLimite_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                float newLimite = (float)Convert.ToSingle(LimiteForm.Text);
                User.Limite = newLimite;

                string query = "UPDATE [dbo].[Users] SET Limit=@Limit WHERE ID=@Id";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Limit", Convert.ToSingle(LimiteForm.Text));
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
                    SucessLimit.IsOpen = true;
                }
                else
                {
                    SucessLimit.IsOpen = false;
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

        private void AlterarLimite_Close_Click(object sender, RoutedEventArgs e)
        {
            if (SucessLimit.IsOpen)
            {
                this.Close();
            }
            SucessLimit.IsOpen = false;
            this.Opacity = 1;
        }
    }
}
