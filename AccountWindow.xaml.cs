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
using Cashin.Models;

namespace Cashin
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
            BoxLimit.Text = $"R$ {User.Limite}";
            BoxBalance.Text = $"R$ {User.Saldo}";
            User.ListaItens.Reverse();
            foreach (Item _item in User.ListaItens)
            {
                Button _button = new Button();
                _button.Height= 75;
                _button.FontSize= 20;
                _button.Background = Brushes.White;
                _button.Cursor= Cursors.Hand;
                _button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                PainelDeTransacoes.Children.Add(_button);

                StackPanel _stackPanel1 = new StackPanel();
                _stackPanel1.Orientation = Orientation.Horizontal;
                _stackPanel1.Margin = new Thickness(0, 0, 0, 0);
                _stackPanel1.HorizontalAlignment= HorizontalAlignment.Stretch;
                _button.Content = (_stackPanel1);

                StackPanel _stackPanel2 = new StackPanel();
                _stackPanel2.Orientation = Orientation.Vertical;
                _stackPanel2.Margin = new Thickness(20, 0, 0, 0);
                _stackPanel2.Width= 250;
                _stackPanel2.VerticalAlignment = VerticalAlignment.Center;
                _stackPanel2.HorizontalAlignment= HorizontalAlignment.Left;
                _stackPanel1.Children.Add(_stackPanel2);

                TextBlock _textbox1 = new TextBlock();
                _textbox1.Text = $"{_item.Titulo}";
                _textbox1.FontSize = 26;
                _stackPanel2.Children.Add(_textbox1);

                TextBlock _textbox2 = new TextBlock();
                _textbox2.Text = $"{_item.Categoria}";
                _textbox2.FontSize = 14;
                _stackPanel2.Children.Add(_textbox2);

                TextBlock _textbox3 = new TextBlock();
                if (_item.Tipo == "Recebimento")
                {
                    _textbox3.Text = $"+ R$ {_item.Valor}";
                }
                else
                {
                    _textbox3.Text = $"- R$ {_item.Valor}";
                }
                _textbox3.FontSize = 30;
                _textbox3.VerticalAlignment = VerticalAlignment.Center;
                _textbox3.HorizontalAlignment = HorizontalAlignment.Center;
                _textbox3.Width = 300;
                _textbox3.Margin = new Thickness(0, 0, 0, 0);
                _stackPanel1.Children.Add(_textbox3);

                TextBlock _textbox4 = new TextBlock();
                _textbox4.Text = $"{_item.Descricao}";
                _textbox4.Width= 300;
                _textbox4.FontSize = 14;
                _textbox4.TextWrapping= TextWrapping.Wrap;
                _textbox4.VerticalAlignment = VerticalAlignment.Center;
                _textbox4.HorizontalAlignment= HorizontalAlignment.Right;
                _textbox4.Margin = new Thickness(40, 0, 0, 0);
                _stackPanel1.Children.Add(_textbox4);
            }
            User.ListaItens.Reverse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItem window = new AddItem();
            window.ShowDialog();
        }

        private void AlterarSaldo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AlterarLimite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
