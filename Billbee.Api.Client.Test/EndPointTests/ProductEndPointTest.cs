using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Moq;
using RestSharp;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
[TestCategory(TestCategories.EndpointTests)]
public class ProductEndPointTest
{
    [TestMethod]
    public void Product_GetStocks_Test()
    {
        var testStock = new Stock();

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<Stock>>>($"/products/stocks", null);
        var mockResult = TestHelpers.GetApiResult(new List<Stock> { testStock });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetStocks();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_UpdateStockMultiple_Test()
    {
        var testCurrentStockInfo = new CurrentStockInfo();
        var testUpdateStock = new UpdateStock();
        var updateStockList = new List<UpdateStock> { testUpdateStock };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<List<ApiResult<CurrentStockInfo>>>($"/products/updatestockmultiple", updateStockList, null);
        var mockResult = new List<ApiResult<CurrentStockInfo>> { TestHelpers.GetApiResult(testCurrentStockInfo) };
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.UpdateStockMultiple(updateStockList);
            Assert.IsNotNull(result);
        });
    }
    
    [TestMethod]
    public void Product_UpdateStock_Test()
    {
        var testCurrentStockInfo = new CurrentStockInfo();
        var testUpdateStock = new UpdateStock();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<CurrentStockInfo>>($"/products/updatestock", testUpdateStock, null);
        var mockResult = TestHelpers.GetApiResult(testCurrentStockInfo);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.UpdateStock(testUpdateStock);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetReservedAmount_Test()
    {
        var testGetReservedAmountResult = new GetReservedAmountResult();
        
        var id = "4711";
        var lookupBy = "id";
        long? stockId = 35;
        NameValueCollection parameters = new NameValueCollection
        {
            { "id", id },
            { "lookupBy", lookupBy },
            { "stockId", stockId.Value.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<GetReservedAmountResult>>($"/products/reservedamount", parameters);
        var mockResult = TestHelpers.GetApiResult(testGetReservedAmountResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetReservedAmount(id, lookupBy, stockId);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_UpdateStockCode_Test()
    {
        var testUpdateStockCode = new UpdateStockCode();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<object>>("/products/updatestockcode", testUpdateStockCode, null);
        var mockResult = TestHelpers.GetApiResult(new object());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.UpdateStockCode(testUpdateStockCode);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetProducts_Test()
    {
        var testProduct = new Product();
        int page = 1;
        int pageSize = 5;
        DateTime? minCreatedAt = DateTime.Now.AddDays(-1);
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() },
            { "minCreatedAt", minCreatedAt.Value.ToString("yyyy-MM-dd") }
        };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Product>>>($"/products", parameters);
        var mockResult = TestHelpers.GetApiPagedResult(new List<Product>{ testProduct });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetProducts(page, pageSize, minCreatedAt);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetProduct_Test()
    {
        var testProduct = new Product();
        string id = "4711";
        var type = ProductIdType.id;
        NameValueCollection parameters = new NameValueCollection { { "lookupBy", type.ToString() } };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<Product>>($"/products/{id}", parameters);
        var mockResult = TestHelpers.GetApiResult(testProduct);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetProduct(id, type);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }

    [TestMethod]
    public void Product_GetCustomFields_Test()
    {
        var testArticleCustomFieldDefinition = new ArticleCustomFieldDefinition();
        int page = 1;
        int pageSize = 5;
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<ArticleCustomFieldDefinition>>>($"/products/custom-fields", parameters);
        var mockResult = TestHelpers.GetApiPagedResult(new List<ArticleCustomFieldDefinition> { testArticleCustomFieldDefinition });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetCustomFields(page, pageSize);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetCustomField_Test()
    {
        var testArticleCustomFieldDefinition = new ArticleCustomFieldDefinition();
        long id = 4711;

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<ArticleCustomFieldDefinition>>($"/products/custom-fields/{id}", null);
        var mockResult = TestHelpers.GetApiResult(testArticleCustomFieldDefinition);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetCustomField(id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetPatchableProductFields_Test()
    {
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<string>>>($"/products/PatchableFields", null);
        var mockResult = TestHelpers.GetApiResult(new List<string> { "foo", "bar" });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetPatchableProductFields();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_PatchArticle_Test()
    {
        var testProduct = new Product();
        long id = 4711;
        var fieldsToPatch = new Dictionary<string, string> { { "foo", "val" }, { "bar", "val2" } };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Patch<ApiResult<Product>>($"/products/{id}", null, fieldsToPatch);
        var mockResult = TestHelpers.GetApiResult(testProduct);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.PatchArticle(id, fieldsToPatch);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_GetArticleImages_Test()
    {
        var testArticleImage = new ArticleImage();
        long id = 4711;

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<ArticleImage>>>($"/products/{id}/images", null);
        var mockResult = TestHelpers.GetApiResult(new List<ArticleImage> { testArticleImage });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetArticleImages(id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
   
    [TestMethod]
    public void Product_GetArticleImage_Test()
    {
        var testArticleImage = new ArticleImage();
        long articleId = 4711;
        long imageId = 4712;

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<ArticleImage>>($"/products/{articleId}/images/{imageId}", null);
        var mockResult = TestHelpers.GetApiResult(testArticleImage);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetArticleImage(articleId, imageId);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
    
    [TestMethod]
    public void Product_GetArticleImage2_Test()
    {
        var testArticleImage = new ArticleImage();
        long imageId = 4712;

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<ArticleImage>>($"/products/images/{imageId}", null);
        var mockResult = TestHelpers.GetApiResult(testArticleImage);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetArticleImage(imageId);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
    
    [TestMethod]
    public void Product_AddArticleImage_Test()
    {
        var testArticleImage = new ArticleImage { Id = 0, ArticleId = 4712 };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<ArticleImage>>($"/products/{testArticleImage.ArticleId}/images/{testArticleImage.Id}", testArticleImage, null);
        var mockResult = TestHelpers.GetApiResult(testArticleImage);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.AddArticleImage(testArticleImage);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
    
    [TestMethod]
    public void Product_UpdateArticleImage_Test()
    {
        var testArticleImage = new ArticleImage { Id = 4711, ArticleId = 4712 };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<ArticleImage>>($"/products/{testArticleImage.ArticleId}/images/{testArticleImage.Id}", testArticleImage, null);
        var mockResult = TestHelpers.GetApiResult(testArticleImage);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.UpdateArticleImage(testArticleImage);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
    
    [TestMethod]
    public void Product_AddMultipleArticleImages_Test()
    {
        var testArticleImage = new ArticleImage { Id = 4711, ArticleId = 4712 };
        var images = new List<ArticleImage> { testArticleImage };
        bool replace = true;
        NameValueCollection parameters = new NameValueCollection { { "replace", replace.ToString() } };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<List<ArticleImage>>>($"/products/{testArticleImage.ArticleId}/images", images, parameters);
        var mockResult = TestHelpers.GetApiResult(new List<ArticleImage> { testArticleImage });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.AddMultipleArticleImages(testArticleImage.ArticleId, images, replace);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    } 
  
    [TestMethod]
    public void Product_DeleteArticleImage_Test()
    {
        long articleId = 4711;
        long imageId = 4712;

        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new ProductEndPoint(restClientMock.Object);
        uut.DeleteArticleImage(articleId, imageId);
        restClientMock.Verify(x => x.Delete($"/products/{articleId}/images/{imageId}", null, ParameterType.QueryString));
    }

    [TestMethod]
    public void Product_DeleteArticleImage2_Test()
    {
        long imageId = 4712;

        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new ProductEndPoint(restClientMock.Object);
        uut.DeleteArticleImage(imageId);
        restClientMock.Verify(x => x.Delete($"/products/images/{imageId}", null, ParameterType.QueryString));
    } 

    [TestMethod]
    public void Product_DeleteMultipleArticleImages_Test()
    {
        var testDeletedImages = new DeletedImages();
        var imageIds = new List<long> { 4711, 4712 };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<DeletedImages>>($"/products/images/delete", imageIds, null);
        var mockResult = TestHelpers.GetApiResult(testDeletedImages);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.DeleteMultipleArticleImages(imageIds);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }

    [TestMethod]
    public void Product_AddProduct_Test()
    {
        var testProduct = new Product();
    
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<Product>>($"/products", testProduct, null);
        var mockResult = TestHelpers.GetApiResult(testProduct);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.AddProduct(testProduct);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Product_DeleteProduct_Test()
    {
        long productId = 4712;

        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new ProductEndPoint(restClientMock.Object);
        uut.DeleteProduct(productId);
        restClientMock.Verify(x => x.Delete($"/products/{productId}", null, ParameterType.QueryString));
    }
    
    [TestMethod]
    public void Product_GetCategories_Test()
    {
        var testCategoryList = new List<ArticleCategory>
        {
            new() { Name = "cat1", Id = 1 },
            new() { Name = "cat2", Id = 2 }
        };
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<ArticleCategory>>>($"/products/category", null);
        var mockResult = TestHelpers.GetApiResult(testCategoryList);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ProductEndPoint(restClient);
            var result = uut.GetCategories();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
        });
    }
}