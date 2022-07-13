using System;
using System.Collections.Generic;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// EndPoint to access all product relevant methods.
    /// </summary>
    public interface IProductEndPoint
    {
        /// <summary>
        /// Query all defined stock locations
        /// </summary>
        /// <returns>Result of the operation</returns>
        ApiResult<List<Stock>> GetStocks();

        /// <summary>
        /// Update the sotck amount for multiple articles
        /// </summary>
        /// <param name="updateStockList">List of UpdateStockRequest</param>
        /// <returns>Result of the operation</returns>
        List<ApiResult<CurrentStockInfo>> UpdateStockMultiple(List<UpdateStock> updateStockList);

        /// <summary>
        /// Update the stock of a single product
        /// </summary>
        /// <param name="updateStockModel">Detail, which article should get which stock</param>
        /// <returns>Result of the operation</returns>
        ApiResult<CurrentStockInfo> UpdateStock(UpdateStock updateStockModel);

        /// <summary>
        /// Queries the reserved amount for a single article by id or by sku
        /// </summary>
        /// <param name="id">The id or the sku of the article to query</param>
        /// <param name="lookupBy">Either the value id or the value sku to specify the meaning of the id parameter</param>
        /// <param name="stockId">Optional the stock id if the multi stock feature is enabled</param>
        /// <returns></returns>
        ApiResult<GetReservedAmountResult> GetReservedAmount(string id, string lookupBy = "id", long? stockId = null);

        /// <summary>
        /// Updates the stock code / stock location of the article
        /// </summary>
        /// <param name="updateStockCodeModel">Details, which article should be changed to which location.</param>
        /// <returns></returns>
        ApiResult<object> UpdateStockCode(UpdateStockCode updateStockCodeModel);

        /// <summary>
        /// Get a list of all products
        /// </summary>
        /// <param name="page">The requested page</param>
        /// <param name="pageSize">The amount of entries per page</param>
        /// <param name="minCreatedAt">When given, only articles, that are newer than the given date are supplied.</param>
        /// <returns></returns>
        ApiPagedResult<List<Product>> GetProducts(int page, int pageSize, DateTime? minCreatedAt = null);

        /// <summary>
        /// Gets details of a single product
        /// </summary>
        /// <param name="id">The id of the article to deliver.</param>
        /// <param name="type">The type if the id. Whether it is the internal id or the sku.</param>
        /// <returns></returns>
        ApiResult<Product> GetProduct(string id, ProductIdType type = ProductIdType.id);

        /// <summary>
        /// Requests a list of all custom fields, usable in products
        /// </summary>
        /// <returns>List of CustomFields</returns>
        ApiPagedResult<List<ArticleCustomFieldDefinition>> GetCustomFields(int page, int pageSize);

        /// <summary>
        /// Requests the definition of a custom field by using it's id
        /// </summary>
        /// <param name="id">Id of the definition to get information about</param>
        /// <returns>The definition of the entry with the given id</returns>
        ApiResult<ArticleCustomFieldDefinition> GetCustomField(long id);

        /// <summary>
        /// Supplies a list of all fields, that can be patched using the PatchArticle- method
        /// </summary>
        /// <returns>List of field names</returns>
        ApiResult<List<string>> GetPatchableProductFields();

        /// <summary>
        /// Patches given fields of an product
        /// </summary>
        /// <param name="id">Id of the product to patch</param>
        /// <param name="fieldsToPatch">Dictionary which uses the field name as key and the new value as value.</param>
        /// <returns></returns>
        ApiResult<Product> PatchArticle(long id, Dictionary<string, string> fieldsToPatch);

        /// <summary>
        /// Collects all images of a specific article
        /// </summary>
        /// <param name="id">Id of the article to get the images.</param>
        /// <returns>List if Images.</returns>
        ApiResult<List<ArticleImage>> GetArticleImages(long id);

        /// <summary>
        /// Gets a specific image object
        /// </summary>
        /// <param name="imageId">Id of the image to gather</param>
        /// <returns>The image object.</returns>
        /// <param name="articleId">If of the article, this image belongs to.</param>
        ApiResult<ArticleImage> GetArticleImage(long articleId, long imageId);

        /// <summary>
        /// Gets a specific image object
        /// </summary>
        /// <param name="imageId">Id of the image to gather</param>
        /// <returns>The image object.</returns>
        ApiResult<ArticleImage> GetArticleImage(long imageId);

        /// <summary>
        /// Creates a new image
        /// </summary>
        /// <param name="image">The image definition to add</param>
        /// <returns>The added image object.</returns>
        ApiResult<ArticleImage> AddArticleImage(ArticleImage image);

        /// <summary>
        /// Updates an existing image
        /// </summary>
        /// <param name="image">Definition of the image to update.</param>
        /// <returns>The updated image object.</returns>
        ApiResult<ArticleImage> UpdateArticleImage(ArticleImage image);

        /// <summary>
        /// Adds muiltiple images to a specific article id.
        /// </summary>
        /// <param name="articleId">Id of the article to attach the images to.</param>
        /// <param name="images">List of images</param>
        /// <param name="replace">If true, existing images will be overwritten.</param>
        /// <returns></returns>
        ApiResult<List<ArticleImage>> AddMultipleArticleImages(long articleId, List<ArticleImage> images, bool replace = false);

        /// <summary>
        /// Deletes a single image of a specified article
        /// </summary>
        /// <param name="articleId">Id of article</param>
        /// <param name="imageId">Id of the image to delete</param>
        void DeleteArticleImage(long articleId, long imageId);

        /// <summary>
        /// Deletes a single image
        /// </summary>
        /// <param name="imageId">id of the image</param>
        void DeleteArticleImage(long imageId);

        /// <summary>
        /// Deletes multiple images
        /// </summary>
        /// <param name="imageIds">List of image ids to delete</param>
        /// <returns>Result of deletion</returns>
        ApiResult<DeletedImages> DeleteMultipleArticleImages(List<long> imageIds);
    }
}