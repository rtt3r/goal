using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goal.Infra.Data.Seedwork.Tests.Mocks
{
    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator ?? throw new(nameof(enumerator));
        }

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            GC.SuppressFinalize(this);

            return new();
        }

        public ValueTask<bool> MoveNextAsync() => new(_enumerator.MoveNext());
    }
}
