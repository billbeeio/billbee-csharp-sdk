using System;
using System.Net.Http;

namespace Billbee.Api.Client
{
    public enum HttpOperation
    {
        Get = 0,
        Post,
        Put,
        Patch,
        Delete,
        Head,
        Options,
        Trace
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiMappingAttribute : Attribute
    {
        public ApiMappingAttribute(string apiPath, HttpOperation httpOperation)
        {
            ApiPath = apiPath;
            HttpOperation = httpOperation;
        }
        
        public string ApiPath { get; set; }
        public HttpOperation HttpOperation { get; set; }
    }
}