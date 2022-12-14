using Cashin.Controllers;
using Cashin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
                Guid id = User.Id;
                DateTime data = DateTime.Now;
                string titulo = TituloForm.Text;
                string categoria = CategoriaForm.Text;
                string desc = DescricaoForm.Text;
                string tipo = TipoForm.Text;
                float valor = Convert.ToSingle(ValorForm.Text);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@ID", id);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Date", data);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Title", titulo);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Value", valor);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Category", categoria);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Description", desc);
                sqlCmdCreateTransaction.Parameters.AddWithValue("@Type", tipo);
                int count = 0;
                try
                {
                    sqlCmdCreateTransaction.ExecuteScalar();
                    count = 1;
                }
                catch
                {
                    count = 0;
                }
                if (count == 1)
                {
                    BalanceController.ActualBalance(tipo, valor);
                    LimitController.ActualLimit(tipo, valor);
                    Item newItem = new()
                    {
                        Id = id,
                        Titulo = titulo,
                        Valor = valor,
                        Descricao = desc,
                        Tipo = tipo,
                        Categoria = categoria
                    };
                    User.ListaItens.Add(newItem);
                    SucessTransaction.IsOpen = true;
                }
                else
                {
                    SucessTransaction.IsOpen = false;
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
        private void AddItem_Close_Click(object sender, RoutedEventArgs e)
        {
            if (SucessTransaction.IsOpen)
            {
                this.Close();
            }
            SucessTransaction.IsOpen = false;

            this.Opacity = 1;
        }
    }
} 

