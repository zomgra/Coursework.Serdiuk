using Coursework.CLI;
using Coursework.CLI.Common;

class Program
{
    // Вхідна точка
    static async Task Main(string[] args)
    {

        GroupsRepository repository = new GroupsRepository();
        Logger logger = new Logger(repository);
        Generator generator = new Generator();

        Console.WriteLine("Увведіть дату за якою ви хочете дізнатись розклад (дд.мм.рррр)");

        repository.InitDates(await generator.FetchDates());
        var date = logger.GetValidUserDate();

        repository.InitGroups(await generator.FetchGroups(date));

        logger.DisplayGroupSelectionPrompt();

        logger.DisplayGroupSchedule();
    }
}
