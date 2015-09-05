using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Core
{
    internal abstract class ConnectionContainer
    {
        private IDbConnection _dbConnection;

        public IDbConnection DbConnection 
        {
            get { return _dbConnection; }
        }

        internal ConnectionContainer(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
