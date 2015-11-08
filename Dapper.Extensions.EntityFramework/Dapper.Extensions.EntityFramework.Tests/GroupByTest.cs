using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Dapper.Extensions.EntityFramework.Model.Entities;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class GroupByTest : Test
    {
        [Fact]
        public void GroupByTKey()
        {
            // assemble
            // SELECT[ProductSubcategoryID],
            //  COUNT(*)
            // FROM[AdventureWorks].[Production].[Product]
            // GROUP BY[ProductSubcategoryID]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .GroupBy(x => x.ProductSubcategoryID)
                .Select(x => new
                {
                    ProductSubcategoryId = x.Key,
                    ProductCount = x.Count()
                })
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(38, products.Count);
            Assert.Equal(37, products.Last().ProductSubcategoryId);
            Assert.Equal(11, products.Last().ProductCount);
        }

        [Fact]
        public void GroupByTKeyTResult()
        {
            // assemble
            // SELECT[ProductSubcategoryID],
            //  COUNT(*)
            // FROM[AdventureWorks].[Production].[Product]
            // GROUP BY[ProductSubcategoryID]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .GroupBy(x => x.ProductSubcategoryID, (key, query) => new
                {
                    ProductSubcategoryId = key,
                    ProductCount = query.Count()
                })
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(38, products.Count);
            Assert.Equal(37, products.Last().ProductSubcategoryId);
            Assert.Equal(11, products.Last().ProductCount);
        }

        [Fact]
        public void GroupByAfterSelect()
        {
            // assemble
            // SELECT[ProductSubcategoryID],
            // FROM[AdventureWorks].[Production].[Product]
            // GROUP BY[ProductSubcategoryID]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Select(x=> new
                {
                    Id = x.ProductID,
                    ProductSubcategoryId = x.ProductSubcategoryID,
                })
                .GroupBy(x => x.ProductSubcategoryId, (key, query) => new
                {
                    ProductSubcategoryId = key,
                    ProductCount = query.Count()
                })
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(38, products.Count);
            Assert.Equal(37, products.Last().ProductSubcategoryId);
            Assert.Equal(11, products.Last().ProductCount);
        }
    }
}
