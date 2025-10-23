using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestHelpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        private const int MaxPageSize = 50;
        
        //default page index is 1
        public int PageIndex { get; set; } = 1;

        //manage page size with a maximum limit
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        //total items in the collection, not just the current page
        public int Count { get; set; }
        //data for the current page
        public IReadOnlyList<T> Data { get; set; }
    }
}