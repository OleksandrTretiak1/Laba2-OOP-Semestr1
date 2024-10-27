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

        // Метод для перевірки, чи належить даний момент часу відрізку між двома іншими моментами
        static bool IsInRange(MyTime start, MyTime finish, MyTime t)
        {
            int startSeconds = ToSecSinceMidnight(start);
            int finishSeconds = ToSecSinceMidnight(finish);
            int momentSeconds = ToSecSinceMidnight(t);

            // Якщо момент finish є меншим за start, значить, ми маємо справу з перехресним днем
            if (finishSeconds < startSeconds)
            {
                finishSeconds = 24 * 60 * 60 + finishSeconds; // Додаємо 24 години до finish, щоб уникнути проблеми з перехресним днем
            }

            // Перевірка, чи момент t знаходиться між start і finish
            return momentSeconds >= startSeconds && momentSeconds < finishSeconds;
        }

        // Метод для обчислення різниці між двома моментами часу
        static int Difference(MyTime mt1, MyTime mt2)
        {
            return ToSecSinceMidnight(mt1) - ToSecSinceMidnight(mt2);
        }

        // Метод для додавання секунд до моменту часу
        static MyTime AddSeconds(MyTime t, int s)
        {
            int totalSeconds = ToSecSinceMidnight(t) + s;
            return FromSecSinceMidnight(totalSeconds);
        }

        // Метод для додавання однієї години до моменту часу
        static MyTime AddOneHour(MyTime t)
        {
            int totalSeconds = ToSecSinceMidnight(t) + 3600;
            return FromSecSinceMidnight(totalSeconds);
        }

        // Метод для додавання однієї хвилини до моменту часу
        static MyTime AddOneMinute(MyTime t)
        {
            int totalSeconds = ToSecSinceMidnight(t) + 60;
            return FromSecSinceMidnight(totalSeconds);
        }

        // Метод для додавання однієї секунди до моменту часу
        static MyTime AddOneSecond(MyTime t)
        {
            int totalSeconds = ToSecSinceMidnight(t) + 1;
            return FromSecSinceMidnight(totalSeconds);
        }

        // Метод для перетворення секунд з півночі в момент часу
        static MyTime FromSecSinceMidnight(int t)
        {
            int secPerDay = 60 * 60 * 24;
            t %= secPerDay;
            if (t < 0)
                t += secPerDay;
            int h = t / 3600;
            int m = (t / 60) % 60;
            int s = t % 60;
            return new MyTime(h, m, s);
        }

        // Метод для перетворення моменту часу в секунди з півночі
        static int ToSecSinceMidnight(MyTime t)
        {
            return t.hour * 3600 + t.minute * 60 + t.second;
        }

        // Структура, яка представляє момент часу
        struct MyTime
        {
            public int hour, minute, second;
            // Конструктор
            public MyTime(int h, int m, int s)
            {
                hour = h;
                minute = m;
                second = s;
            }
            // Перевизначення методу ToString для виводу моменту часу у зручному форматі
            public override string ToString()
            {
                return $"{hour}:{minute.ToString("00")}:{second.ToString("00")}";
            }

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
