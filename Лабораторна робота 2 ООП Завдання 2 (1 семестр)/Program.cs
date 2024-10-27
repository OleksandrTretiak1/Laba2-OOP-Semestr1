using System;

namespace laba4
{
    
      //   У цьому коді ми перетворили структуру MyTime в клас MyTime для дотримання принципів ООП.
      //   Основні зміни:
      //1. Використання класу замість структури дозволяє інкапсулювати дані і функціональність, пов'язані з часом, в одному місці.
      //2. Методи для роботи з часом (обчислення різниці, додавання секунд тощо) були перетворені в методи класу, 
      //   що дозволяє працювати з екземплярами MyTime напряму.
      //3. Ми використали методи інкапсуляції для доступу до властивостей об'єкта та виконання операцій, що забезпечує гнучкість
      //   і захищає дані від небажаних змін.
     

    class MyTime
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public MyTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public int ToSecSinceMidnight()
        {
            return Hour * 3600 + Minute * 60 + Second;
        }

        public static MyTime FromSecSinceMidnight(int totalSeconds)
        {
            int secPerDay = 60 * 60 * 24;
            totalSeconds %= secPerDay;
            if (totalSeconds < 0)
                totalSeconds += secPerDay;
            int h = totalSeconds / 3600;
            int m = (totalSeconds / 60) % 60;
            int s = totalSeconds % 60;
            return new MyTime(h, m, s);
        }

        public MyTime AddSeconds(int seconds)
        {
            int totalSeconds = ToSecSinceMidnight() + seconds;
            return FromSecSinceMidnight(totalSeconds);
        }

        public MyTime AddOneHour() => AddSeconds(3600);

        public MyTime AddOneMinute() => AddSeconds(60);

        public MyTime AddOneSecond() => AddSeconds(1);

        public int Difference(MyTime other)
        {
            return ToSecSinceMidnight() - other.ToSecSinceMidnight();
        }

        public bool IsInRange(MyTime start, MyTime end)
        {
            int startSeconds = start.ToSecSinceMidnight();
            int endSeconds = end.ToSecSinceMidnight();
            int currentSeconds = ToSecSinceMidnight();

            if (endSeconds < startSeconds)
            {
                endSeconds += 24 * 60 * 60;
            }

            return currentSeconds >= startSeconds && currentSeconds < endSeconds;
        }

        public string WhatLesson()
        {
            MyTime[] startTimes = {
                new MyTime(8, 0, 0), new MyTime(9, 40, 0), new MyTime(11, 20, 0),
                new MyTime(13, 0, 0), new MyTime(14, 40, 0), new MyTime(16, 10, 0)
            };
            MyTime[] endTimes = {
                new MyTime(9, 20, 0), new MyTime(11, 0, 0), new MyTime(12, 40, 0),
                new MyTime(14, 20, 0), new MyTime(16, 0, 0), new MyTime(17, 30, 0)
            };

            int currentTime = ToSecSinceMidnight();
            if (currentTime < startTimes[0].ToSecSinceMidnight())
                return "Пари ще не почалися";
            if (currentTime > endTimes[5].ToSecSinceMidnight())
                return "Пари вже скінчилися";

            for (int i = 0; i < startTimes.Length; i++)
            {
                if (IsInRange(startTimes[i], endTimes[i]))
                {
                    return $"{i + 1}-а пара";
                }
                else if (i < startTimes.Length - 1 && IsInRange(endTimes[i], startTimes[i + 1]))
                {
                    return $"перерва між {i + 1}-ю та {i + 2}-ю парою";
                }
            }

            return "Пари вже скінчилися";
        }

        public override string ToString()
        {
            return $"{Hour}:{Minute:D2}:{Second:D2}";
        }
    }

    class Program
    {
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
