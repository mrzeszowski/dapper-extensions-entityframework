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
        where T : class
    {
        private IDbSet<T> _dbSet;
        private IQueryable<T> _queryable;
        private ConstructorInfo _newCtor;

        internal IQueryable<T> Queryable
        {
            get
            {
                return _queryable ?? (_queryable = _dbSet);
            }
        }

        #region Constructors

        internal Query(IDbSet<T> dbSet, IDbConnection dbConnection)
            : base(dbConnection)
        {
            _dbSet = dbSet;
        }

        private Query(IQueryable<T> queryable, IDbConnection dbConnection)
            : base(dbConnection)
        {
            _queryable = queryable;
        }

        private Query(IQueryable<T> queryable, ConstructorInfo newCtor, IDbConnection dbConnection)
            : this(queryable, dbConnection)
        {
            _newCtor = newCtor;
        }

        #endregion

        public IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
            where TResult : class
        {
            CatchExpressionCtor(selector.Body);

            return new Query<TResult>(
                Queryable.Select(selector),
                _newCtor,
                base.DbConnection
                );
        }

        public List<T> ToList()
        {
            if (Check.NotNull(_newCtor))
            {
                return DbConnection.Query(Queryable.ToString())
                    .Select(x =>
                        _newCtor.Invoke(
                            ((IDictionary<string, object>)x).Values.ToArray()
                            ) as T
                        )
                    .ToList();
            }
            return DbConnection.Query<T>(Queryable.ToString()).ToList();
        }

        #region Private Methods

        private void CatchExpressionCtor(Expression expression)
        {
            if (expression is NewExpression)
            {
                _newCtor = (expression as NewExpression).Constructor;
            }
        }

        #endregion
    }
}
