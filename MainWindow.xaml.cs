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
using System.Runtime.CompilerServices;

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

                    String query2 = "SELECT ID, Name, Email, Balance, Limit FROM [dbo].[Users] WHERE Email=@Email AND Password=@Password";
                    SqlCommand cmd = new SqlCommand(query2, con);
                    cmd.Parameters.AddWithValue("@Email", EmailForm.Text);
                    cmd.Parameters.AddWithValue("@Password", PasswordForm.Password);
                    SqlDataReader rdr;

                    using (cmd)
                    {
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            User.Id = (Guid)rdr["ID"];
                            User.Nome = rdr["Name"].ToString();
                            User.Email = rdr["Email"].ToString();
                            User.Saldo = Convert.ToSingle(rdr["Balance"]);
                            User.Limite = Convert.ToSingle(rdr["Limit"]);
                        }
                        rdr.Close();
                    }

                    String query3 = "SELECT * FROM [Transactions] WHERE ID=@Id";
                    SqlCommand cmd2 = new SqlCommand(query3, con);
                    cmd2.Parameters.AddWithValue("@Id", User.Id);
                    SqlDataReader rdr2;

                    using (cmd2)
                    {
                        rdr2 = cmd2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            Item NewItem = new Item()
                            {
                                Id = (Guid)rdr2["ID"],
                                Titulo = rdr2["Title"].ToString(),
                                Valor = Convert.ToSingle(rdr2["Value"]),
                                Descricao = rdr2["Description"].ToString(),
                                Tipo = rdr2["Type"].ToString(),
                                Categoria = rdr2["Category"].ToString()
                            };
                            User.ListaItens.Add(NewItem);
                        }
                        rdr2.Close();
                    }
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
            if (Sucess.IsOpen == true)
            {
                AccountWindow window = new AccountWindow();
                window.Show();
                this.Close();
            }
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
