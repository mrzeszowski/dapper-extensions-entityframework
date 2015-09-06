using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Extensions.EntityFramework.Model.Entities;

using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class SkipTest : Test
    {
        [Fact]
        public void Skip()
        {
            // assemble

            // act 
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .OrderBy(x => x.ProductID)
                .Skip(100)
                .ToList();

            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(404, products.Count);
            Assert.IsType<List<Product>>(products);
        }
    }
}
