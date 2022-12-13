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

namespace Cashin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_12; User Id=usuario_12; Password=9077879676;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                String query = "SELECT COUNT(*) FROM [db_12].[dbo].[User] WHERE ((email=@Username) AND (senha=@Password))";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", UsernameForm.Text);
                sqlCmd.Parameters.AddWithValue("@Password", PasswordForm.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    this.Opacity = 0.5;
                    Sucess.IsOpen = true;
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Sucess.IsOpen = false;
            Fail.IsOpen = false;
            this.Opacity = 1;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RegisterViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
