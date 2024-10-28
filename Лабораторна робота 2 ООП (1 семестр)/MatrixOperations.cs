using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNamespace
{
    public partial class MyMatrix
    {
        // Оператор додавання двох матриць. Працює лише для матриць однакового розміру
        public static MyMatrix operator +(MyMatrix a, MyMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Матриці повинні мати однакові розміри для додавання.");

            var result = new double[a.Height, a.Width];
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
            return new MyMatrix(result);
        }

        // Оператор множення двох матриць. Працює лише, якщо кількість стовпців першої
        // матриці дорівнює кількості рядків другої
        public static MyMatrix operator *(MyMatrix a, MyMatrix b)
        {
            if (a.Width != b.Height)
                throw new ArgumentException("Множення матриць не визначено для заданих розмірів.");

            var result = new double[a.Height, b.Width];
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    for (int k = 0; k < a.Width; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return new MyMatrix(result);
        }

        // Приватний метод для створення транспонованого масиву матриці (лише масив)
        private double[,] GetTransponedArray()
        {
            var transposed = new double[Width, Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    transposed[j, i] = elements[i, j];
                }
            }
            return transposed;
        }

        // Метод для створення копії транспонованої матриці (повертає MyMatrix)
        public MyMatrix GetTransponedCopy()
        {
            return new MyMatrix(GetTransponedArray());
        }

        // Метод для транспонування матриці "на місці" (замінює елементи поточної матриці)
        public void TransponeMe()
        {
            elements = GetTransponedArray();
        }
    }
    public partial class MyMatrix
    {
        public static void Main(string[] args)
        {
            // Створюємо дві матриці для демонстрації операцій
            double[,] array1 = { { 1, 2 }, { 3, 4 } };
            double[,] array2 = { { 5, 6 }, { 7, 8 } };

            // Ініціалізуємо матриці з масивів
            MyMatrix matrix1 = new MyMatrix(array1);
            MyMatrix matrix2 = new MyMatrix(array2);

            Console.WriteLine("Перша матриця:");
            Console.WriteLine(matrix1);

            Console.WriteLine("Друга матриця:");
            Console.WriteLine(matrix2);

            // Додавання матриць
            try
            {
                MyMatrix sumMatrix = matrix1 + matrix2;
                Console.WriteLine("Сума матриць:");
                Console.WriteLine(sumMatrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні матриць: " + ex.Message);
            }

            // Множення матриць
            try
            {
                MyMatrix productMatrix = matrix1 * matrix2;
                Console.WriteLine("Добуток матриць:");
                Console.WriteLine(productMatrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при множенні матриць: " + ex.Message);
            }

            // Транспонування матриці
            MyMatrix transposedMatrix = matrix1.GetTransponedCopy();
            Console.WriteLine("Транспонована перша матриця:");
            Console.WriteLine(transposedMatrix);

            // Пряме транспонування
            matrix1.TransponeMe();
            Console.WriteLine("Перша матриця після прямого транспонування:");
            Console.WriteLine(matrix1);
            Console.ReadLine();
        }
    }
}
