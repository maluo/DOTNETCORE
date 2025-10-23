using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specs
{
    public class ProductSpec : BaseSepc<Product>
    {
        public ProductSpec(string? brand, string? type, string? sort): 
            base(
                (x => (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
                (string.IsNullOrWhiteSpace(type) || x.Type == type)))
        {
            if (string.IsNullOrWhiteSpace(sort)) return;
            switch (sort.ToLower())
            {
                case "priceasc":
                    AddOrderBy(p => p.Price);
                    break;
                case "pricedesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                case "nameasc":
                    AddOrderBy(p => p.Name);
                    break;
                case "namedesc":
                    AddOrderByDesc(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}