using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class OrderByTest : Test
    {
        [Fact]
        public void OrderByString()
        {
            // assemble
            // SELECT [ProductNumber]
            // FROM [Production].[Product]
            // ORDER BY [ProductNumber]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .OrderBy(x => x.ProductNumber)
                .Select(x => x.ProductNumber)
                .ToList();

            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal("AR-5381", products.First());
            Assert.Equal("WB-H098", products.Last());
        }
    }
}
