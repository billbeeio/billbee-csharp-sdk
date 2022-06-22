using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Interfaces.Endpoint;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Interfaces.Endpoint.IProductEndPoint" />
    public class ProductEndPoint : RestClientBaseClass, IProductEndPoint
    {
        internal ProductEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <inheritdoc />
        public ApiResult<List<Stock>> GetStocks()
        {
            return requestResource<ApiResult<List<Stock>>>("/products/stocks");
        }

        /// <inheritdoc />
        public List<ApiResult<CurrentStockInfo>> UpdateStockMultiple(List<UpdateStock> updateStockList)
        {
            return post<List<ApiResult<CurrentStockInfo>>>("/products/updatestockmultiple", updateStockList);
        }

        /// <inheritdoc />
        public ApiResult<CurrentStockInfo> UpdateStock(UpdateStock updateStockModel)
        {
            return post<ApiResult<CurrentStockInfo>>("/products/updatestock", updateStockModel);
        }

        /// <inheritdoc />
        public ApiResult<GetReservedAmountResult> GetReservedAmount(string id, string lookupBy = "id",
            long? stockId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", id);
            parameters.Add("lookupBy", lookupBy);
            if (stockId != null)
            {
                parameters.Add("stockId", stockId.Value.ToString());
            }

            return requestResource<ApiResult<GetReservedAmountResult>>($"/products/reservedamount", parameters);
        }

        /// <inheritdoc />
        public ApiResult<object> UpdateStockCode(UpdateStockCode updateStockCodeModel)
        {
            return post<ApiResult<object>>("/products/updatestockcode", updateStockCodeModel);
        }

        /// <inheritdoc />
        public ApiPagedResult<List<Product>> GetProducts(int page, int pageSize, DateTime? minCreatedAt = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            if (minCreatedAt != null)
            {
                parameters.Add("minCreatedAt", minCreatedAt.Value.ToString("yyyy-MM-dd"));
            }

            return requestResource<ApiPagedResult<List<Product>>>($"/products", parameters);
        }

        /// <inheritdoc />
        public ApiResult<Product> GetProduct(string id, ProductIdType type = ProductIdType.id)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("lookupBy", type.ToString());

            return requestResource<ApiResult<Product>>($"/products/{id}", parameters);
        }

        /// <inheritdoc />
        public ApiPagedResult<List<ArticleCustomFieldDefinition>> GetCustomFields(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiPagedResult<List<ArticleCustomFieldDefinition>>>($"/products/custom-fields",
                parameters);
        }

        /// <inheritdoc />
        public ApiResult<ArticleCustomFieldDefinition> GetCustomField(long id)
        {
            return requestResource<ApiResult<ArticleCustomFieldDefinition>>($"/products/custom-fields/{id}");
        }

        /// <inheritdoc />
        public ApiResult<List<string>> GetPatchableProductFields()
        {
            return requestResource<ApiResult<List<string>>>($"/products/PatchableFields");
        }

        /// <inheritdoc />
        public ApiResult<Product> PatchArticle(long id, Dictionary<string, string> fieldsToPatch)
        {
            return patch<ApiResult<Product>>($"/products/{id}", data: fieldsToPatch);
        }

        /// <inheritdoc />
        public ApiResult<List<ArticleImage>> GetArticleImages(long id)
        {
            return requestResource<ApiResult<List<ArticleImage>>>($"/products/{id}/images");
        }

        /// <inheritdoc />
        public ApiResult<ArticleImage> GetArticleImage(long articleId, long imageId)
        {
            return requestResource<ApiResult<ArticleImage>>($"/products/{articleId}/images/{imageId}");
        }

        /// <inheritdoc />
        public ApiResult<ArticleImage> GetArticleImage(long imageId)
        {
            return requestResource<ApiResult<ArticleImage>>($"/products/images/{imageId}");
        }

        /// <inheritdoc />
        public ApiResult<ArticleImage> AddArticleImage(ArticleImage image)
        {
            if (image.Id != 0)
            {
                throw new InvalidValueException("To add a new image, only 0 as Id is allowed.");
            }

            return put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        /// <inheritdoc />
        public ApiResult<ArticleImage> UpdateArticleImage(ArticleImage image)
        {
            if (image.Id == 0)
            {
                throw new InvalidValueException("To update an image, the Id must not be 0.");
            }

            return put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        /// <inheritdoc />
        public ApiResult<List<ArticleImage>> AddMultipleArticleImages(long articleId, List<ArticleImage> images,
            bool replace = false)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("replace", replace.ToString());
            return put<ApiResult<List<ArticleImage>>>($"/products/{articleId}/images", images, parameters);
        }

        /// <inheritdoc />
        public void DeleteArticleImage(long articleId, long imageId)
        {
            delete($"/products/{articleId}/images/{imageId}");
        }

        /// <inheritdoc />
        public void DeleteArticleImage(long imageId)
        {
            delete($"/products/images/{imageId}");
        }

        /// <inheritdoc />
        public ApiResult<DeletedImages> DeleteMultipleArticleImages(List<long> imageIds)
        {
            return post<ApiResult<DeletedImages>>($"/products/images/delete", imageIds);
        }
    }
}