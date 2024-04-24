using System;
using System.Collections.Generic;

namespace PassengerTrainConfigurator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandCreateTrain = 1;
            const int CommandExit = 2;

            int minTickets = 150;
            int maxTickets = 400;

            bool isWork = true;

            Dispatcher dispatcher = new Dispatcher(minTickets, maxTickets);

            while (isWork)
            {
                dispatcher.UpdateInfo();

                Console.WriteLine($"Меню: {CommandCreateTrain} - cоздать поезд {CommandExit} - выход ");

                Console.Write("\r\nВыберите команду:");

                if (int.TryParse(Console.ReadLine(), out int inputUser))
                {
                    switch (inputUser)
                    {
                        case CommandCreateTrain:
                            dispatcher.Work();
                            break;

                        case CommandExit:
                            isWork = false;
                            break;

                        default:
                            Console.WriteLine("\nТакой команды не существует!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректные данные!");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

class Direction
{
    public string StartPoint { get; private set; }
    public string EndPoint { get; private set; }

    public Direction()
    {
        Console.Write("Укажите точку отправления: ");

        StartPoint = Console.ReadLine();

        Console.Write("Укажите точку прибытия: ");

        EndPoint = Console.ReadLine();
    }
}

class Train
{
    private static int s_numberTrain = 0;

    private int _vanСapacity = 54;

    public Train()
    {
        NumberTrain = NumberTrain = ++s_numberTrain;
    }

    public int NumberTrain { get; private set; }
    public int NumberVan { get; private set; }
    public int NumberOfPassengers { get; private set; }

    public void AddingVans(int numberOfPassengers)
    {
        NumberVan = (int)Math.Ceiling((decimal)numberOfPassengers / _vanСapacity);
        NumberOfPassengers = numberOfPassengers;
    }
}

class TicketOffice
{
    public int NumberTicketsSold { get; private set; }

    public void TicketSales(int minNumberPassengers, int maxNumberPassengers)
    {
        Random random = new Random();

        NumberTicketsSold = random.Next(minNumberPassengers, maxNumberPassengers);

        Console.WriteLine($"Было проданно: {NumberTicketsSold} билетов.");
    }
}

class Dispatcher
{
    private Dictionary<Direction, Train> _trains = new Dictionary<Direction, Train>();

    private int _minNumberPassengers;
    private int _maxNumberPassengers;

    public Dispatcher(int minNumberPassengers, int maxNumberPassengers)
    {
        _minNumberPassengers = minNumberPassengers;
        _maxNumberPassengers = maxNumberPassengers;
    }

    public void Work()
    {
        Direction direction = new Direction();

        TicketOffice ticketOffice = new TicketOffice();

        ticketOffice.TicketSales(_minNumberPassengers, _maxNumberPassengers);

        Train train = new Train();

        train.AddingVans(ticketOffice.NumberTicketsSold);

        Console.WriteLine($"Поезд {train.NumberTrain} сформирован! Его состав состоит из {train.NumberVan} " +
            $"вагонов и он направляеться из {direction.StartPoint} в {direction.EndPoint}.");

        _trains.Add(direction, train);
    }

    public void UpdateInfo()
    {
        int offset = 2;

        Console.WriteLine($"Колличество текущих поездов: {_trains.Count}");

        foreach (var train in _trains)
        {
            Console.WriteLine($"Поезд №{train.Value.NumberTrain} едет из {train.Key.StartPoint} в {train.Key.EndPoint}." +
            $" Колличество пассажиров {train.Value.NumberOfPassengers}." +
            $" Колличество вагонов в составе {train.Value.NumberVan}.");
        }

        Console.SetCursorPosition(0, _trains.Count + offset);
    }
}