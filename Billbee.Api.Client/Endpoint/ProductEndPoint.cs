using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// EndPoint to access all product relevant methods.
    /// </summary>
    public class ProductEndPoint : RestClientBaseClass
    {
        internal ProductEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        /// Update the sotck amount for multiple articles
        /// </summary>
        /// <param name="updateStockList">List of UpdateStockRequest</param>
        /// <returns>Result of the operation</returns>
        public List<ApiResult<CurrentStockInfo>> UpdateStockMultiple(List<UpdateStock> updateStockList)
        {
            return post<List<ApiResult<CurrentStockInfo>>>("/products/updatestockmultiple", updateStockList);
        }

        /// <summary>
        /// Update the stock of a single product
        /// </summary>
        /// <param name="updateStockModel">Detail, which article should get which stock</param>
        /// <returns>Result of the operation</returns>
        public ApiResult<CurrentStockInfo> UpdateStock(UpdateStock updateStockModel)
        {
            return post<ApiResult<CurrentStockInfo>>("/products/updatestock", updateStockModel);
        }

        /// <summary>
        /// Queries the reserved amount for a single article by id or by sku
        /// </summary>
        /// <param name="id">The id or the sku of the article to query</param>
        /// <param name="lookupBy">Either the value id or the value sku to specify the meaning of the id parameter</param>
        /// <param name="stockId">Optional the stock id if the multi stock feature is enabled</param>
        /// <returns></returns>
        public ApiResult<GetReservedAmountResult> GetReservedAmount(string id, string lookupBy = "id", int? stockId = null)
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


        /// <summary>
        /// Updates the stock code / stock location of the article
        /// </summary>
        /// <param name="updateStockCodeModel">Details, which article should be changed to which location.</param>
        /// <returns></returns>
        public ApiResult<object> UpdateStockCode(UpdateStockCode updateStockCodeModel)
        {
            return post<ApiResult<object>>("/products/updatestockcode", updateStockCodeModel);
        }

        /// <summary>
        /// Get a list of all products
        /// </summary>
        /// <param name="page">The requested page</param>
        /// <param name="pageSize">The amount of entries per page</param>
        /// <param name="minCreatedAt">When given, only articles, that are newer than the given date are supplied.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets details of a single product
        /// </summary>
        /// <param name="id">The id of the article to deliver.</param>
        /// <param name="type">The type if the id. Whether it is the internal id or the sku.</param>
        /// <returns></returns>
        public ApiResult<Product> GetProduct(string id, ProductIdType type = ProductIdType.id)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("lookupBy", type.ToString());

            return requestResource<ApiResult<Product>>($"/products/{id}", parameters);
        }

        /// <summary>
        /// Requests a list of all custom fields, usable in products
        /// </summary>
        /// <returns>List of CustomFields</returns>
        public ApiPagedResult<List<ArticleCustomFieldDefinition>> GetCustomFields(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiPagedResult<List<ArticleCustomFieldDefinition>>>($"/products/custom-fields", parameters);
        }

        /// <summary>
        /// Requests the definition of a custom field by using it's id
        /// </summary>
        /// <param name="id">Id of the definition to get information about</param>
        /// <returns>The definition of the entry with the given id</returns>
        public ApiResult<ArticleCustomFieldDefinition> GetCustomField(int id)
        {
            return requestResource<ApiResult<ArticleCustomFieldDefinition>>($"/products/custom-fields/{id}");
        }

        /// <summary>
        /// Supplies a list of all fields, that can be patched using the PatchArticle- method
        /// </summary>
        /// <returns>List of fieldnames</returns>
        public ApiResult<List<string>> GetPatchableProductFields()
        {
            return requestResource<ApiResult<List<string>>>($"/products/PatchableFields");
        }

        /// <summary>
        /// Patches given fields of an product
        /// </summary>
        /// <param name="id">Id of the product to patch</param>
        /// <param name="fieldsToPatch">Dictionary which uses the fieldname as key and the new value as value.</param>
        /// <returns></returns>
        public ApiResult<Product> PatchArticle(int id, Dictionary<string, string> fieldsToPatch)
        {
            return patch<ApiResult<Product>>($"/products/{id}", data: fieldsToPatch);
        }

        /// <summary>
        /// Collects all images of a specific article
        /// </summary>
        /// <param name="id">Id of the article to get the images.</param>
        /// <returns>List if Images.</returns>
        public ApiResult<List<ArticleImage>> GetArticleImages(int id)
        {
            return requestResource<ApiResult<List<ArticleImage>>>($"/products/{id}/images");
        }

        /// <summary>
        /// Gets a specific image object
        /// </summary>
        /// <param name="imageId">Id of the image to gather</param>
        /// <returns>The image object.</returns>
        /// <param name="articleId">If of the article, this image belongs to.</param>
        public ApiResult<ArticleImage> GetArticleImage(int articleId, int imageId)
        {
            return requestResource<ApiResult<ArticleImage>>($"/products/{articleId}/images/{imageId}");
        }

        /// <summary>
        /// Gets a specific image object
        /// </summary>
        /// <param name="imageId">Id of the image to gather</param>
        /// <returns>The image object.</returns>
        public ApiResult<ArticleImage> GetArticleImage(int imageId)
        {
            return requestResource<ApiResult<ArticleImage>>($"/products/images/{imageId}");
        }

        /// <summary>
        /// Creates a new image
        /// </summary>
        /// <param name="image">The image defintion to add</param>
        /// <returns>The added image object.</returns>
        public ApiResult<ArticleImage> AddArticleImage(ArticleImage image)
        {
            if (image.Id != 0)
            {
                throw new InvalidValueException("To add a new image, only 0 as Id is allowed.");
            }

            return put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        /// <summary>
        /// Updates an existing image
        /// </summary>
        /// <param name="image">Definition of the image to update.</param>
        /// <returns>The updated image object.</returns>
        public ApiResult<ArticleImage> UpdateArticleImage(ArticleImage image)
        {
            if (image.Id == 0)
            {
                throw new InvalidValueException("To update an image, the Id must not be 0.");
            }

            return put<ApiResult<ArticleImage>>($"/products/{image.ArticleId}/images/{image.Id}", image);
        }

        /// <summary>
        /// Adds muiltiple images to a specific article id.
        /// </summary>
        /// <param name="articleId">Id of the article to attach the images to.</param>
        /// <param name="images">List of images</param>
        /// <param name="replace">If true, existing images will be overwritten.</param>
        /// <returns></returns>
        public ApiResult<List<ArticleImage>> AddMultipleArticleImages(int articleId, List<ArticleImage> images, bool replace = false)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("replace", replace.ToString());
            return put<ApiResult<List<ArticleImage>>>($"/products/{articleId}/images", images, parameters);
        }

        /// <summary>
        /// Deletes a single image of a specified article
        /// </summary>
        /// <param name="articleId">Id of article</param>
        /// <param name="imageId">Id of the image to delete</param>
        public void DeleteArticleImage(int articleId, int imageId)
        {
            delete($"/products/{articleId}/images/{imageId}");
        }

        /// <summary>
        /// Deletes a single image
        /// </summary>
        /// <param name="imageId">id of the image</param>
        public void DeleteArticleImage(int imageId)
        {
            delete($"/products/images/{imageId}");
        }

        /// <summary>
        /// Deletes multiple images
        /// </summary>
        /// <param name="imageIds">List of image ids to delete</param>
        /// <returns>Result of deletion</returns>
        public ApiResult<DeletedImages> DeleteMultipleArticleImages(List<int> imageIds)
        {
            return post<ApiResult<DeletedImages>>($"/products/images/delete", imageIds);
        }

    }
}
