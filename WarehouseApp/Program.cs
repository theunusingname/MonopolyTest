using WarehouseApp.Warehouse;

namespace WarehouseApp;

class Program
{
    static void Main(string[] args)
    {
        decimal[,] testPallets =
        {
            { 5, 5, 0.2m },
            { 3, 6, 0.2m },
            { 3.5m, 5.6m, 0.2m },
            { 3, 5, 0.2m },
            { 3, 6, 0.2m }
        };

        decimal[,] testBoxes =
        {
            { 1, 1, 4, 1 },
            { 2, 2, 2, 3 },
            { 3, 3, 3, 2 },
            { 1, 2, 2, 5 },
            { 2, 1, 2, 3 },
            { 3, 3, 3, 2 },
            { 1, 1, 4, 1 },
            { 2, 2, 2, 3 },
            { 3, 3, 3, 2 },
            { 1, 2, 2, 5 },
            { 2, 1, 2, 3 },
            { 3, 3, 3, 2 },
        };

        var warehouse = new Warehouse.Warehouse(new InMemoryRepo());
        var palleteIds = new List<string>();
        var boxIds = new List<string>();
        for (var i = 0; i <= testPallets.GetUpperBound(0); i++)
        {
            palleteIds.Add(warehouse.AddPallete(testPallets[i, 0], testPallets[i, 1], testPallets[i, 2]));
        }

        for (var i = 0; i <= testBoxes.GetUpperBound(0); i++)
        {
            boxIds.Add(warehouse.AddBoxWithExpireDate(testBoxes[i, 0], testBoxes[i, 1], testBoxes[i, 2],
                testBoxes[i, 3],
                DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(i/2)), palleteIds[i % palleteIds.Count]));
        }

        for (var i = 0; i <= testBoxes.GetUpperBound(0); i++)
        {
            boxIds.Add(warehouse.AddBoxWithManufacturedDate(testBoxes[i, 0], testBoxes[i, 1], testBoxes[i, 2],
                testBoxes[i, 3],
                DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(i/2)), palleteIds[i % palleteIds.Count]));
        }

        Console.WriteLine("Grouped pallets");
        var grouped = warehouse.GetGroupedPallets();

        foreach (var keyValuePair in grouped)
        {
            Console.WriteLine($"\nDate:{keyValuePair.Key}");
            ShowPallets(keyValuePair.Value);
        }

        var top = warehouse.GetTop3ByExpireDate();
        Console.WriteLine("______________________");
        Console.WriteLine("\nTop 3 by expire date");
        ShowPallets(top);
    }

    private static void ShowPallets(IEnumerable<Pallete> palletes)
    {
        foreach (var pallete in palletes)
        {
            Console.WriteLine(pallete);
            foreach (var box in pallete.GetBoxes())
            {
                Console.WriteLine(box);
            }
        }
    }
}


// ТЗ см. ниже, результат выложить на GitHub
//  
// Решение задачи будет оценено нашими разработчиками и использовано для общения с тобой на техническом онлайн интервью
//
// Разработать консольное .NET приложение для склада, удовлетворяющее следующим требованиям:
// - Построить иерархию классов, описывающих объекты на складе - паллеты и коробки:
// - Помимо общего набора стандартных свойств (ID, ширина, высота, глубина, вес), паллета может содержать в себе коробки.
// - У коробки должен быть указан срок годности или дата производства. Если указана дата производства, то срок годности вычисляется из даты производства плюс 100 дней.
// - Срок годности и дата производства — это конкретная дата без времени (например, 01.01.2023).
// - Срок годности паллеты вычисляется из наименьшего срока годности коробки, вложенной в паллету. Вес паллеты вычисляется из суммы веса вложенных коробок + 30кг.
// - Объем коробки вычисляется как произведение ширины, высоты и глубины.
// - Объем паллеты вычисляется как сумма объема всех находящихся на ней коробок и произведения ширины, высоты и глубины паллеты.
// - Каждая коробка не должна превышать по размерам паллету (по ширине и глубине).
// - Консольное приложение:
// - Получение данных для приложения можно организовать одним из способов:
// - Генерация прямо в приложении
// - Чтение из файла или БД
// - Пользовательский ввод
// - Вывести на экран:
//  - Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.
// - 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.
// - (Опционально) Покрыть функционал unit-тестами.
// - (Очень желательно) Код должен быть написан в соответствии с https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
// - (Совершенно не обязательно) Вместо консольного приложения сделать полноценный пользовательский интерфейс. На оценку решения никак не влияет.