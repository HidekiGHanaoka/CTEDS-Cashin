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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterDB_Click(object sender, RoutedEventArgs e)
        {
            string stringConexao = "Server=SERVER; Initial Catalog=DB; User Id=User; Password=Pass;";
            SqlConnection con = new SqlConnection(stringConexao);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                String query = "SELECT COUNT(*) FROM [dbo].[Users] WHERE Email=@Email";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Email", EmailForm.Text);
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
                    String queryCreateUser = "INSERT INTO [dbo].[Users]([ID],[Name],[Email],[Password],[Balance],[Limit]) VALUES (@ID, @Name, @Email, @Password, @Balance, @Limit)";
                    SqlCommand sqlCmdRegisterUser = new SqlCommand(queryCreateUser, con);
                    sqlCmdRegisterUser.CommandType = System.Data.CommandType.Text;
                    sqlCmdRegisterUser.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Name", NameForm.Text);
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Email", EmailForm.Text);
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Password", PasswordForm.Password);
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Balance", Convert.ToSingle(0));
                    sqlCmdRegisterUser.Parameters.AddWithValue("@Limit", Convert.ToSingle(0));
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
            if (SucessRegister.IsOpen)
            {
                this.Close();
            }
            SucessRegister.IsOpen = false;
            AlreadyRegistered.IsOpen = false;
            this.Opacity = 1;
        }
    }
}
