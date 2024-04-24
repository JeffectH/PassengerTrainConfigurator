using System;
using System.Collections.Generic;

namespace PassengerTrainConfigurator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrainPlan trainPlan = new TrainPlan(150, 500);
            TrainManager trainManager = new TrainManager(trainPlan);
            trainManager.Work();
        }

    }

    class Direction
    {
        public string StartPoint { get; private set; }
        public string EndPoint { get; private set; }

        public void Create()
        {
            Console.Write("Укажите точку отправления: ");
            StartPoint = Console.ReadLine();
            Console.Write("Укажите точку прибытия: ");
            EndPoint = Console.ReadLine();
        }

        public void ResettingDirectionData()
        {
            StartPoint = "";
            EndPoint = "";
        }
    }

    class Train
    {
        private static int s_numberTrain = 1;

        public Train()
        {
            NumberTrain = GetNumberTrain();
        }

        public int Vmestimost { get; private set; }
        public int NumberTrain { get; private set; }

        public void AddingCarriage()
        {

        }

        private int GetNumberTrain()
        {
            NumberTrain = s_numberTrain++;
            return NumberTrain;
        }
    }

    class TrainPlan
    {
        private Direction _direction = new Direction();
        private Train _train;
        private int _maxNumberPassengers;
        private int _minNumberPassengers;

        public TrainPlan(int minNumberPassengers, int maxNumberPassengers)
        {
            _maxNumberPassengers = maxNumberPassengers;
            _minNumberPassengers = minNumberPassengers;
        }

        public int NumberTicketsSold { get; private set; }
        public int NumberCompartments { get; private set; }
        public int NumberReservedSeat { get; private set; }
        public int NumberSharedCarriage { get; private set; }

        public int GetNumberTicketsSold()
        {
            Random random = new Random();
            NumberTicketsSold = random.Next(_minNumberPassengers, _maxNumberPassengers);

            return NumberTicketsSold;
        }

        public void CreateDirection()
        {
            _direction.Create();
        }

        public string GetDirection()
        {
            string direction = _direction.StartPoint + " - " + _direction.EndPoint;
            return direction;
        }

        public void CreateTrain()
        {
            _train = new Train();
        }

        public int GetNumberTrain()
        {
            return _train.NumberTrain;
        }

        public int GetNumberSeatsTrain()
        {
            return _train.Vmestimost;
        }

        public void ClearingOldData()
        {
            NumberTicketsSold = 0;
            NumberCompartments = 0;
            NumberReservedSeat = 0;
            NumberSharedCarriage = 0;
            _direction.ResettingDirectionData();
        }

        public void AddingCarriageTrain()
        {

            Console.WriteLine("Вагон добавлен!");
        }


        private void CountTheNumberWagons()
        {

        }
    }

    class TrainManager
    {
        private TrainPlan _trainPlan;

        public TrainManager(TrainPlan trainPlan)
        {
            _trainPlan = trainPlan;
        }

        public void Work()
        {
            bool isOpen = true;

            while (isOpen)
            {
                _trainPlan.CreateTrain();

                InfoTrain();
                _trainPlan.CreateDirection();
                Console.ReadKey();


                InfoTrain();
                Console.Write($"\nКолличество проданных билетов на данное направление: {_trainPlan.GetNumberTicketsSold()}");
                Console.ReadKey();

                while (_trainPlan.GetNumberSeatsTrain() < _trainPlan.NumberTicketsSold)
                {
                    InfoTrain();
                    Console.WriteLine("В поезде не хватает мест для всех пассажиров!");
                    Console.WriteLine("Нужно сформировать поезд. Для этого нужно добавить вагоны поезду, чтобы всем пассажирам хватило мест.\n");
                    Console.WriteLine($"Есть 3 типа вагонов. Выберайте необходимые вагоны до тех пор пока вам нехватит их, чтобы посадить всех пассажиров:\n" +
                        $"\n1.Общий с вместимостью: " +
                        $"\n2.Плацкарт с вместимостью: " +
                        $"\n3.Купе с вместимостью: ");
                    Console.Write("\nВведите номер вагона: ");

                    if (int.TryParse(Console.ReadLine(), out int userInput))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Введены некорректные данные!");
                        continue;
                    }

                    Console.ReadKey();
                    Console.Clear();

                }

                InfoTrain();
                Console.WriteLine("Поезд сформирован и готов к отправлению! \n" +
                    "Отправлять поезд?\n" +
                    "1.Да\n" +
                    "2.Нет\n");
                Console.Write("Ваш ответ: ");

                if (int.TryParse(Console.ReadLine(), out int userAnswer))
                {
                    switch (userAnswer)
                    {
                        case 1:
                            Console.WriteLine("Поезд отправлен. Все довольны!");
                            Console.WriteLine("Вы можете сформировать новый поезд!");
                            _trainPlan.ClearingOldData();
                            break;

                        case 2:
                            Console.WriteLine($"Поезд не был отправлен. Из-за задержки на вас написали жалобы {_trainPlan.NumberTicketsSold} пассажиров и Вас уволили!");
                            isOpen = false;
                            break;
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void InfoTrain()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Информация о поезде №{_trainPlan.GetNumberTrain()} ");
            Console.WriteLine($"Направление: {_trainPlan.GetDirection()}");
            Console.WriteLine($"Кол-во мест в поезде: {_trainPlan.GetNumberSeatsTrain()}");
            Console.WriteLine($"Кол-во пассажиров: {_trainPlan.NumberTicketsSold}");
            Console.WriteLine($"Кол-во вагонов: купе:{_trainPlan.NumberCompartments} плацкарт:{_trainPlan.NumberReservedSeat} общий:{_trainPlan.NumberSharedCarriage}" +
                $" итого:{_trainPlan.NumberCompartments + _trainPlan.NumberReservedSeat + _trainPlan.NumberSharedCarriage}");
            Console.SetCursorPosition(0, 6);
        }
    }
}

