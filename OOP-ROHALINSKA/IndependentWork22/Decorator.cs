namespace IndependentWork22
{
    public abstract class BookDecorator : IComponent
    {
        protected IComponent _component;

        protected BookDecorator(IComponent component)
        {
            _component = component;
        }

        public virtual int GetWordCount()
        {
            return _component.GetWordCount();
        }

        public virtual void Display(int indent = 0)
        {
            _component.Display(indent);
        }
    }

    public class SpellCheckDecorator : BookDecorator
    {
        public SpellCheckDecorator(IComponent component) : base(component) { }

        public override void Display(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)} [SpellCheck: Орфографію перевірено. Помилок не знайдено]");
            base.Display(indent);
        }
    }

    public class WordLimitDecorator : BookDecorator
    {
        private readonly int _maxWords;

        public WordLimitDecorator(IComponent component, int maxWords) : base(component)
        {
            _maxWords = maxWords;
        }

        public override void Display(int indent = 0)
        {
            int currentWords = base.GetWordCount();
            if (currentWords > _maxWords)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{new string(' ', indent)} [Увага! Перевищено ліміт слів! Максимум: {_maxWords}, Поточна кількість: {currentWords}]");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{new string(' ', indent)} [Обмеження слів в нормі (менше {_maxWords})]");
            }
            base.Display(indent);
        }
    }
}