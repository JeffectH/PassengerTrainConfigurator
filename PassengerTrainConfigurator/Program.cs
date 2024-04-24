using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PassengerTrainConfigurator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandCreateTrain = 1;
            const int CommandExit = 2;

            bool _isWork = true;

            Dispatcher dispatcher = new Dispatcher(150, 400);

            while (_isWork)
            {
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
                            _isWork = false;
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

    public void Create()
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
        NumberTrain = GetNumberTrain();
    }

    public int NumberTrain { get; private set; }
    public int NumberVan { get; private set; }

    public void AddingVans(int numberOfpassengers)
    {
        NumberVan = (int)Math.Ceiling((decimal)numberOfpassengers /_vanСapacity);
    }

    private int GetNumberTrain()
    {
        NumberTrain = ++s_numberTrain;
        return NumberTrain;
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

        direction.Create();

        TicketOffice ticketOffice = new TicketOffice();

        ticketOffice.TicketSales(_minNumberPassengers, _maxNumberPassengers);

        Train train = new Train();

        train.AddingVans(ticketOffice.NumberTicketsSold);

        Console.WriteLine($"Поезд {train.NumberTrain} сформирован! Его состав состоит из {train.NumberVan} " +
            $"вагонов и он направляеться из {direction.StartPoint} в {direction.EndPoint}.");

        _trains.Add(direction, train);
    }
}


