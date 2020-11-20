using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Common
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPageCount { get; private set; }

        public int TotalRecordCount { get; set; }

        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedItemsViewModel(int pageIndex, int pageSize, int totalPageCount, int totalRecordCount, IEnumerable<TEntity> data)
        {
            this.CurrentPage = pageIndex;
            this.PageSize = pageSize;
            this.TotalPageCount = totalPageCount;
            this.TotalRecordCount = totalRecordCount;
            this.Data = data;
        }
    }
}
