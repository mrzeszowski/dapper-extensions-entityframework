using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Dapper.Extensions.EntityFramework.Model.Entities;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class Select : Test
    {
        [Fact]
        public void SelectTTResult()
        {
            // assemble
            // SELECT ProductID,
            //     Name,
            //     SellStartDate
            // FROM Production.Product

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Select(x => new
                {
                    Id = x.ProductID,
                    Name = x.Name,
                    StartDate = x.SellStartDate
                }).ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotNull(products.First().Id);
            Assert.NotEmpty(products);
            Assert.Equal(504, products.Count);
        }

        [Fact]
        public void SeletcTTResultTyped()
        {
            // assemble
            // SELECT * FROM Production.Product            

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Select(x => x)
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.IsType<Product>(products.First());
            Assert.Equal(504, products.Count);
        }
    }
}
