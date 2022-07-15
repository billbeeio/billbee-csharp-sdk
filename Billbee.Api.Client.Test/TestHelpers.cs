using System.Linq.Expressions;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test;

public static class TestHelpers
{
    public static void RestClientMockTest<T>(Expression<Func<IBillbeeRestClient, T>> expression, T mockResult, Action<IBillbeeRestClient> actAndAssert)
    {
        // arrange
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(expression)
            .Returns(mockResult);

        // act & assert
        actAndAssert(restClientMock.Object);
        restClientMock.Verify(expression);
        if (mockResult != null)
        {
            Assert.IsNotNull(mockResult);
        }
    }
    
    public static ApiResult<T> GetApiResult<T>(T t, ApiResult<T>.ErrorCodeEnum errorCode = ApiResult<T>.ErrorCodeEnum.NoError, string? errorMessage = null)
    {
        return new ApiResult<T>
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage,
            Data = t
        };
    }

    public static ApiPagedResult<List<T>> GetApiPagedResult<T>(List<T> ts, int page = 1, int pageSize = 5, int totalPages = 10, ApiResult<List<T>>.ErrorCodeEnum errorCode = ApiResult<List<T>>.ErrorCodeEnum.NoError, string? errorMessage = null)
    {
        return new ApiPagedResult<List<T>>
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage,
            Data = ts,
            Paging = new ApiPagedResult<List<T>>.PagingInformation
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRows = pageSize * totalPages
            }
        };
    }
}