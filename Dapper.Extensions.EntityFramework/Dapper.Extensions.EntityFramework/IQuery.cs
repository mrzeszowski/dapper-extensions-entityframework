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
        // where
        IQuery<T> Where(Expression<Func<T, bool>> predicate);

        // select
        IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        // to list
        List<T> ToList();
    }
}
