using System;
using System.Collections.Generic;

// 1. Спочатку запуск програми (інструкції верхнього рівня)
Cinema cinema = new Cinema();
cinema.Run();

// 2. А вже нижче пішли самі класи проєкту

class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }

    public Movie(string title, string genre, int duration)
    {
        Title = title;
        Genre = genre;
        Duration = duration;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Title} | Жанр: {Genre} | Тривалість: {Duration} хв.");
    }
}

class CinemaHall
{
    public int Number { get; set; }
    public int Capacity { get; set; }

    public CinemaHall(int number, int capacity)
    {
        Number = number;
        Capacity = capacity;
    }
}

class Screening
{
    public Movie Movie { get; set; }
    public CinemaHall Hall { get; set; }
    public string Time { get; set; }
    public List<Ticket> Tickets { get; set; }

    public Screening(Movie movie, CinemaHall hall, string time)
    {
        Movie = movie;
        Hall = hall;
        Time = time;
        Tickets = new List<Ticket>();
    }

    public bool SellTicket(Customer customer)
    {
        if (Tickets.Count >= Hall.Capacity)
        {
            Console.WriteLine("На цей сеанс квитків немає!");
            return false;
        }

        int seat = Tickets.Count + 1;
        Ticket ticket = new Ticket(Movie.Title, Hall.Number, seat, Time, customer);
        Tickets.Add(ticket);

        Console.WriteLine("Квиток успішно придбано!");
        ticket.ShowInfo();

        return true;
    }

    public void ShowInfo(int number)
    {
        Console.WriteLine(
            $"{number}. {Movie.Title} | Час: {Time} | " +
            $"Зал: {Hall.Number} | Вільних місць: {Hall.Capacity - Tickets.Count}");
    }
}
class Ticket
{
    public string MovieTitle { get; set; }
    public int HallNumber { get; set; }
    public int SeatNumber { get; set; }
    public string Time { get; set; }
    public Customer Customer { get; set; }

    public Ticket(string movieTitle, int hallNumber, int seatNumber,
        string time, Customer customer)
    {
        MovieTitle = movieTitle;
        HallNumber = hallNumber;
        SeatNumber = seatNumber;
        Time = time;
        Customer = customer;
    }

    public void ShowInfo()
    {
        Console.WriteLine("--- Ваш квиток ---");
        Console.WriteLine($"Фільм: {MovieTitle}");
        Console.WriteLine($"Час: {Time}");
        Console.WriteLine($"Зал: {HallNumber}");
        Console.WriteLine($"Місце: {SeatNumber}");
        Console.WriteLine($"Покупець: {Customer.Name}");
    }
}

class Customer
{
    public string Name { get; set; }

    public Customer(string name)
    {
        Name = name;
    }
}

class Cinema
{
    private List<Movie> movies = new List<Movie>();
    private List<Screening> screenings = new List<Screening>();

    public Cinema()
    {
        Movie movie1 = new Movie("Аватар", "Фантастика", 162);
        Movie movie2 = new Movie("Шрек", "Мультфільм", 90);
        Movie movie3 = new Movie("Інтерстеллар", "Фантастика", 169);

        movies.Add(movie1);
        movies.Add(movie2);
        movies.Add(movie3);

        CinemaHall hall1 = new CinemaHall(1, 3);
        CinemaHall hall2 = new CinemaHall(2, 5);

        screenings.Add(new Screening(movie1, hall1, "14:00"));
        screenings.Add(new Screening(movie2, hall2, "16:30"));
        screenings.Add(new Screening(movie3, hall1, "19:00"));
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== КІНОТЕАТР ===");
            Console.WriteLine("1. Переглянути фільми");
            Console.WriteLine("2. Переглянути сеанси");
            Console.WriteLine("3. Купити квиток");
            Console.WriteLine("0. Вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    ShowMovies();
                    break;

                case "2":
                    ShowScreenings();
                    break;

                case "3":
                    BuyTicket();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Дякуємо за відвідування!");
                    break;

                default:
                    Console.WriteLine("Неправильний пункт меню!");
                    break;
            }
        }
    }

    private void ShowMovies()
    {
        Console.WriteLine("\n=== СПИСОК ФІЛЬМІВ ===");

        foreach (Movie movie in movies)
        {
            movie.ShowInfo();
        }
    }

    private void ShowScreenings()
    {
        Console.WriteLine("\n=== РОЗКЛАД СЕАНСІВ ===");

        for (int i = 0; i < screenings.Count; i++)
        {
            screenings[i].ShowInfo(i + 1);
        }
    }

    private void BuyTicket()
    {
        ShowScreenings();

        Console.Write("Введіть номер сеансу: ");
        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 || number > screenings.Count)
        {
            Console.WriteLine("Неправильний номер сеансу!");
            return;
        }

        Console.Write("Введіть ім'я покупця: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Ім'я не може бути порожнім!");
            return;
        }

        Customer customer = new Customer(name);
        screenings[number - 1].SellTicket(customer);
    }
}
