using Coursework.CLI.Data;

namespace Coursework.CLI
{
    public class GroupsRepository
    {
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<string> Dates { get; set; } = new List<string>();

        public void InitGroups(List<Group> groups) => Groups.AddRange(groups);
        public void InitDates(List<string> dates) => Dates.AddRange(dates);
        public Group GetGroupByNumber(string number)
        {
            return Groups.FirstOrDefault(g => g.Number.Contains(number));
        }

        public void GetTimeTable()
        {
            foreach (var group in Groups)
            {
                group.GetAllData();
            }
        }
    }
}
