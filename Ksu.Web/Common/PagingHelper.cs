using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arabia.Web.Common
{

    public class PagingHelper<T> : List<T>
    {

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int ViewsCount { get; private set; }
        public PagingHelper(IQueryable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            this.AddRange(source.Skip((PageIndex == 0 ? 0 : PageIndex -1) * PageSize).Take(PageSize));
        }
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex + 1 <= TotalPages); }
        }


    }
}