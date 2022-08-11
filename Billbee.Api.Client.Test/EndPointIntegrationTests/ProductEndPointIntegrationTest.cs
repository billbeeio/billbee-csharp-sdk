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
            Description = new List<MultiLanguageString>
            {
                new MultiLanguageString
                {
                    Text = "desc",
                    LanguageCode = "de"
                }
            }
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
#pragma warning disable CS8618
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618
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
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
            
            var result =
                CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetProducts(1, int.MaxValue));
            Assert.AreEqual(_countExpectedAfterTest, result.Data.Count);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetCategories_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCategories());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_AddProduct_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                TestData.Product);
            var product = result.Data;
            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Id);
            Assert.AreEqual(TestData.Product.SKU, product.SKU);
            Assert.AreEqual(TestData.Product.Title.First().Text, product.Title.First().Text);

            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                product.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_DeleteProduct_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            
            CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
            CrudHelpers.GetOneExpectException<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetStocks_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetStocks());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_UpdateStockMultiple_IntegrationTest()
        {
            var product1 = _createProduct("4711");
            Assert.IsNotNull(product1.Id);
            var product2 = _createProduct("4712");
            Assert.IsNotNull(product2.Id);
            
            Assert.AreEqual(null, product1.StockCurrent);
            Assert.AreEqual(null, product2.StockCurrent);

            var updateStocks = new List<UpdateStock>
            {
                new UpdateStock
                {
                    Reason = "Wareneingang",
                    Sku = product1.SKU,
                    NewQuantity = 1
                },
                new UpdateStock
                {
                    Reason = "Wareneingang",
                    Sku = product2.SKU,
                    NewQuantity = 1
                }
            };

            var results = IntegrationTestHelpers.ApiClient.Products.UpdateStockMultiple(updateStocks);
            foreach (var result in results)
            {
                Assert.IsTrue(result.Success);
            }
            
            var updatedProduct1Result = CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                product1.Id.ToString()!);
            var updatedProduct1 = updatedProduct1Result.Data;
            Assert.IsNotNull(updatedProduct1);
            Assert.AreEqual(1, updatedProduct1.StockCurrent);
            
            var updatedProduct2Result = CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                product2.Id.ToString()!);
            var updatedProduct2 = updatedProduct2Result.Data;
            Assert.IsNotNull(updatedProduct2);
            Assert.AreEqual(1, updatedProduct2.StockCurrent);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                product1.Id.Value);
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                product2.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_UpdateStock_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            
            Assert.IsNull(createdProduct.StockCurrent);

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
            var updatedProduct = updatedProductResult.Data;
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(1, updatedProduct.StockCurrent);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id!);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetReservedAmount_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            
            var reservedResult = CrudHelpers.GetOneApiResult<GetReservedAmountResult>((id) => IntegrationTestHelpers.ApiClient.Products.GetReservedAmount(id),
                createdProduct.Id.ToString()!, false);
            Console.WriteLine($"ReservedAmount={reservedResult.Data.ReservedAmount}");
            Assert.AreEqual(0M, reservedResult.Data.ReservedAmount);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id!);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_UpdateStockCode_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            Assert.IsTrue(string.IsNullOrEmpty(createdProduct.StockCode));
            
            var updateStockCode = new UpdateStockCode
            {
                Sku = "4711",
                StockCode = "bar"
            };
            var updateResult = IntegrationTestHelpers.ApiClient.Products.UpdateStockCode(updateStockCode);
            Assert.IsNotNull(updateResult);
            Assert.AreEqual((int)ApiResult<object>.ErrorCodeEnum.NoError, (int)updateResult.ErrorCode);
            
            var getResult = CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            Assert.AreEqual("bar", getResult.Data.StockCode);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetProducts_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetProducts(1, int.MaxValue));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetProduct_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            
            CrudHelpers.GetOneApiResult<Product>((id) => IntegrationTestHelpers.ApiClient.Products.GetProduct(id),
                createdProduct.Id.ToString()!);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetCustomFields_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCustomFields(1, int.MaxValue));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetCustomField_IntegrationTest()
        {
            var customField = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetCustomFields(1, int.MaxValue)).Data.FirstOrDefault();
            Assert.IsNotNull(customField);
            Assert.IsNotNull(customField.Id);

            CrudHelpers.GetOneApiResult<ArticleCustomFieldDefinition>((id) => IntegrationTestHelpers.ApiClient.Products.GetCustomField(id),
                customField.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetPatchableProductFields_IntegrationTest()
        {
            var patchableFields = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetPatchableProductFields());
            Console.WriteLine("Patchable Product Fields:");
            foreach (var field in patchableFields.Data)
            {
                Console.WriteLine(field);
            }
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_PatchArticle_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);
            Assert.AreEqual(TestData.Product.Description.First().Text, createdProduct.Description.First().Text);
            
            var fieldsToPatch = new Dictionary<string, string>
            {
                { "Description", "Modified" }
            };
            var patchResult = CrudHelpers.Patch<Product>(
                (id, fields) => IntegrationTestHelpers.ApiClient.Products.PatchArticle(id, fields),
                createdProduct.Id.Value, fieldsToPatch);
            
            Assert.AreEqual("Modified", patchResult.Data.Description.First().Text);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetArticleImages_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_GetArticleImage_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            CrudHelpers.GetOneApiResult<ArticleImage>((id) => IntegrationTestHelpers.ApiClient.Products.GetArticleImage(id),
                resultImage.Data.Id);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_AddArticleImage_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            Assert.AreEqual((byte)1, resultImage.Data.Position);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_UpdateArticleImage_IntegrationTest()
        {
            var createdProduct = _createProduct();
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
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_AddMultipleArticleImages_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
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
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
            Assert.AreEqual(3, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_DeleteArticleImage_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            
            CrudHelpers.DeleteOne<ArticleImage>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteArticleImage(id),
                resultImage.Data.Id);
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
            Assert.AreEqual(0, imagesResult.Data.Count);
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Product_DeleteMultipleArticleImages_IntegrationTest()
        {
            var createdProduct = _createProduct();
            Assert.IsNotNull(createdProduct.Id);

            var articleImage = TestData.GetArticleImage(createdProduct.Id.Value);
            var resultImage1 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            var resultImage2 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            var resultImage3 = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Products.AddArticleImage(x),
                articleImage);
            
            var imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
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
            
            imagesResult = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Products.GetArticleImages(createdProduct.Id.Value));
            Assert.AreEqual(1, imagesResult.Data.Count);
            Assert.IsTrue(imagesResult.Data.Any(x => x.Id == resultImage3.Data.Id));
            
            // cleanup
            CrudHelpers.DeleteOne<Product>((id) => IntegrationTestHelpers.ApiClient.Products.DeleteProduct(id),
                createdProduct.Id.Value);
        }

        private Product _createProduct(string sku = null!)
        {
            var product = TestData.Product;
            if (!string.IsNullOrWhiteSpace(sku))
            {
                product.SKU = sku;
            }
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Products.AddProduct(w),
                product);
            var createdProduct = result.Data;
            Assert.IsNotNull(createdProduct);
            Assert.IsNotNull(createdProduct.Id);

            return createdProduct;
        }
    }
}