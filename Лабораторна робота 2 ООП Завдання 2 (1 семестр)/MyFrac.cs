using System;

public class MyFrac
{
    //   Основні зміни:
    // Використання класу замість структури дозволяє інкапсулювати дані і функціональність, пов'язані з дробами.
    // Методи для роботи з дробами були перетворені в методи класу,що дозволяє працювати з екземплярами MyFrac напряму.
    // Використав методи інкапсуляції для доступу до властивостей об'єкта та виконання операцій, що забезпечує гнучкість
    // і захищає дані від небажаних змін.

    private long nom;    // Чисельник 
    private long denom;  // Знаменник 

    // Конструктор, що ініціалізує чисельник та знаменник
    public MyFrac(long nom_, long denom_)
    {
        if (denom_ == 0)
            throw new ArgumentException("Знаменник не може бути нулем.");

        // Якщо знаменник від'ємний, змінюємо знак чисельника і знаменника
        if (denom_ < 0)
        {
            nom_ = -nom_;
            denom_ = -denom_;
        }

        // Скорочення дробу за допомогою НСД
        long gcd = GCD(Math.Abs(nom_), denom_);
        nom = nom_ / gcd;
        denom = denom_ / gcd;
    }

    // Властивість для отримання чисельника (тільки для читання)
    public long Numerator => nom;

    // Властивість для отримання знаменника (тільки для читання)
    public long Denominator => denom;

    // Метод для скорочення дробу за алгоритмом Евкліда
    private static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    // Метод для подання дробу у вигляді "чисельник/знаменник"
    public override string ToString()
    {
        return $"{nom}/{denom}";
    }

    // Метод для подання дробу з виділеною цілою частиною
    public string ToStringWithIntegerPart()
    {
        long integerPart = nom / denom;
        long remainder = nom % denom;
        if (remainder == 0)
            return integerPart.ToString();

        return integerPart != 0 ? $"({integerPart}+{Math.Abs(remainder)}/{denom})" : $"{nom}/{denom}";
    }

    // Метод для обчислення дійсного значення дробу
    public double ToDouble()
    {
        return (double)nom / denom;
    }

    // Метод для додавання дробу
    public MyFrac Add(MyFrac other)
    {
        return new MyFrac(nom * other.denom + denom * other.nom, denom * other.denom);
    }

    // Метод для віднімання дробу
    public MyFrac Minus(MyFrac other)
    {
        return new MyFrac(nom * other.denom - denom * other.nom, denom * other.denom);
    }

    // Метод для множення дробів
    public MyFrac Multiply(MyFrac other)
    {
        return new MyFrac(nom * other.nom, denom * other.denom);
    }

    // Метод для ділення дробів
    public MyFrac Divide(MyFrac other)
    {
        return new MyFrac(nom * other.denom, denom * other.nom);
    }

    // Метод для обчислення суми 1/(1*2)+1/(2*3)+...+1/(n*(n+1))
    public static MyFrac CalcSum1(int n)
    {
        MyFrac sum = new MyFrac(0, 1);
        for (int i = 1; i <= n; i++)
        {
            MyFrac addend = new MyFrac(1, i * (i + 1));
            sum = sum.Add(addend);
        }
        return sum;
    }

    // Метод для обчислення добутку (1–1/4)*(1–1/9)*...*(1–1/n^2)
    public static MyFrac CalcSum2(int n)
    {
        MyFrac product = new MyFrac(1, 1);
        for (int i = 2; i <= n; i++)
        {
            MyFrac factor = new MyFrac(1, 1).Minus(new MyFrac(1, i * i));
            product = product.Multiply(factor);
        }
        return product;
    }
}