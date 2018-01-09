using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Single entry result set for api requests
    /// </summary>
    /// <typeparam name="T">Type of content as part of the result</typeparam>
    public class ApiResult<T>
    {
        public enum ErrorCodeEnum
        {
            NoError = 0,
            ApiNotActivated,
            OrderNotFound,
            InvoiceNotCreated,
            CantCreateInvoice,
            OrderCanceled,
            OrderDeleted,
            OrderNotPaid,
            InternalError,
            OrderExists,
            WrongShopId,
            InvalidData,
            CreateUser_DuplicateEmail,
            CreateUser_InvalidEmail,
            CreateUser_UserRejected,
            CreateUser_TermsNotAccepted,
            NoPaidAccount,
            APIServiceNotBooked,
            CantCreateDeliveryNote
        }

        /// <summary>
        /// Shows, of a request was successful or not.
        /// </summary>
        public bool Success
        {
            get
            {
                return ErrorMessage == null && ErrorCode == ErrorCodeEnum.NoError;
            }
        }

        /// <summary>
        /// If a request failed, a detailed message can be found here.
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// If a request failed, the failure cause can be found here <<see cref="ErrorCodeEnum"/>
        /// </summary>
        public ErrorCodeEnum ErrorCode { get; set;}

        /// <summary>
        /// Response content to a request.
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Multi entry result set for api requests, with paging behaviour
    /// </summary>
    /// <typeparam name="T">Type of content as part of the result typically of type IEnumerable.</typeparam>
    public class ApiPagedResult<T> : ApiResult<T>
    {
        /// <summary>
        /// Information about the paging
        /// </summary>
        public class PagingInformation
        {
            /// <summary>
            /// Currently delivered page
            /// </summary>
            public int Page { get; set; }
            /// <summary>
            /// Total count of pages available with the given <see cref="PageSize"/>
            /// </summary>
            public int TotalPages { get; set; }
            /// <summary>
            /// Total count of available datasets.
            /// </summary>
            public int TotalRows { get; set; }
            /// <summary>
            /// Defines how many entries each page shoul contain
            /// </summary>
            public int PageSize { get; set; }
        }

        /// <summary>
        /// Shows the relation of the delivered content in terms of content paging.
        /// </summary>
        public PagingInformation Paging { get; set; }
    }
}
