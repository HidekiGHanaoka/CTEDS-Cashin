﻿using System;
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
