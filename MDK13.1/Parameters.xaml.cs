using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Parameters.xaml
    /// </summary>
    public partial class Parameters : Window
    {
        public Parameters()
        {
            InitializeComponent();
        }

        private void save(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowManager.Columns = Convert.ToInt32(tbColumns.Text);
                WindowManager.Rows = Convert.ToInt32(tbRows.Text);

                StreamWriter file = new("Files/config.ini");
                
                file.WriteLine(tbRows.Text);
                file.WriteLine(tbColumns.Text);
                
                file.Close();

                tbRows.Clear();
                tbColumns.Clear();

                MessageBox.Show("Сохранение прошло успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
