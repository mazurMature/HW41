using System;
using System.Collections.Generic;

namespace HW41
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dispetcher dispetcher = new Dispetcher();

            dispetcher.Work();
        }
    }

    class Dispetcher
    {
        const string CommandAddTrain = "1";
        const string CommandExit = "2";

        private List<Train> _trains = new List<Train>();

        private Random _random = new Random();

        private int _minPassengers = 50;
        private int _maxPassengers = 200;

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                ShowAllTrains();

                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case CommandAddTrain:
                        CreateTrain();
                        break;

                    case CommandExit:
                        isWork = false;
                        Console.WriteLine("\nВыход из программы...");
                        break;

                    default:
                        Console.WriteLine("Некорректная команда. Пожалуйста, выберите из доступных пунктов меню.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void CreateTrain()
        {
            int passengers = _random.Next(_minPassengers, _maxPassengers + 1);

            Console.Clear();
            Console.WriteLine("Создание нового поезда...\n");

            Console.Write("Введите начальный пункт: ");
            string from = Console.ReadLine();

            Console.Write("Введите конечный пункт: ");
            string to = Console.ReadLine();

            Direction direction = new Direction(from, to);

            Console.WriteLine($"Продано билетов: {passengers}");

            Console.WriteLine("Нажмите любую клавишу для формирования поезда...");
            Console.ReadKey();

            Train train = new Train(direction);

            FormTrain(passengers, train);
            _trains.Add(train);

            Console.WriteLine("\nПоезд успешно создан:");
            train.ShowFullInfo();

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        public void FormTrain(int passengers, Train train)
        {
            int minCapacity = 30;
            int maxCapacity = 50;

            int passengersLeft = passengers;

            while (passengersLeft > 0)
            {
                int capacity = _random.Next(minCapacity, maxCapacity + 1);

                if (capacity > passengersLeft)
                    capacity = passengersLeft;

                Wagon wagon = new Wagon(capacity);
                train.AddWagon(wagon);

                passengersLeft -= capacity;
            }
        }

        public void ShowAllTrains()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Нет созданных поездов.");
            }
            else
            {
                Console.WriteLine("Список поезда:");

                for (int i = 0; i < _trains.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_trains[i].GetShortInfo()}");
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine($"{CommandAddTrain}. Создать поезд");
            Console.WriteLine($"{CommandExit}. Выход");
            Console.Write("Выберите действие: ");
        }
    }

    class Direction
    {
        public Direction(string from, string to)
        {
            StartPoint = from;
            EndPoint = to;
        }

        public string StartPoint { get; private set; }
        public string EndPoint { get; private set; }

        public string GetDirection()
        {
            return $"{StartPoint} -> {EndPoint}";
        }
    }

    class Wagon
    {
        public Wagon(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; private set; }
    }

    class Train
    {
        private Direction _direction;

        private List<Wagon> _wagons = new List<Wagon>();

        public Train(Direction direction)
        {
            _direction = direction;
        }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        public void ShowFullInfo()
        {
            Console.WriteLine($"Направление: {_direction.GetDirection()}");
            Console.WriteLine($"Количество вагонов: {_wagons.Count}");

            for (int i = 0; i < _wagons.Count; i++)
                Console.WriteLine($"Вагон {i + 1}: вместимость {_wagons[i].Capacity} пассажиров");
        }

        public string GetShortInfo()
        {
            return $"Направление: {_direction.GetDirection()}, Вагонов: {_wagons.Count}";
        }
    }
}
