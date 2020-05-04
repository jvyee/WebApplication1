using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public class AsyncEnumerableQuery<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
{
    public AsyncEnumerableQuery(IEnumerable<T> enumerable) : base(enumerable)
    {
    }

    public AsyncEnumerableQuery(Expression expression) : base(expression)
    {
    }

    public IDbAsyncEnumerator<T> GetAsyncEnumerator()
    {
        return new AsyncEnumerator<T>(((IEnumerable<T>)this).GetEnumerator());
    }

    IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
    {
        return GetAsyncEnumerator();
    }

    IQueryProvider IQueryable.Provider
    {
        get { return new AsyncQueryProvider<T>(this); }
    }


    private class AsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public void Dispose()
        {
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_enumerator.MoveNext());
        }

        public T Current => _enumerator.Current;

        object IDbAsyncEnumerator.Current => Current;
    }
}
