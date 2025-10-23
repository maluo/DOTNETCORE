using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specs
{
    public class BrandListSpec : BaseSepc<Product, string>
    {
        public BrandListSpec()
        {
            AddSelect(b => b.Brand);
            ApplyDistinct();
        }
    }
}