using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidel_Implementation
{
    public static class LINQExtensions
    {
        public static List<int> FindAllIndex<T>(this List<T> container, Predicate<T> match)
        {
            var items = container.FindAll(match);
            List<int> indexes = new List<int>();
            foreach (var item in items)
            {
                indexes.Add(container.IndexOf(item));
            }

            return indexes;
        }
    }
}
