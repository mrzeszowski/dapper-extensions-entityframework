using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework
{
    public interface IQuery<T>
        where T : class
    {
        // select
        IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class;

        // to list
        List<T> ToList();
    }
}
