using HtmlAgilityPack;
using Group = Coursework.CLI.Data.Group;

namespace Coursework.CLI
{
    // Клас для опрацювання та парсингу сайту з розкладом
    public class Generator
    {
        private readonly HttpClient _client;
        public Generator()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.kre.dp.ua/education-process/timetable"),
            };
        }

        public async Task<List<Group>> FetchGroups(string? date)
        {
            var groups = new List<Group>();
            var doc = await GetDocument(date);

            var table = doc.DocumentNode.SelectSingleNode("//table[@id='raspis']");
            var body = table.SelectNodes("tbody")[1];

            var trs = body.SelectNodes("tr");

            ParceGroups(trs, groups);

            return groups;
        }

        private async Task<HtmlDocument> GetDocument(string date)
        {
            var formContent = new FormUrlEncodedContent(new[]
                {
                 new KeyValuePair<string, string>("data", date),
                });

            var responce = await _client.PostAsync("", formContent);

            var html = await responce.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        private void ParceGroups(HtmlNodeCollection trs, List<Group> groups)
        {
            for (int i = 1; i < trs.Count; i += 3)
            {
                string group = "";
                List<string> lessons = new List<string>();
                List<string> teachers = new List<string>(); ;
                string room = "";

                if (trs[i + 0].SelectSingleNode("td[@id='group']") != null)
                {
                    group = trs[i + 0].SelectSingleNode("td[@id='group']").InnerText;
                }

                if (trs[i + 0].SelectNodes("td[@id='predm']") != null)
                {

                    var predm = trs[i + 0].SelectNodes("td[@id='predm']");
                    var lesso = predm.Select(n => n.InnerText).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    lessons = lesso;
                }

                if (trs[i + 1].SelectNodes("td[@id='prep']") != null)
                {

                    var prep = trs[i + 1].SelectNodes("td[@id='prep']");
                    var tech = prep.Select(n => n.InnerText).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    teachers = tech;
                }



                var auds = trs[i + 2].SelectNodes("td[@id='aud']");

                if (auds.Select(a => a.SelectSingleNode("br")) != null)
                {
                    var na = auds.Where(a => a.SelectSingleNode("br") != null).ToList();
                    if (na.Any())
                    {
                        room = na[0].InnerText;
                    }
                }
                var newgroup = new Group() { Lessons = lessons, Number = group, Teachers = teachers, Room = room };
                groups.Add(newgroup);
            }
        }

        public async Task<List<string>> FetchDates()
        {
            var doc = await GetDocument("");

            var dateInputs = doc.DocumentNode.SelectNodes("//input[@type='submit' and contains(@value, '.')]");
            return dateInputs.Select(s=>s.GetAttributeValue("value", "")).ToList();
        }
    }
}
