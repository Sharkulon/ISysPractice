namespace ISysWebAppBack.Services
{
    public sealed class PagesList<T> : List<T>
    {/// <summary>
     /// page's index
     /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// page's size
        /// </summary>
        public static int PageSize { get; } = Config.PageNumber;

        /// <summary>
        /// constructor for PagesList class
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageIndex"></param>
        public PagesList(List<T> items, int count, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            this.AddRange(items);
        }
        /// <summary>
        /// previous page check
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;
        /// <summary>
        /// next page check
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// function for creating new list
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static PagesList<T> Create(List<T> source, int pageIndex)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
            return new PagesList<T>(items, count, pageIndex);
        }
    }
}
