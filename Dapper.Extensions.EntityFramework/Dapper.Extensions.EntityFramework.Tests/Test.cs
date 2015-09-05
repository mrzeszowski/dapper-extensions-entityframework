using Dapper.Extensions.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public abstract class Test
    {
        private IDbConnection _dbConnection;
        public IDbConnection DbConnection
        {
            get { return _dbConnection ?? (_dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksContext"].ConnectionString)); }
        }
        
        private AdventureWorksContext _context;
        public AdventureWorksContext Context
        {
            get { return _context ?? (_context = new AdventureWorksContext()); }
        }
    }
}
