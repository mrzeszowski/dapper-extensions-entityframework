using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework
{
    public interface IQuery<T>
    {
        // order by
        IQuery<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);

        // skip
        IQuery<T> Skip(int count);

        // take
        IQuery<T> Take(int count);

        // where
        IQuery<T> Where(Expression<Func<T, bool>> predicate);

        // select
        IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        // to list
        List<T> ToList();
    }
}
