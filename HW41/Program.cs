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
        private List<Train> _trains = new List<Train>();

        private Random _random = new Random();

        private int _minPassengers = 50;
        private int _maxPassengers = 200;

        public void Work()
        {
            const string CommandAddTrain = "1";
            const string CommandExit = "2";

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                ShowAllTrains();

                Console.WriteLine("\nМеню:");
                Console.WriteLine($"{CommandAddTrain}. Создать поезд");
                Console.WriteLine($"{CommandExit}. Выход");
                Console.Write("Выберите действие: ");

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
                        Console.WriteLine("Некорректный ввод. Нажмите любую клавишу...");
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

            Train train = new Train(direction, passengers);

            train.FormTrain();
            _trains.Add(train);

            Console.WriteLine("\nПоезд успешно создан:");
            train.ShowFullInfo();

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
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
    }

    class Direction
    {
        public string StartPoint { get; private set; }
        public string EndPoint { get; private set; }

        public Direction(string from, string to)
        {
            StartPoint = from; 
            EndPoint = to;   
        }

        public string ShowDirection()
        {
            return $"{StartPoint} -> {EndPoint}";
        }
    }

    class Wagon
    {
        public int Capacity { get; private set; }

        public Wagon(int capacity)
        {
            Capacity = capacity;
        }
    }

    class Train
    {
        private Direction _direction;
        private int _passengers;

        private List<Wagon> _wagons = new List<Wagon>();

        private Random _random = new Random();

        public Train(Direction direction, int passengers)
        {
            _direction = direction;
            _passengers = passengers;
        }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        public void FormTrain()
        {
            int _minCapacity = 30;
            int _maxCapacity = 50;

            int passengersLeft = _passengers;

            while (passengersLeft > 0)
            {
                int capacity = _random.Next(_minCapacity, _maxCapacity + 1); 

                if (capacity > passengersLeft)
                    capacity = passengersLeft;

                _wagons.Add(new Wagon(capacity));

                passengersLeft -= capacity;
            }
        }

        public void ShowFullInfo()
        {
            Console.WriteLine($"Направление: {_direction.ShowDirection()}");
            Console.WriteLine($"Количество вагонов: {_wagons.Count}");

            for (int i = 0; i < _wagons.Count; i++)
                Console.WriteLine($"Вагон {i + 1}: вместимость {_wagons[i].Capacity} пассажиров");
        }

        public string GetShortInfo()
        {
            return $"Направление: {_direction.ShowDirection()}, Вагонов: {_wagons.Count}";
        }
    }
}
