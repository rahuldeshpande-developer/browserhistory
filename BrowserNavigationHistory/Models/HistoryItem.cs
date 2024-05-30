namespace BrowserNavigationHistory.Models
{
    /// <summary>
    /// Represents every history item of a user's browser navigation history.
    /// </summary>
    public class HistoryItem
    {
        /// <summary>
        /// A unique ID for every history item.
        /// </summary>
        public int Id { get;set;}

        /// <summary>
        /// The ID of the User who performed the navigation.
        /// </summary>
        public int UserId { get; set;}

        /// <summary>
        /// The Url of the page the user navigated to.
        /// </summary>
        public string Url { get;set;}

        /// <summary>
        /// The title of the page the user navigated to.
        /// </summary>
        public string Title { get;set;}

        /// <summary>
        /// The timestamp of when the user navigated to this page.
        /// </summary>
        public DateTime TimeStamp { get;set;}
    }
}

