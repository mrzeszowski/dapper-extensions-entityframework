using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Dapper.Extensions.EntityFramework.Tests
{
    public class IncludeTest : Test
    {
        [Fact]
        public void Include()
        {
            // assemble
            //SELECT 
            //[Production].[Product].[ProductID], 
            //[Production].[ProductModel] .[Name]
            //FROM  [Production].[Product]
            //LEFT OUTER JOIN [Production].[ProductModel] 
            //    ON [Production].[Product].[ProductModelID] = [Production].[ProductModel] .[ProductModelID]

            // act
            var products = DbConnection.Use(Context)
                .From(x => x.Products)
                .Include(x => x.ProductModel)
                .Where(x => x.ProductModel.Name != "")
                .Select(x => new
                {
                    ProductId = x.ProductID,
                    ModelName = x.ProductModel.Name
                })
                .ToList();

            // assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(504, products.Count);
        }
    }
}
