using Dapper.Extensions.EntityFramework.Model;
using Dapper.Extensions.EntityFramework.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class ToListTest : Test
    {
        [Fact]
        public void ToList()
        {
            // assemble
            // SELECT * FROM Production.Products

            // act
            var products = DbConnection.Use(Context).From(x => x.Products).ToList();

            // assert
            Assert.NotNull(products);
            Assert.IsType<List<Product>>(products);
            Assert.Equal(products.Count(), 504);
        }
    }
}
