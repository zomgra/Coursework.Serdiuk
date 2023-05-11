namespace Coursework.CLI.UI
{
    public class Logger
    {
        public static string GetUserDate(GroupsRepository repository)
        {
            Console.WriteLine("Доступні дати: ");
            Console.WriteLine(string.Join(", ", repository.Dates));
            var date = Console.ReadLine();

            if (!repository.Dates.Where(d => d.Equals(date)).Any() || string.IsNullOrWhiteSpace(date))
            {
                Console.WriteLine("Такої дати не знайденно, спробуйте ще раз");
                return GetUserDate(repository);
            }
            else
            {
                return date;
            }
        }
    }
}
