using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Extensions.EntityFramework.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Dapper.Extensions.EntityFramework
{
    public static class QueryFactory
    {
        public static IContextQuery<TDbContext> Use<TDbContext>(this IDbConnection dbConnection)
            where TDbContext : DbContext
        {
            var dbContext = Activator.CreateInstance<TDbContext>();
            return new ContextQuery<TDbContext>(dbContext, dbConnection);
        }

        public static IContextQuery<TDbContext> Use<TDbContext>(this IDbConnection dbConnection, TDbContext dbContext)
            where TDbContext : DbContext
        {
            return new ContextQuery<TDbContext>(dbContext, dbConnection);
        }

        public static IQuery<TEntity> From<TEntity, TDbContext>(this IContextQuery<TDbContext> contextQuery,
            Expression<Func<TDbContext, IDbSet<TEntity>>> predicate)
            where TDbContext : DbContext
            where TEntity : class
        {
            var dbSet = predicate.Compile().Invoke(contextQuery.DbContext) as IDbSet<TEntity>;
            return new Query<TEntity>(dbSet, contextQuery.DbConnection);
        }
    }
}
