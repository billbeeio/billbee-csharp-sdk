using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IProductEndPoint" />
    public class ProductEndPoint : IProductEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal ProductEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/products/stocks", HttpOperation.Get)]
        public ApiResult<List<Stock>> GetStocks()
        {
            return _restClient.Get<ApiResult<List<Stock>>>("/products/stocks");
        }

        [ApiMapping("/api/v1/products/updatestockmultiple", HttpOperation.Post)]
        public List<ApiResult<CurrentStockInfo>> UpdateStockMultiple(List<UpdateStock> updateStockList)
        {
            return _restClient.Post<List<ApiResult<CurrentStockInfo>>>("/products/updatestockmultiple", updateStockList);
        }

        [ApiMapping("/api/v1/products/updatestock", HttpOperation.Post)]
        public ApiResult<CurrentStockInfo> UpdateStock(UpdateStock updateStockModel)
        {
            return _restClient.Post<ApiResult<CurrentStockInfo>>("/products/updatestock", updateStockModel);
        }

        [ApiMapping("/api/v1/products/reservedamount", HttpOperation.Get)]
        public ApiResult<GetReservedAmountResult> GetReservedAmount(string id, string lookupBy = "id", long? stockId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", id);
            parameters.Add("lookupBy", lookupBy);
            if (stockId != null)
            {
                parameters.Add("stockId", stockId.Value.ToString());
            }

            return _restClient.Get<ApiResult<GetReservedAmountResult>>($"/products/reservedamount", parameters);
        }

        [ApiMapping("/api/v1/products/updatestockcode", HttpOperation.Post)]
        public ApiResult<object> UpdateStockCode(UpdateStockCode updateStockCodeModel)
        {
            return _restClient.Post<ApiResult<object>>("/products/updatestockcode", updateStockCodeModel);
        }

        [ApiMapping("/api/v1/products", HttpOperation.Get)]
        public ApiPagedResult<List<Product>> GetProducts(int page, int pageSize, DateTime? minCreatedAt = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            if (minCreatedAt != null)
            {
                parameters.Add("minCreatedAt", minCreatedAt.Value.ToString("yyyy-MM-dd"));
            }

            return _restClient.Get<ApiPagedResult<List<Product>>>($"/products", parameters);
        }

        [ApiMapping("/api/v1/products/{id}", HttpOperation.Get)]
        public ApiResult<Product> GetProduct(string id, ProductIdType type = ProductIdType.id)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("lookupBy", type.ToString());

            return _restClient.Get<ApiResult<Product>>($"/products/{id}", parameters);
        }
        
        [ApiMapping("/api/v1/products", HttpOperation.Post)]
        public ApiResult<Product> AddProduct(Product product)
        {
            return _restClient.Post<ApiResult<Product>>($"/products", product);
        }
        
        [ApiMapping("/api/v1/products/{id}", HttpOperation.Delete)]
        public void DeleteProduct(long id)
        {
            _restClient.Delete($"/products/{id}");
        }
        
        [ApiMapping("/api/v1/products/category", HttpOperation.Get)]
        public ApiResult<List<ArticleCategory>> GetCategories()
        {
            return _restClient.Get<ApiResult<List<ArticleCategory>>>($"/products/category");
        }

        [ApiMapping("/api/v1/products/custom-fields", HttpOperation.Get)]
        public ApiPagedResult<List<ArticleCustomFieldDefinition>> GetCustomFields(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return _restClient.Get<ApiPagedResult<List<ArticleCustomFieldDefinition>>>($"/products/custom-fields", parameters);
        }

        [ApiMapping("/api/v1/products/custom-fields/{id}", HttpOperation.Get)]
        public ApiResult<ArticleCustomFieldDefinition> GetCustomField(long id)
        {
            return _restClient.Get<ApiResult<ArticleCustomFieldDefinition>>($"/products/custom-fields/{id}");
        }

        [ApiMapping("/api/v1/products/PatchableFields", HttpOperation.Get)]
        public ApiResult<List<string>> GetPatchableProductFields()
        {
            return _restClient.Get<ApiResult<List<string>>>($"/products/PatchableFields");
        }
        
        [ApiMapping("/api/v1/products/{id}", HttpOperation.Patch)]
        public ApiResult<Product> PatchArticle(long id, Dictionary<string, string> fieldsToPatch)
        {
            return _restClient.Patch<ApiResult<Product>>($"/products/{id}", data: fieldsToPatch);
        }

        [ApiMapping("/api/v1/products/{productId}/images", HttpOperation.Get)]
        public ApiResult<List<ArticleImage>> GetArticleImages(long id)
        {
            return _restClient.Get<ApiResult<List<ArticleImage>>>($"/products/{id}/images");
        }

        [ApiMapping("/api/v1/products/{productId}/images/{imageId}", HttpOperation.Get)]
        public ApiResult<ArticleImage> GetArticleImage(long articleId, long imageId)
        {
            return _restClient.Get<ApiResult<ArticleImage>>($"/products/{articleId}/images/{imageId}");
        }

        [ApiMapping("/api/v1/products/images/{imageId}", HttpOperation.Get)]
        public ApiResult<ArticleImage> GetArticleImage(long imageId)
        {
            return _restClient.Get<ApiResult<ArticleImage>>($"/products/images/{imageId}");
        }

        [ApiMapping("/api/v1/products/{productId}/images/{imageId}", HttpOperation.Put)]
        public ApiResult<ArticleImage> AddArticleImage(ArticleImage image)
        {
            if (image.Id != 0)
            {
                throw new InvalidValueException("To add a new image, only 0 as Id is allowed.");
            }

            return _restClient.Put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        [ApiMapping("/api/v1/products/{productId}/images/{imageId}", HttpOperation.Put)]
        public ApiResult<ArticleImage> UpdateArticleImage(ArticleImage image)
        {
            if (image.Id == 0)
            {
                throw new InvalidValueException("To update an image, the Id must not be 0.");
            }

            return _restClient.Put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        [ApiMapping("/api/v1/products/{productId}/images", HttpOperation.Put)]
        public ApiResult<List<ArticleImage>> AddMultipleArticleImages(long articleId, List<ArticleImage> images, bool replace = false)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("replace", replace.ToString());
            return _restClient.Put<ApiResult<List<ArticleImage>>>($"/products/{articleId}/images", images, parameters);
        }

        [ApiMapping("/api/v1/products/{productId}/images/{imageId}", HttpOperation.Delete)]
        public void DeleteArticleImage(long articleId, long imageId)
        {
            _restClient.Delete($"/products/{articleId}/images/{imageId}");
        }

        [ApiMapping("/api/v1/products/images/{imageId}", HttpOperation.Delete)]
        public void DeleteArticleImage(long imageId)
        {
            _restClient.Delete($"/products/images/{imageId}");
        }

        [ApiMapping("/api/v1/products/images/delete", HttpOperation.Post)]
        public ApiResult<DeletedImages> DeleteMultipleArticleImages(List<long> imageIds)
        {
            return _restClient.Post<ApiResult<DeletedImages>>($"/products/images/delete", imageIds);
        }

    }
}
