namespace Coursework.CLI.Common
{
    // Класс для зручного відображення розкладу
    public class Logger
    {
        private readonly GroupsRepository _repository;

        public Logger(GroupsRepository repository)
        {
            _repository = repository;
        }

        public string GetValidUserDate()
        {
            Console.WriteLine("Доступні дати: ");
            Console.WriteLine(string.Join(", ", _repository.Dates));
            var date = Console.ReadLine();

            if (!_repository.Dates.Where(d => d.Equals(date)).Any() || string.IsNullOrWhiteSpace(date))
            {
                Console.WriteLine("Такої дати не знайденно, спробуйте ще раз");
                return GetValidUserDate();
            }
            else
            {
                return date;
            }
        }

        public void DisplayGroupSelectionPrompt()
        {
            Console.WriteLine("Яка група Вас цікавить?(варіанти): ");
            Console.WriteLine(string.Join(", ", _repository.Groups.Select(g => g.Number)));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Або напишіть '00' щоб побачити увесь розклад");
            Console.ResetColor();
        }

        public void DisplayGroupSchedule()
        {

            var number = Console.ReadLine();

            if (number.Equals("00"))
            {
                _repository.GetTimeTable();
                return;
            }

            if (string.IsNullOrWhiteSpace(number) || !_repository.Groups.Where(g => g.Number.Equals(number)).Any())
            {
                Console.WriteLine("Ваш запрос не був знайден, спробуйте ще раз");
                DisplayGroupSchedule();
            }
            else
            {
                var group = _repository.GetGroupByNumber(number);
                Console.WriteLine();
                Console.WriteLine($"Розклад заннять групи {number}: ");
                group.PrintData();
            }
        }
    }
}
