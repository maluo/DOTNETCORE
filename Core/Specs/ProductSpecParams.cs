using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specs
{
    public class ProductSpecParams
    {
        //pagination parameters
        private const int MaxPageSize = 50;
        private int _pageSize = 10;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        //allowing multiple bands passing into the params with the link
        private List<string> _brand = new List<string>();
        public List<string> Brands
        {
            get => _brand;
            set => _brand = value.SelectMany(x =>x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
        private List<string> _type = new List<string>();
        public List<string> Types
        {
            get => _type;
            set => _type = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        public string? sort { get; set; }

        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
    }
}