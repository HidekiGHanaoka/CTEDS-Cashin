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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cashin.Views
{
    /// <summary>
    /// Interaction logic for NewItemView.xaml
    /// </summary>
    public partial class NewItemView : UserControl
    {
        public NewItemView()
        {
            InitializeComponent();
        }

        private void RegisterDB_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                String queryCreateTransaction = "INSERT INTO [db_12].[dbo].[Transações] ([email],[data],[valor],[categoria],[descrição],[tipo]) VALUES(@email, @data, @valor, @categoria, @descrição, @tipo)";
                SqlCommand sqlCmdCreateTransaction = new SqlCommand(queryCreateTransaction, con);
                sqlCmdCreateTransaction.CommandType = System.Data.CommandType.Text;
                sqlCmdCreateTransaction.Parameters.AddWithValue("@email", email.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@data", System.DateTime.Now.ToShortDateString()); 
                sqlCmdCreateTransaction.Parameters.AddWithValue("@valor", ValorForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@categoria", CategoriaForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@descrição", DescricaoForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@tipo", TipoForm.Text);
                sqlCmdCreateTransaction.ExecuteScalar();
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
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SucessTransaction.IsOpen = false;
            this.Opacity = 1;
        }
    }
}
