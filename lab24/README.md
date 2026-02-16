# Лабораторна робота №24 
## Тема: Strategy + Observer: динамічна підстановка алгоритмів + тести.
## Мета роботи
Застосувати патерни Strategy та Observer для створення гнучкої системи, яка дозволяє
динамічно змінювати алгоритми обробки даних та автоматично сповіщати залежні компоненти
про зміни, а також написати юніт-тести для перевірки цієї функціональності.

## Що реалізовано
### Патерн Strategy

Використовується для зміни алгоритму обробки числа.

Є інтерфейс:
```csharp
public interface INumericOperationStrategy
{
    double Execute(double value);
    string OperationName { get; }
}
```

Реалізації:

- Квадрат числа

- Куб числа

- Квадратний корінь

Клас `NumericProcessor`:
```csharp
public class NumericProcessor
{
    private INumericOperationStrategy _strategy;

    public NumericProcessor(INumericOperationStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(INumericOperationStrategy strategy)
    {
        _strategy = strategy;
    }

    public double Process(double input)
    {
        return _strategy.Execute(input);
    }

    public string CurrentOperationName => _strategy.OperationName;
}
```

Стратегію можна змінювати під час роботи програми.

### Патерн Observer

Використовується для повідомлення інших об’єктів про результат.

Клас-публішер:
```csharp
public class ResultPublisher
{
    public event Action<double, string> ResultCalculated;

    public void PublishResult(double result, string operationName)
    {
        ResultCalculated?.Invoke(result, operationName);
    }
}
```

Спостерігачі:

- Вивід у консоль

- Збереження історії

- Повідомлення, якщо результат більше порогу

### Як працює програма

У `Main`:
```csharp
var publisher = new ResultPublisher();
var processor = new NumericProcessor(new SquareOperationStrategy());

var consoleObserver = new ConsoleLoggerObserver();
consoleObserver.Subscribe(publisher);

double result = processor.Process(5);
publisher.PublishResult(result, processor.CurrentOperationName);

processor.SetStrategy(new CubeOperationStrategy());

result = processor.Process(5);
publisher.PublishResult(result, processor.CurrentOperationName);
```

Програма:
- Обчислює число.
- Публікує результат.
- Усі підписані об’єкти автоматично отримують повідомлення.

### Тестування

Можна перевірити:
```csharp
[Fact]
public void Square_ShouldReturnCorrectResult()
{
    var strategy = new SquareOperationStrategy();
    Assert.Equal(25, strategy.Execute(5));
}
```
## Висновок

У цій роботі:
- Реалізовано Strategy для зміни алгоритму.
- Реалізовано Observer через події.
- Програма легко розширюється.
- Дотримано принципів SOLID.

Система є гнучкою та зручною для додавання нових операцій або нових спостерігачів.

## Скрін
![alt text](image-1.png)