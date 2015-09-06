using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Extensions.EntityFramework.Model.Entities;

using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class SelectTest : Test
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
        public void Select()
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
            Assert.IsType<List<Product>>(products);
            Assert.Equal(504, products.Count);
        }

        [Fact]
        public void SelectName()
        { 
            // assemble
            // SELECT [Name]
            // FROM [AdventureWorks].[Production].[Product]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Select(x => x.Name)
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.IsType<List<string>>(products);
            Assert.NotEmpty(products);
            Assert.Equal(504, products.Count);
        }
    }
}
