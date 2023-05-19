namespace Coursework.CLI.Data
{
    // Клас для роботи з групою
    public class Group
    {
        public string Number { get; set; } = string.Empty;
        public List<string> Teachers { get; set; } = new List<string>();
        public List<string> Lessons { get; set; } = new List<string>();
        public string Room { get; set; } = string.Empty ;

        public void PrintData()
        {
            Console.WriteLine("----------------------");
            if(Lessons.Any())
                Console.WriteLine($"Пари групи: {string.Join(", ", Lessons)}");
            if (Teachers.Any())
                Console.WriteLine($"Вчителя: {string.Join(", ", Teachers)}");
            if (!string.IsNullOrWhiteSpace(Room))
                Console.WriteLine($"Клас для групи: {Room}");
            Console.WriteLine("----------------------");
        }
        public void PrintFullData()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine($"Група: №{Number}");
            if (Lessons.Any())
                Console.WriteLine($"Пари групи: {string.Join(", ", Lessons)}");
            if (Teachers.Any())
                Console.WriteLine($"Вчителя: {string.Join(", ", Teachers)}");
            if (!string.IsNullOrWhiteSpace(Room))
                if (!string.IsNullOrWhiteSpace(Room))
                Console.WriteLine($"Клас для групи: {Room}");
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }
    }
}
