using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Reflection;
using Dapper.Extensions.EntityFramework.Exceptions;

namespace Dapper.Extensions.EntityFramework.Core
{
    internal class Query<T> : ConnectionContainer, IQuery<T>
    {
        private IQueryable<T> _queryable;
        private QueryOptions _options;

        internal IQueryable<T> Queryable
        {
            get
            {
                return _queryable;
            }
            set
            {
                _queryable = value;
            }
        }

        #region Constructors

        internal Query(IQueryable<T> queryable, IDbConnection dbConnection)
            : base(dbConnection)
        {
            _queryable = queryable;

            if (Check.IsNull(_options))
            {
                _options = new QueryOptions();
            }
        }

        private Query(IQueryable<T> queryable, QueryOptions options, IDbConnection dbConnection)
            : this(queryable, dbConnection)
        {
            _options = options;
        }

        #endregion

        public IQuery<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _options.GroupByInvoked = true;

            return new Query<IGrouping<TKey, T>>(
                Queryable.GroupBy(keySelector),
                _options,
                DbConnection
            );
        }

        public IQuery<TResult> GroupBy<TKey, TResult>(Expression<Func<T, TKey>> keySelector, Expression<Func<TKey, IEnumerable<T>, TResult>> resultSelector)
        {
            _options.GroupByInvoked = true;

            CatchExpressionCtor(resultSelector.Body);

            return new Query<TResult>(
                Queryable.GroupBy(keySelector, resultSelector),
                _options,
                DbConnection
            );
        }

        public IQuery<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            Queryable = Queryable.Include(path);
            return this;
        }

        public IQuery<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            Queryable = Queryable.OrderBy(keySelector);
            return this;
        }

        public IQuery<T> Skip(int count)
        {
            Queryable = Queryable.Skip(count);
            return this;
        }

        public IQuery<T> Take(int count)
        {
            Queryable = Queryable.Take(count);
            return this;
        }

        public IQuery<T> Where(Expression<Func<T, bool>> predicate)
        {
            Queryable = Queryable.Where(predicate);
            return this;
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            CatchExpressionCtor(selector.Body);

            return new Query<TResult>(
                Queryable.Select(selector),
                _options,
                DbConnection
                );
        }

        public List<T> ToList()
        {
            if (Check.NotNull(_options.NewCtor))
            {
                return DbConnection.Query(Queryable.ToString())
                    .Select(x =>
                        _options.NewCtor.Invoke(
                            ((IDictionary<string, object>)x).Values.Skip(_options.GroupByInvoked ? 1 : 0).ToArray()
                            )
                        )
                    .Cast<T>()
                    .ToList<T>();
            }
            return DbConnection.Query<T>(Queryable.ToString()).ToList();
        }

        #region Private Methods

        private void CatchExpressionCtor(Expression expression)
        {
            if (expression is NewExpression)
            {
                _options.NewCtor = (expression as NewExpression).Constructor;
            }
        }

        #endregion
    }
}
