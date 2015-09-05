using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Core
{
    public static class Check
    {
        public static bool IsNull<T>(T obj)
        {
            return obj == null;
        }

        public static bool NotNull<T>(T obj)
        {
            return obj != null;
        }
    }
}
