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
        public ProductSpec(ProductSpecParams specParams) : 
            base(x =>
            (string.IsNullOrWhiteSpace(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
            (!specParams.Types.Any() || specParams.Types.Contains(x.Type)))
        {
            if (string.IsNullOrWhiteSpace(specParams.sort)) return;

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.sort.ToLower())
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