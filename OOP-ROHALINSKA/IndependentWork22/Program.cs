using System;

namespace IndependentWork22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(" 1. Створення сторінок книги (Leaf) ");
            IComponent page1 = new Page(1, "Вступ до об'єктно-орієнтованого проектування та шаблонів."); 
            IComponent page2 = new Page(2, "Патерн Компонувальник дозволяє будувати ієрархічні дерева об'єктів."); 
            IComponent page3 = new Page(3, "Декоратор динамічно розширює поведінку, уникаючи жорсткого наслідування класів."); 

            Console.WriteLine(" 2. Формування ієрархії книги (Composite) ");
            Chapter introduction = new Chapter("Вступна частина");
            introduction.Add(page1);

            Chapter mainPart = new Chapter("Основна теорія");
            mainPart.Add(page2);
            mainPart.Add(page3);

            Chapter fullBook = new Chapter("Паттерни проектування: Посібник");
            fullBook.Add(introduction);
            fullBook.Add(mainPart);

            Console.WriteLine(" Відображення структури без декораторів ");
            fullBook.Display();
            Console.WriteLine($"Загальна кількість слів у книзі: {fullBook.GetWordCount()}");

            Console.WriteLine(" 3. Застосування декораторів ");

            Console.WriteLine("[Декоруємо Сторінку 1 перевіркою орфографії]:");
            IComponent checkedPage = new SpellCheckDecorator(page1);
            checkedPage.Display();

            Console.WriteLine("[Декоруємо весь Основний Розділ лімітом у 15 слів (там зараз 17 слів)]:");
            IComponent limitedChapter = new WordLimitDecorator(mainPart, 15);
            limitedChapter.Display();

            Console.WriteLine("[Декоруємо ВСЮ КНИГУ одночасно лімітом (на 50 слів) та перевіркою орфографії]:");
            IComponent fullyDecoratedBook = new SpellCheckDecorator(new WordLimitDecorator(fullBook, 50));
            fullyDecoratedBook.Display();

            Console.ReadLine();
        }
    }
}