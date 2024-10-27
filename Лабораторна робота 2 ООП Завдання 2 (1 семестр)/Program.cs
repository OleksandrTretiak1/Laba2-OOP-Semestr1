using System;

namespace laba4
{

    class Programm
    {
        // Метод для визначення, яка зараз пара або перерва
        public string WhatLesson(MyTime[] start, MyTime[] end)
        {
            int currentSec = ToSecSinceMidnight();

            if (currentSec < start[0].ToSecSinceMidnight()) return "Пари ще не почалися";
            if (currentSec > end[end.Length - 1].ToSecSinceMidnight()) return "Пари вже скінчилися";

            for (int i = 0; i < start.Length; i++)
            {
                if (IsInRange(start[i], end[i]))
                {
                    return $"{i + 1}-а пара";
                }
                else if (i < start.Length - 1 && IsInRange(end[i], start[i + 1]))
                {
                    return $"Перерва між {i + 1}-ю та {i + 2}-ю парою";
                }
            }
            return "Пари вже скінчилися";
        }

        // Перевіряє, чи належить поточний момент часу до діапазону
        public bool IsInRange(MyTime start, MyTime end)
        {
            int startSec = start.ToSecSinceMidnight();
            int endSec = end.ToSecSinceMidnight();
            int currentSec = ToSecSinceMidnight();

            if (endSec < startSec)
                endSec += 24 * 3600;

            return currentSec >= startSec && currentSec < endSec;
        }

        public class MyTime
    {
        // Властивості годин, хвилин та секунд
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        // Конструктор класу MyTime
        public MyTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        // Перевизначення методу ToString для зручного виводу
        public override string ToString()
        {
            return $"{Hour}:{Minute:D2}:{Second:D2}";
        }

        // Метод для конвертації поточного моменту часу в секунди від початку доби
        public int ToSecSinceMidnight()
        {
            return Hour * 3600 + Minute * 60 + Second;
        }

        // Метод для обчислення різниці між двома моментами часу
        public int Difference(MyTime other)
        {
            return ToSecSinceMidnight() - other.ToSecSinceMidnight();
        }

        // Метод для додавання секунд до поточного моменту часу
        public MyTime AddSeconds(int seconds)
        {
            int totalSeconds = ToSecSinceMidnight() + seconds;
            return FromSecSinceMidnight(totalSeconds);
        }

         // Статичний метод для конвертації секунд з півночі у формат MyTime
        public static MyTime FromSecSinceMidnight(int seconds)
        {
            int secPerDay = 24 * 3600;
            seconds %= secPerDay;
            if (seconds < 0)
                seconds += secPerDay;

            int h = seconds / 3600;
            int m = (seconds / 60) % 60;
            int s = seconds % 60;

            return new MyTime(h, m, s);
        }
     

        // Додає одну годину до поточного часу
        public MyTime AddOneHour()
        {
            return AddSeconds(3600);
        }

        // Додає одну хвилину до поточного часу
        public MyTime AddOneMinute()
        {
            return AddSeconds(60);
        }

        // Додає одну секунду до поточного часу
        public MyTime AddOneSecond()
        {
            return AddSeconds(1);
        }


            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.GetEncoding(1251);

                int[] timeInput = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                MyTime time = new MyTime(timeInput[0], timeInput[1], timeInput[2]);

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Секунди від півночі: " + time.ToSecSinceMidnight());
                        break;
                    case 2:
                        Console.WriteLine("Введіть кількість секунд від півночі:");
                        int seconds = int.Parse(Console.ReadLine());
                        Console.WriteLine("Час: " + MyTime.FromSecSinceMidnight(seconds));
                        break;
                    case 3:
                        Console.WriteLine("Час +1 секунда: " + time.AddOneSecond());
                        break;
                    case 4:
                        Console.WriteLine("Час +1 хвилина: " + time.AddOneMinute());
                        break;
                    case 5:
                        Console.WriteLine("Час +1 година: " + time.AddOneHour());
                        break;
                    case 6:
                        Console.WriteLine("Введіть кількість секунд для додавання:");
                        int sec = int.Parse(Console.ReadLine());
                        Console.WriteLine("Час після додавання секунд: " + time.AddSeconds(sec));
                        break;
                    case 7:
                        Console.WriteLine("Введіть другий час у форматі ГГ ММ СС:");
                        int[] time2Input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                        MyTime time2 = new MyTime(time2Input[0], time2Input[1], time2Input[2]);
                        Console.WriteLine("Різниця в секундах: " + time.Difference(time2));
                        break;
                    case 8:
                        Console.WriteLine("Введіть стартовий момент часу у форматі ГГ ММ СС:");
                        int[] startInput = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                        MyTime start = new MyTime(startInput[0], startInput[1], startInput[2]);

                        Console.WriteLine("Введіть кінцевий момент часу у форматі ГГ ММ СС:");
                        int[] endInput = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                        MyTime end = new MyTime(endInput[0], endInput[1], endInput[2]);

                        if (time.IsInRange(start, end))
                            Console.WriteLine("Обраний момент часу знаходиться в діапазоні.");
                        else
                            Console.WriteLine("Обраний момент часу не знаходиться в діапазоні.");
                        break;
                    case 9:
                        Console.WriteLine("Зараз: " + time.WhatLesson());
                        break;
                    default:
                        Console.WriteLine("Неправильний вибір операції.");
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}
