using System;
using System.Text;

public class MyFrac
{
    // Інкапсуляція: чисельник та знаменник є приватними, доступ до них здійснюється через властивості Numerator та Denominator.
    // Конструктор забезпечує ініціалізацію дробу та перевірку валідності значень (наприклад, знаменник не може бути нулем).
    // Методи класу MyFrac (Add, Minus, Multiply, Divide) виконують арифметичні операції з дробами та повертають нові екземпляри MyFrac.
    // Статичні методи (CalcSum1, CalcSum2) виконують складні обчислення з дробами без зміни їх стану.
    // Перевизначення методу ToString дозволяє зручно виводити дроби у вигляді "чисельник/знаменник".

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

    // Метод для подання дробу у вигляді "чисельник/знаменник"
    public override string ToString()
    {
        return $"{nom}/{denom}";
    }

    // Метод для подання дробу з виділеною цілою частиною
    public string ToStringWithIntegerPart()
    {
        StringBuilder sb = new StringBuilder();
        long integerPart = nom / denom;
        long remainder = nom % denom;

        if (remainder == 0)
        {
            sb.Append(integerPart.ToString());
        }
        else
        {
            if (integerPart != 0)
            {
                sb.Append(integerPart.ToString());
                sb.Append("+");
            }
            sb.Append(Math.Abs(remainder).ToString());
            sb.Append("/");
            sb.Append(denom.ToString());
        }

        return sb.ToString();
    }

    // Метод для обчислення дійсного значення дробу
    public double ToDouble()
    {
        return (double)nom / denom;
    }

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
}
