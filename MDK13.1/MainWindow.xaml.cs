using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using libmas;

namespace MDK13._1
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

        int[,] matrix;
        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Агальцов Даниил");
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            rows.Clear();
            columns.Clear();
            rez.Clear();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Items.Count != 0)
            {
                Libmas.Save(matrix);
            }
            else MessageBox.Show("Таблица не была создана", "Упс...", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            try
            {
                matrix = Libmas.Open();
                dataGrid.ItemsSource = Libmas.ToDataTable(matrix).DefaultView;

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Упс...", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {

            bool f = Int32.TryParse(rows.Text, out int row);
            bool f1 = Int32.TryParse(columns.Text, out int column);
            if (f && f1 && row > 0 && column > 0)
            {
                matrix = new int[row, column];
                Random random = new Random();
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                        matrix[i, j] = random.Next(-10, 30);// заполняем матрицу
                }
                dataGrid.ItemsSource = Libmas.ToDataTable(matrix).DefaultView;
            }
            else MessageBox.Show("Ошибка!\rЗаполните ячейки целыми положительными числами", "Упс...", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Items.Count != 0)
            {
                int columnCount = Libmas.Searches(matrix);
                if (columnCount != -1)
                    rez.Text = $"Колонка {columnCount + 1}";
                else
                    rez.Text = "0";
            }
            else MessageBox.Show("Ошибка!\rПожалуйста, нажмите кнопку заполнить", "Упс...", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        DispatcherTimer timer;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new();
            timer.Tick += Timer_Tick;
            timer.Interval = new(0, 0, 0, 1, 0);
            timer.IsEnabled = true;

            try
            {
                StreamReader file = new("config.ini");

                int i = Convert.ToInt32(file.ReadLine()),
                    j = Convert.ToInt32(file.ReadLine());

                file.Close();

                matrix = new int[i, j];

                dataGrid.ItemsSource = Libmas.ToDataTable(matrix).DefaultView;
            }
            catch { }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (matrix != null)
            {
                size.Text = $"{matrix.GetLength(0)} x {matrix.GetLength(1)}";
            }
        }

        private void dataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            int index0 = e.Column.DisplayIndex;
            int index1 = e.Row.GetIndex();
            number.Text=($"{index1 + 1} x {index0 + 1}");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Parameters(object sender, RoutedEventArgs e)
        {
            Parameters pw = new();
            pw.Show();
            pw.Owner = this;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                matrix = new int[WindowManager.Rows, WindowManager.Columns];
                dataGrid.ItemsSource = Libmas.ToDataTable(matrix).DefaultView;
            }
            catch { }
        }
    }
}
