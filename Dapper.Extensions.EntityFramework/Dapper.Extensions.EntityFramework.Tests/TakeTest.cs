using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Extensions.EntityFramework.Model.Entities;

using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class TakeTest : Test
    {
        [Fact]
        public void Take()
        {
            // assemble
            // SELECT TOP (60) *
            // FROM [Production].[Product]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Take(60)
                .ToList();

            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(60, products.Count);
            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void TakeWhereNameStartWithSelectProductNumber()
        {
            // assemble
            // SELECT TOP (60) [ProductNumber]
            // FROM [Production].[Product]
            // WHERE [Name] LIKE 'Chain%'

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Where(x => x.Name.StartsWith("Chain"))
                .Select(x => x.ProductNumber)
                .Take(60)
                .ToList();

            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(5, products.Count);
            Assert.IsType<List<string>>(products);
        }
    }
}
