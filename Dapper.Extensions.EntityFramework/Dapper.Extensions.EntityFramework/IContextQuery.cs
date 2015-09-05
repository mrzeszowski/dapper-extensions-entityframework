using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework
{
    public interface IContextQuery<TDbContext>
    {
        TDbContext DbContext { get; }
        IDbConnection DbConnection { get; }
    }
}
