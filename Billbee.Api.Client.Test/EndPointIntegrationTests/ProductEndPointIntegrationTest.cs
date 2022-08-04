using System.Net.Http.Headers;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static Product Product => new Product
        {
            Title = new List<MultiLanguageString>
            {
                new MultiLanguageString()
                {
                    Text = "the Title",
                    LanguageCode = "de"
                }
            },
            Type = 0,
            Images = new List<ArticleImage>(),
            InvoiceText = new List<MultiLanguageString>
            {
                new MultiLanguageString()
                {
                    Text = "invoice text",
                    LanguageCode = "de"
                }
            },
            SKU = "4711",
        };

        public static ArticleImage ArticleImage => new ArticleImage
        {
            Position = 1
        };

        public static ArticleImage GetArticleImage(long productId)
        {
            var ret = ArticleImage;
            ret.ArticleId = productId;
            return ret;
        }
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class ProductEndPointIntegrationTest
    {
        public TestContext TestContext { get; set; }
        private long _countExpectedAfterTest = -1;

        [TestInitialize]
        public void TestInitialize()
        {
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);

            var result =
                CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetProducts(1, int.MaxValue));
            _countExpectedAfterTest = result.Data.Count;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var result =
                CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetProducts(1, int.MaxValue));
            Assert.AreEqual(_countExpectedAfterTest, result.Data.Count);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetCategories_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCategories());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddProduct_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var product = result.Data;
            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Id);
            Assert.AreEqual(TestData.Product.Title.First().Text, product.Title.First().Text);

            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                product.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void DeleteProduct_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);
            
            CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
            CrudHelpers.GetOneExpectException<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetStocks_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetStocks());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateStockMultiple_IntegrationTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateStock_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);
            
            Assert.AreEqual(null, createdProduct.StockCurrent);

            var updateStock = new UpdateStock
            {
                OldQuantity = 0,
                NewQuantity = 1,
                Sku = TestData.Product.SKU,
                Reason = "Wareneingang"
            };
            var updateStockResult = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.UpdateStock(x), updateStock, false);
            Console.WriteLine($"CurrentStock={updateStockResult.Data.CurrentStock}");
            
            var updatedProductResult = CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            Assert.AreEqual(1, updatedProductResult.Data.StockCurrent);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetReservedAmount_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);
            
            var reservedResult = CrudHelpers.GetOneApiResult<GetReservedAmountResult>((id) => IntegrationTestHelpers.ApiClient.Products.GetReservedAmount(id),
                createdProduct.Id.ToString()!, false);
            Console.WriteLine($"ReservedAmount={reservedResult.Data.ReservedAmount}");
            Assert.AreEqual(0M, reservedResult.Data.ReservedAmount);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateStockCode_IntegrationTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetProducts_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetProducts(1, int.MaxValue));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetProduct_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);
            
            CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetCustomFields_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCustomFields(1, int.MaxValue));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetCustomField_IntegrationTest()
        {
            var customField = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCustomFields(1, int.MaxValue)).Data.FirstOrDefault();
            Assert.IsNotNull(customField);

            CrudHelpers.GetOneApiResult<ArticleCustomFieldDefinition>((id) => IntegrationTestHelpers.ApiClient.Products.GetCustomField(id),
                customField!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetPatchableProductFields_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetPatchableProductFields());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void PatchArticle_IntegrationTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetArticleImages_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetArticleImage_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            CrudHelpers.GetOneApiResult<ArticleImage>((id) => IntegrationTestHelpers.ApiClient.Products.GetArticleImage(id),
                resultImage.Data.Id);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddArticleImage_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            Assert.AreEqual((byte)1, resultImage.Data.Position);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateArticleImage_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            Assert.AreEqual((byte)1, resultImage.Data.Position);

            var updatedArticleImage = resultImage.Data;
            updatedArticleImage.Position = 2;
            var updateResult = CrudHelpers.Put<ArticleImage>(x => IntegrationTestHelpers.ApiClient.Products.UpdateArticleImage(x),
                updatedArticleImage);
            Assert.AreEqual((byte)2, updateResult.Data.Position);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddMultipleArticleImages_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(0, imagesResult.Data.Count);

            var articleImages = new List<ArticleImage>
            {
                TestData.GetArticleImage(createdProduct.Id.Value),
                TestData.GetArticleImage(createdProduct.Id.Value),
                TestData.GetArticleImage(createdProduct.Id.Value)
            };
            Console.WriteLine();
            Console.WriteLine($"Adding multiple ArticleImages (3)");
            IntegrationTestHelpers.ApiClient.Products.AddMultipleArticleImages(createdProduct.Id.Value, articleImages);
            Console.WriteLine($"Added multiple ArticleImages");
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(3, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void DeleteArticleImage_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            
            CrudHelpers.DeleteOne<ArticleImage>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteArticleImage(id),
                resultImage.Data.Id);
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(0, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void DeleteMultipleArticleImages_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage1 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            var resultImage2 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            var resultImage3 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(3, imagesResult.Data.Count);

            Console.WriteLine();
            Console.WriteLine($"Delete multiple ArticleImages with Ids={resultImage1.Data.Id},{resultImage2.Data.Id}");
            var deleteResult = IntegrationTestHelpers.ApiClient.Products.DeleteMultipleArticleImages(new List<long>
                { resultImage1.Data.Id, resultImage2.Data.Id });
            Console.WriteLine($"Deleted multiple ArticleImages");
            Assert.IsNotNull(deleteResult);
            Assert.IsNotNull(deleteResult.Data);
            Assert.AreEqual(2, deleteResult.Data.Deleted.Count);
            Assert.AreEqual(0, deleteResult.Data.NotFound.Count);
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct!.Id!.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            Assert.IsTrue(imagesResult.Data.Any(x => x.Id == resultImage3.Data.Id));
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct!.Id!.Value);
        }
    }
}