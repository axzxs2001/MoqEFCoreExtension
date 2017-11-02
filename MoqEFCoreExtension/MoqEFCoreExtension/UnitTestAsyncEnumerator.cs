using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoqEFCoreExtension
{
      class UnitTestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public UnitTestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
}
