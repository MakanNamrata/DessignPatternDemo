namespace CompanyApplication.Shared
{
    public class PagedList<T>
    {
        public PagedList()
        {

        }

        public List<T> Items { get; set;  }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }

        public PagedList(List<T> items, int currentPage, int totalPage, int pageSize, long totalCount)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPage = totalPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
