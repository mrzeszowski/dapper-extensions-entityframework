using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class WhereTest : Test
    {
        [Fact]
        public void WhereSellEndDateHasValue()
        {
            // assemble
            // SELECT *
            // FROM [AdventureWorks].[Production].[Product]
            // WHERE [SellEndDate] IS NOT NULL

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Where(x => x.SellEndDate.HasValue)
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(98, products.Count);
        }

        [Fact]
        public void WhereProductNumberStartWith()
        { 
            // assemble
            // SELECT *
            // FROM [AdventureWorks].[Production].[Product]
            // WHERE [ProductNumber] LIKE 'FR-R%'

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Where(x => x.ProductNumber.StartsWith("FR-R"))
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(33, products.Count);
        }

        [Fact]
        public void WhereComplexAndWithSelectId()
        { 
            // assemble
            // SELECT [ProductID]
            // FROM [AdventureWorks].[Production].[Product]
            // WHERE [ProductID] > 400 AND [ProductID] < 600 AND [Name] LIKE '%Washer 3%'

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Where(x => x.ProductID > 400 && x.ProductID < 600)
                .Where(x => x.Name.Contains("Washer 3"))
                .Select(x => x.ProductID)
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(3, products.Count);
        }
    }
}
