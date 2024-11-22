using System;

public class Program
{
    public static void Main()
    {
        // Створення об'єктів класу MyFrac
        MyFrac frac1 = new MyFrac(17, 7);
        MyFrac frac2 = new MyFrac(3, 8);

        // Демонстрація методів
        Console.WriteLine("Звичайне подання дробу:");
        Console.WriteLine(frac1);
        Console.WriteLine(frac2);

        Console.WriteLine("\nПодання дробу з виділеною цілою частиною:");
        Console.WriteLine(frac1.ToStringWithIntegerPart());
        Console.WriteLine(frac2.ToStringWithIntegerPart());

        Console.WriteLine("\nДійсне значення дробу:");
        Console.WriteLine(frac1.ToDouble());
        Console.WriteLine(frac2.ToDouble());

        Console.WriteLine("\nАрифметичні операції:");
        Console.WriteLine("Сума: " + Program.Add(frac1, frac2));
        Console.WriteLine("Різниця: " + Program.Minus(frac1, frac2));
        Console.WriteLine("Добуток: " + Program.Multiply(frac1, frac2));
        Console.WriteLine("Частка: " + Program.Divide(frac1, frac2));

        Console.WriteLine("\nСума CalcSum1:");
        Console.WriteLine(Program.CalcSum1(5));

        Console.WriteLine("\nДобуток CalcSum2:");
        Console.WriteLine(Program.CalcSum2(5));

        Console.ReadLine();
    }

    // Метод для додавання дробу
    public static MyFrac Add(MyFrac frac1, MyFrac frac2)
    {
        return new MyFrac(frac1.Numerator * frac2.Denominator + frac1.Denominator * frac2.Numerator, frac1.Denominator * frac2.Denominator);
    }

    // Метод для віднімання дробу
    public static MyFrac Minus(MyFrac frac1, MyFrac frac2)
    {
        return new MyFrac(frac1.Numerator * frac2.Denominator - frac1.Denominator * frac2.Numerator, frac1.Denominator * frac2.Denominator);
    }

    // Метод для множення дробів
    public static MyFrac Multiply(MyFrac frac1, MyFrac frac2)
    {
        return new MyFrac(frac1.Numerator * frac2.Numerator, frac1.Denominator * frac2.Denominator);
    }

    // Метод для ділення дробів
    public static MyFrac Divide(MyFrac frac1, MyFrac frac2)
    {
        return new MyFrac(frac1.Numerator * frac2.Denominator, frac1.Denominator * frac2.Numerator);
    }

    // Метод для обчислення суми 1/(1*2)+1/(2*3)+...+1/(n*(n+1))
    public static MyFrac CalcSum1(int n)
    {
        MyFrac sum = new MyFrac(0, 1);
        for (int i = 1; i <= n; i++)
        {
            MyFrac addend = new MyFrac(1, i * (i + 1));
            sum = Add(sum, addend);
        }
        return sum;
    }

    // Метод для обчислення добутку (1–1/4)*(1–1/9)*...*(1–1/n^2)
    public static MyFrac CalcSum2(int n)
    {
        MyFrac product = new MyFrac(1, 1);
        for (int i = 2; i <= n; i++)
        {
            MyFrac factor = Program.Minus(new MyFrac(1, 1), new MyFrac(1, i * i)); 
            product = Multiply(product, factor);
        }
        return product;
    }
}
