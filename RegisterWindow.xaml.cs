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

namespace Cashin
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Page
    {
        public RegisterWindow()
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
                String query = "SELECT COUNT(*) FROM [db_12].[dbo].[User] WHERE email=@Username";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", EmailForm.Text); //Username passado no WPF
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    this.Opacity = 0.5;
                    AlreadyRegistered.IsOpen = true; //Popup
                }
                else
                {
                    this.Opacity = 0.5;
                    SucessRegister.IsOpen = true; // Popup
                    String queryCreateUser = "INSERT INTO [db_12].[dbo].[User] VALUES (@Email, @Username, @Password)";
                    SqlCommand sqlCmdRegisterUser = new SqlCommand(queryCreateUser, con);
                    sqlCmdRegisterUser.CommandType = System.Data.CommandType.Text;
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Username", UsernameForm.Text); //Username passado no WPF
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Email", EmailForm.Text); //Username passado no WPF
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Password", PasswordForm.Password); //Senha passada no WPF
                    sqlCmdRegisterUser.ExecuteScalar();

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
            if (SucessRegister.IsOpen == true) {
                AccountWindow page = new AccountWindow();
                this.Content = page;
            };
            SucessRegister.IsOpen = false;
            AlreadyRegistered.IsOpen = false;
            this.Opacity = 1;
        }
    }
}
