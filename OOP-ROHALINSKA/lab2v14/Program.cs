using System;
using System.Collections.Generic;

class Restaurant
{
    private string name;
    private string cuisine;
    private List<string> menu = new List<string>();

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Cuisine
    {
        get { return cuisine; }
        set { cuisine = value; }
    }

    public double Rating { get; set; }

    public Restaurant(string name, string cuisine, double rating)
    {
        this.name = name;
        this.cuisine = cuisine;
        Rating = rating;
    }

    // ІНДЕКСАТОР
    public string this[int index]
    {
        get { return menu[index]; }
        set { menu[index] = value; }
    }

    // ОПЕРАТОР +
    public static Restaurant operator +(Restaurant r, string dish)
    {
        r.menu.Add(dish);
        return r;
    }

    // ОПЕРАТОРИ > <
    public static bool operator >(Restaurant r1, Restaurant r2)
    {
        return r1.Rating > r2.Rating;
    }

    public static bool operator <(Restaurant r1, Restaurant r2)
    {
        return r1.Rating < r2.Rating;
    }

    public void ServeDish(int index)
    {
        Console.WriteLine($"{name} подає: {menu[index]}");
    }
}

class Program
{
    static void Main()
    {
        Restaurant r1 = new Restaurant("La Piazza", "Італійська", 4.7);
        Restaurant r2 = new Restaurant("Sushi World", "Японська", 4.9);

        r1 = r1 + "Піца";
        r1 = r1 + "Паста";

        r2 = r2 + "Суші";
        r2 = r2 + "Рамен";

        Console.WriteLine(r1[0]);
        Console.WriteLine(r2[1]);

        r1.ServeDish(1);

        if (r2 > r1)
            Console.WriteLine("Sushi World має вищий рейтинг");
        else
            Console.WriteLine("La Piazza має вищий рейтинг");

        Console.ReadLine();
    }
}
