namespace IndependentWork22
{
    public class Page : IComponent
    {
        public int PageNumber { get; private set; }
        public string Content { get; private set; }

        public Page(int pageNumber, string content)
        {
            PageNumber = pageNumber;
            Content = content;
        }

        public int GetWordCount()
        {
            if (string.IsNullOrWhiteSpace(Content)) return 0;
            
            return Content.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public void Display(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}📄 Сторінка {PageNumber} (Слів: {GetWordCount()})");
        }
    }

    public class Chapter : IComponent
    {
        public string Title { get; private set; }
        private readonly List<IComponent> _elements = new List<IComponent>();

        public Chapter(string title)
        {
            Title = title;
        }

        public void Add(IComponent element)
        {
            _elements.Add(element);
        }

        public void Remove(IComponent element)
        {
            _elements.Remove(element);
        }

        public int GetWordCount()
        {
            int totalWords = 0;
            foreach (var element in _elements)
            {
                totalWords += element.GetWordCount();
            }
            return totalWords;
        }

        public void Display(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)} Розділ: \"{Title}\" (Всього слів: {GetWordCount()})");
            foreach (var element in _elements)
            {
                element.Display(indent + 4);
            }
        }
    }
}