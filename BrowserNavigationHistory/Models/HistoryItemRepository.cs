using BrowserNavigationHistory.Utilities;

namespace BrowserNavigationHistory.Models
{
    /// <summary>
    /// A class for accessing and working with <see cref="HistoryItem"/> instances stored in the backend.
    /// </summary>
    public class HistoryItemRepository : IHistoryItemRepository
    {
        /// <summary>
        /// Initializes the <see cref="HistoryItemRepository"/> class
        /// </summary>
        public HistoryItemRepository()
        {
            using (var context =new BrowserHistoryContext())
            {
                context.Database.EnsureCreated();
            }
        }

        #region Get
        /// <summary>
        /// Gets all the <see cref="HistoryItem"/> instances stored.
        /// </summary>
        /// <returns>All the <see cref="HistoryItem"/> instances.</returns>
        public IEnumerable<HistoryItem> Get()
        {
            using(var context = new BrowserHistoryContext())
            {   
                return context.HistoryItems.ToList();
            }
        }

        /// <summary>
        /// Gets the history item for a specific id.
        /// </summary>
        /// <param name="id">The Id to get the history item for.</param>
        /// <returns>The history item found</returns>
        public HistoryItem? GetById(int id)
        {
            using(var context = new BrowserHistoryContext())
            {
                return context.HistoryItems.Where(historyItem => historyItem.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets history items in a paginated fashion.
        /// </summary>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The size of each page</param>
        /// <returns>Returns <paramref name="pageSize"/> items after <paramref name="pageNumber"/> pages</returns>
        public IEnumerable<HistoryItem> GetByPage(int pageNumber, int pageSize)
        {
            using(var context = new BrowserHistoryContext())
            {
                return context.HistoryItems
                                .Skip((pageNumber - 1) *  pageSize)
                                .Take(pageSize).ToList();
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new <see cref="HistoryItem"/> in the database.
        /// </summary>
        /// <param name="item">The item to save in the database.</param>
        public void Create(HistoryItem item)
        {           
            using(var context = new BrowserHistoryContext())
            {
                item.Id =  Helper.GenerateRandomId();
                context.HistoryItems.Add(item);
                context.SaveChanges();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing <see cref="HistoryItem"/>
        /// </summary>
        /// <param name="item">The item to update</param>
        public void Update(HistoryItem item)
        {
            using(var context = new BrowserHistoryContext())
            {
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes a <see cref="HistoryItem"/> from the database.
        /// </summary>
        /// <param name="id">The id of the <see cref="HistoryItem"/> to delete.</param>
        public void Delete(int id)
        {
            using(var context = new BrowserHistoryContext())
            {
                context.Remove(context.HistoryItems.SingleOrDefault(historyItem => historyItem.Id == id));
                context.SaveChanges();
            }
        }
        #endregion
    }
}
