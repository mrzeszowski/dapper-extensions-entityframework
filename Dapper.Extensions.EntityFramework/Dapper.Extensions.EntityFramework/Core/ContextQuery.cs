using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Core
{
    internal class ContextQuery<TDbContext> : ConnectionContainer, IContextQuery<TDbContext>
        where TDbContext : DbContext
    {
        private TDbContext _dbContext;
        public TDbContext DbContext 
        {
            get { return _dbContext; } 
        }

        internal ContextQuery(TDbContext dbContext, IDbConnection dbConnection)
            : base (dbConnection)
        {
            _dbContext = dbContext;
        }
    }
}
