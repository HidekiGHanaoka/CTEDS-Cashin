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
    public partial class AddItem : Window
    {
        Context ctx = new Context();
        public AddItem()
        {
            InitializeComponent();
        }
        private void NewItemDB_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                String queryCreateTransaction = "INSERT INTO [dbo].[Transactions]([ID],[Title],[Value],[Description],[Date],[Type],[Category]) VALUES (@ID, @Title, @Value, @Description, @Date, @Type, @Category)";
                SqlCommand sqlCmdCreateTransaction = new SqlCommand(queryCreateTransaction, con);
                sqlCmdCreateTransaction.CommandType = System.Data.CommandType.Text;
                sqlCmdCreateTransaction.Parameters.AddWithValue("@ID", ctx.User.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Date", System.DateTime.Now.ToShortDateString());
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Title", TituloForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Value", ValorForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Category", CategoriaForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Description", DescricaoForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Type", TipoForm.Text);
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

