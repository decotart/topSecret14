using System;
using System.Collections.Generic;
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

namespace MDK13._1
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
        }

        const string Password = "123";

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == Password)
            {
                MainWindow mw = new();
                mw.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пароль не верный");
                tbPass.Clear();
            }
        }
    }
}
