using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MoqEFCoreExtension
{
     class UnitTestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public UnitTestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public UnitTestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new UnitTestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new UnitTestAsyncQueryProvider<T>(this); }
        }
    }
}
