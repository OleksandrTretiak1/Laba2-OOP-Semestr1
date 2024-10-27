using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNamespace
{
    public partial class MyMatrix
    {
        private double[,] elements;

        public MyMatrix(double[,] array)
        {
            elements = (double[,])array.Clone();
        }

        public MyMatrix(double[][] jaggedArray)
        {
            int rowCount = jaggedArray.Length;
            int colCount = jaggedArray[0].Length;

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

        public int Height => elements.GetLength(0);
        public int Width => elements.GetLength(1);

        public int GetHeight() => Height;
        public int GetWidth() => Width;

        public double this[int row, int col]
        {
            get => elements[row, col];
            set => elements[row, col] = value;
        }

        public double GetElement(int row, int col) => this[row, col];

        public void SetElement(int row, int col, double value)
        {
            this[row, col] = value;
        }

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
