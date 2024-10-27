using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNamespace
{
    public partial class MyMatrix
    {
        // Основне поле класу - двовимірний масив, що зберігає елементи матриці
        private double[,] elements;

        // Конструктор: створення матриці з двовимірного масиву типу double[,]
        public MyMatrix(double[,] array)
        {
            elements = (double[,])array.Clone();
        }

        // Конструктор: створення матриці з зубчастого масиву double[][], якщо він прямокутний
        public MyMatrix(double[][] jaggedArray)
        {
            int rowCount = jaggedArray.Length;
            int colCount = jaggedArray[0].Length;

            // Перевірка, чи є масив прямокутним
            for (int i = 1; i < rowCount; i++)
            {
                if (jaggedArray[i].Length != colCount)
                    throw new ArgumentException("Зубчастий масив повинен бути прямокутним.");
            }

            elements = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    elements[i, j] = jaggedArray[i][j];
                }
            }
        }

        // Конструктор: створення матриці з масиву рядків
        // Кожен рядок повинен містити однакову кількість чисел, розділених пробілами або табуляцією
        public MyMatrix(string[] rows)
        {
            int rowCount = rows.Length;
            int colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            elements = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                var rowValues = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (rowValues.Length != colCount)
                    throw new ArgumentException("Усі рядки повинні мати однакову кількість стовпчиків.");

                for (int j = 0; j < colCount; j++)
                {
                    elements[i, j] = double.Parse(rowValues[j]);
                }
            }
        }

        // Конструктор: створення матриці з рядка, що містить числа, розділені пробілами та переведенням рядка
        public MyMatrix(string matrixString)
        {
            var rows = matrixString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int rowCount = rows.Length;
            int colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            elements = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                var rowValues = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (rowValues.Length != colCount)
                    throw new ArgumentException("Матриця повинна бути прямокутною.");

                for (int j = 0; j < colCount; j++)
                {
                    elements[i, j] = double.Parse(rowValues[j]);
                }
            }
        }

        // Властивість для отримання висоти (кількості рядків) матриці
        public int Height => elements.GetLength(0);

        // Властивість для отримання ширини (кількості стовпчиків) матриці
        public int Width => elements.GetLength(1);

        // Java-стиль геттер для висоти
        public int GetHeight() => Height;

        // Java-стиль геттер для ширини
        public int GetWidth() => Width;

        // Індексатор для доступу до елементів матриці
        public double this[int row, int col]
        {
            get => elements[row, col];
            set => elements[row, col] = value;
        }

        // Java-стиль геттер для окремого елемента
        public double GetElement(int row, int col) => this[row, col];

        // Java-стиль сетер для окремого елемента
        public void SetElement(int row, int col, double value)
        {
            this[row, col] = value;
        }

        // Перевизначення методу ToString() для зручного виведення матриці
        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result += elements[i, j].ToString("0.##") + "\t";
                }
                result = result.TrimEnd() + "\n";
            }
            return result;
        }
    }
}
