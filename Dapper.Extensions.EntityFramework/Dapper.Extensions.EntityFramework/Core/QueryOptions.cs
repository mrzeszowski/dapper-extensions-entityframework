using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Core
{
    internal class QueryOptions
    {
        public ConstructorInfo NewCtor { get; set; }

        public bool GroupByInvoked { get; set; }
    }
}
