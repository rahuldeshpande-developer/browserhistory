namespace BrowserNavigationHistory.Models
{
    public interface IHistoryItemRepository
    {
        public IEnumerable<HistoryItem> Get();

        public HistoryItem GetById(int id);

        public IEnumerable<HistoryItem> GetByPage(int pageNumber, int pageSize);

        public void Delete(int id);

        public void Update(HistoryItem item);

        public void Create(HistoryItem item);
    }
}
