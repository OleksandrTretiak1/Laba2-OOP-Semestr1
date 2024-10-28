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
        Console.WriteLine("Сума: " + frac1.Add(frac2));     
        Console.WriteLine("Різниця: " + frac1.Minus(frac2)); 
        Console.WriteLine("Добуток: " + frac1.Multiply(frac2)); 
        Console.WriteLine("Частка: " + frac1.Divide(frac2));  

        Console.WriteLine("\nСума CalcSum1:");
        Console.WriteLine(MyFrac.CalcSum1(5));  

        Console.WriteLine("\nДобуток CalcSum2:");
        Console.WriteLine(MyFrac.CalcSum2(5)); 
        Console.ReadLine();
    }
}