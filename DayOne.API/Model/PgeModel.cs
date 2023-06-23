namespace DayOne.API.Model
{
    public class PageModel
    {
        const int maxPageSize = 25;
        private int _pageSize = 10;
        public int PageNumber { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize=(value>maxPageSize) ? value : maxPageSize;
            }
        }

    }
}
