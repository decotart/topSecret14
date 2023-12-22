using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Windows.Media;

namespace libmas
{
    public static class Libmas
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                res.Rows.Add(row);
            }
            return res;
        }
        public static void Save(int[,] matrix)
        {
            SaveFileDialog save = new();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true)
            {
                StreamWriter file = new(save.FileName);
                file.WriteLine(matrix.GetLength(0));
                file.WriteLine(matrix.GetLength(1));

                foreach (int i in matrix)
                {
                    file.WriteLine(i);
                }
                file.Close();
            }
        }
        public static int[,]? Open()// знак ? - если пустой файл то вернётся null
        {
            OpenFileDialog open = new();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            if (open.ShowDialog() == true)
            {
                StreamReader file = new(open.FileName);

                int length1 = Convert.ToInt32(file.ReadLine());
                int length2 = Convert.ToInt32(file.ReadLine());

                int[,] matrix = new int[length1, length2];

                for (int i = 0; i < length1; i++)
                {
                    for (int j = 0; j < length2; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(file.ReadLine());
                    }
                }
                file.Close();
                return matrix;
            }
            return null;

        }
        public static int Searches(int[,] matrix)
        { 
            int columnIndex = -1;
            bool foundOddColumn = false;//найден нечетный столбец

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                bool isOddColumn = true;//это нечетный столбец
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] % 2 == 0)
                    {
                        isOddColumn = false;
                        break;
                    }
                }

                if (isOddColumn)
                {
                    columnIndex = j;
                    foundOddColumn = true;
                    break;
                }
            }

            if (foundOddColumn)
                return columnIndex;
            else
                return -1;
        }
    }
}
