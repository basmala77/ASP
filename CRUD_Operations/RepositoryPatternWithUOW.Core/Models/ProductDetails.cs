using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Allergens { get; set; }
    }
}
