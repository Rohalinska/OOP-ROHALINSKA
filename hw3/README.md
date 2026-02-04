# Принципи SRP, OCP, LSP (SOLID) 

Принципи SOLID допомагають писати код так, щоб його було легше розуміти, змінювати і тестувати. Нижче - коротко і по-простому про кожен із принципів з прикладами.

## 1. SRP - один клас = одна задача

SRP (Single Responsibility Principle) означає, що клас повинен відповідати лише за одну конкретну річ.
Якщо клас робить усе підряд (і логіку, і збереження в файл, і вивід на екран), його важко міняти і підтримувати.

### Поганий приклад
```cshar
class ReportService
{
    public void CreateReport() { }
    public void SaveToFile() { }
    public void Print() { }
}
```

Тут один клас і створює звіт, і зберігає його, і друкує.
Якщо зміниться спосіб збереження - доведеться лізти в цей же клас.

### Краще рішення

```cshar
class ReportCreator
{
    public void CreateReport() { }
}

class ReportSaver
{
    public void SaveToFile() { }
}

class ReportPrinter
{
    public void Print() { }
}
```

Тепер кожен клас відповідає за свою просту задачу.
Код легше міняти і розуміти.

## 2. OCP - розширюй, але не ламаючи старе

OCP (Open/Closed Principle) означає, що код краще розширювати, а не переписувати старий.
Тобто коли додаєш нову функцію, не маєш ламати те, що вже працює.

### Поганий приклад
```cshar
class DiscountService
{
    public double GetDiscount(string type)
    {
        if (type == "student") return 0.2;
        if (type == "vip") return 0.3;
        return 0;
    }
}
```

Щоб додати новий тип знижки, треба постійно міняти цей метод.

### Краще рішення
```cshar
interface IDiscount
{
    double GetDiscount();
}

class StudentDiscount : IDiscount
{
    public double GetDiscount() => 0.2;
}

class VipDiscount : IDiscount
{
    public double GetDiscount() => 0.3;
}
```

Тепер можна додавати нові знижки, не змінюючи старий код - просто створюєш новий клас.

## 3. LSP - підкласи не повинні ламати логіку

LSP (Liskov Substitution Principle) означає, що підклас має працювати так, щоб його можна було спокійно підставити замість базового класу.

### Поганий приклад
```cshar
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
        throw new NotSupportedException();
    }
}
```

Код думає, що будь-який птах літає, але пінгвін - ні.
У результаті програма може впасти.

### Краще рішення
```cshar
interface IFlyingBird
{
    void Fly();
}

class Bird { }

class Sparrow : Bird, IFlyingBird
{
    public void Fly()
    {
        Console.WriteLine("Sparrow is flying");
    }
}

class Penguin : Bird { }
```

Тепер літати можуть тільки ті, хто реально вміє це робити.

## Bисновок

SRP - не роби з одного класу “комбайн на всі задачі”.

OCP - додавай нове, не ламаючи старе.

LSP - підкласи не повинні вести себе “дивно” у порівнянні з базовим класом.

Разом із ISP та DIP ці принципи допомагають робити код чистішим, простішим і менш болючим для змін.