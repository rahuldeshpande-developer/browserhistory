using BrowserNavigationHistory.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BrowserNavigationHistory.Models
{
    /// <summary>
    /// A context class
    /// </summary>
    public class BrowserHistoryContext : DbContext
    {
        /// <summary>
        /// Gathers seed information by reading it from a local file.
        /// </summary>
        /// <returns>Returns a list of <see href="HistoryItem"></see> instances.</returns>
        private List<HistoryItem> GetSeedData()
        {
            string fileName = "browserhistorydata.csv";
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = "Data";
            string fullPath = Path.Combine(currentDirectory, path, fileName);

            var result = new List<HistoryItem>();
            List<HistoryItem> historyItems = new List<HistoryItem>();
            int maxSize = 1000;
            int count = 0;
            using (StreamReader reader = new StreamReader(fullPath))
            {
                try
                {
                    while (!reader.EndOfStream && count < maxSize)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        HistoryItem historyItem = new HistoryItem()
                        {
                            Id = Helper.GenerateRandomId(),
                            Url = values[0],
                            Title = values[0].Substring(10),
                            TimeStamp = Helper.UnixTimeStampToDateTime(Convert.ToDouble(values[1]))
                        };
                        historyItems.Add(historyItem);
                        count += 1;                    
                    }
                }
                catch
                {
                    count += 1;    
                }
            }
            return historyItems;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var historyItems = GetSeedData();
            foreach (var item in historyItems)
            {
                modelBuilder.Entity<HistoryItem>().HasData(item);
            }
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BrowserHistoryNavDb");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<HistoryItem> HistoryItems { get;set;} = null;
    }
}
