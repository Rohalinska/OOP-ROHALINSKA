using System;

namespace lab22
{

    class Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }

    class Penguin : Bird
    {
        public override void Fly()
        {
            throw new NotImplementedException("Penguins can't fly");
        }
    }

    class BirdService
    {
        public static void MakeBirdFly(Bird bird)
        {
            bird.Fly(); 
        }
    }

    interface IFlyingBird
    {
        void Fly();
    }

    class NewBird
    {
        public string Name { get; set; }

        public NewBird(string name)
        {
            Name = name;
        }
    }

    class Sparrow : NewBird, IFlyingBird
    {
        public Sparrow(string name) : base(name) { }

        public void Fly()
        {
            Console.WriteLine($"{Name} is flying");
        }
    }

    class NewPenguin : NewBird
    {
        public NewPenguin(string name) : base(name) { }
    }

    class FlyingBirdService
    {
        public static void MakeBirdFly(IFlyingBird bird)
        {
            bird.Fly(); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Порушення LSP ===");
            try
            {
                Bird bird1 = new Bird();
                BirdService.MakeBirdFly(bird1);

                Bird bird2 = new Penguin();
                BirdService.MakeBirdFly(bird2); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }

            Console.WriteLine("\n=== Правильний варіант (LSP) ===");

            Sparrow sparrow = new Sparrow("Sparrow");
            FlyingBirdService.MakeBirdFly(sparrow);

            NewPenguin penguin = new NewPenguin("Penguin");
            Console.WriteLine($"{penguin.Name} не літає, тому метод Fly() для нього не викликається.");

            Console.ReadLine();
        }
    }
}
