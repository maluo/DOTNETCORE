using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specs
{
    public class ProductSpecParams
    {
        //allowing multiple bands passing into the params with the link
        private List<string> _brand = new List<string>();
        public List<string> Brand
        {
            get => _brand;
            set => _brand = value.SelectMany(x =>x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
        private List<string> _type = new List<string>();
        public List<string> Type
        {
            get => _type;
            set => _type = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
}