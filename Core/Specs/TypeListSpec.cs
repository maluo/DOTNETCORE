using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specs
{
    public class TypeListSpec : BaseSepc<Product, string>
    {
        public TypeListSpec()
        {
            AddSelect(t => t.Type);
            ApplyDistinct();
        }
    }
}