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
            // Налаштування виводу для підтримки кирилиці в консолі
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1251);

            // Запит часу від користувача
            Console.WriteLine("Введіть час: ");
            int[] time = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            MyTime mt = new MyTime(time[0], time[1], time[2]);

            // Вибір методу користувачем
            Console.WriteLine("Виберіть метод: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine(ToSecSinceMidnight(mt));
                    break;
                case 2:
                    Console.WriteLine("Введіть кількість секунд");
                    int seconds = int.Parse(Console.ReadLine());
                    Console.WriteLine(FromSecSinceMidnight(seconds));
                    break;
                case 3:
                    Console.WriteLine(AddOneSecond(mt));
                    break;
                case 4:
                    Console.WriteLine(AddOneMinute(mt));
                    break;
                case 5:
                    Console.WriteLine(AddOneHour(mt));
                    break;
                case 6:
                    Console.WriteLine("Введіть кількість секунд, які треба додати");
                    int sec = int.Parse(Console.ReadLine());
                    Console.WriteLine(AddSeconds(mt, sec));
                    break;
                case 7:
                    Console.WriteLine("Введіть другий момент часу: ");
                    int[] time2 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    MyTime mt2 = new MyTime(time2[0], time2[1], time2[2]);
                    Console.WriteLine(Difference(mt, mt2));
                    break;
                case 8:
                    Console.WriteLine("Введіть стартовий момент часу: ");
                    int[] timeStart = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    MyTime start = new MyTime(timeStart[0], timeStart[1], timeStart[2]);
                    Console.WriteLine("Введіть кінцевий момент часу: ");
                    int[] timeFinish = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    MyTime finish = new MyTime(timeFinish[0], timeFinish[1], timeFinish[2]);
                    if (IsInRange(start, finish, mt))
                        Console.WriteLine("Обраний момент часу знаходиться в діапазоні");
                    else
                        Console.WriteLine("Обраний момент часу не знаходиться в діапазоні");
                    break;
                case 9:
                    Console.WriteLine(WhatLesson(mt));
                    break;
            }
            Console.ReadLine();
        }
    }
}
