using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extensions.EntityFramework.Exceptions
{
    public class WrongResultTypeException : Exception
    {
        public WrongResultTypeException() 
            : base("Select type is different than choosen entity set.")
        {
        }
    }
}
