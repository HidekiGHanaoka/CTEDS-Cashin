using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cashin.ViewModels;
using Cashin.Models;

namespace Cashin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context ctx = new Context();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                String query = "SELECT COUNT(*) FROM [dbo].[Users] WHERE Email=@Email AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Email", EmailForm.Text);
                sqlCmd.Parameters.AddWithValue("@Password", PasswordForm.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if (count == 1)
                {
                    this.Opacity = 0.5;
                    Sucess.IsOpen = true;
                    SqlDataReader rdr;

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            ctx.ActualUser = new User()
                            {
                                Nome = rdr["username"].ToString(),
                                Email = rdr["email"].ToString(),
                                Saldo = (float)rdr["saldo"],
                                Limite = (float)rdr["limite"]
                            };
                        }
                    }
                    //String query2 = "SELECT * FROM [Transactions] WHERE ("

                }
                else
                {
                    this.Opacity = 0.5;
                    Fail.IsOpen = true;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow window = new AccountWindow();
            window.Show();
            this.Close();
            Sucess.IsOpen = false;
            Fail.IsOpen = false;
            this.Opacity = 1;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
