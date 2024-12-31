using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specifications
{
    public class Pagination<T> where T : class
    {
        public Pagination() { }
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public IReadOnlyList<T> Data { get; }
    }
}
