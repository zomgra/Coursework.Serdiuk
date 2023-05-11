using Coursework.CLI;
using Coursework.CLI.UI;

class Program
{
    static async Task Main(string[] args)
    {
        var repository = new GroupsRepository();
        var generator = new Generator();

        Console.WriteLine("Увведіть дату за якою ви хочете дізнатись розклад (дд.мм.рррр)");

        repository.InitDates(await generator.FetchDates());
        var date = GetUserDate(repository);

        repository.InitGroups(generator.FetchGroups(date).GetAwaiter().GetResult());

        Console.WriteLine("Яка група Вас цікавить?(варіанти): ");
        Console.WriteLine(string.Join(", ", repository.Groups.Select(g => g.Number)));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Або напишіть '00' щоб побачити увесь розклад");
        Console.ResetColor();

        var number = Console.ReadLine();

        ViewData(repository, number);
    }

    private static string GetUserDate(GroupsRepository repository)
    {
        Console.WriteLine("Доступні дати: ");
        Console.WriteLine(string.Join(", ", repository.Dates));
        var date = Console.ReadLine();

        if (!repository.Dates.Where(d=> d.Equals(date)).Any() || string.IsNullOrWhiteSpace(date))
        {
            Console.WriteLine("Такої дати не знайденно, спробуйте ще раз");
            return GetUserDate(repository);
        }
        else
        {
            return date;
        }
    }

    private static void ViewData(GroupsRepository repository, string number)
    {
        
        if (number.Equals("00"))
        {
            repository.GetTimeTable();
        }

        if (string.IsNullOrWhiteSpace(number) || !repository.Groups.Where(g => g.Number.Equals(number)).Any())
        {
            Console.WriteLine("Ваш запрос не був знайден, спробуйте ще раз");
            ViewData(repository, number);
        }
        else
        {
            var group = repository.GetGroupByNumber(number);

            Console.WriteLine($"Розклад заннять групи {number}: ");
            group.PrintData();
        }
    }
}
