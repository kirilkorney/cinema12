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

