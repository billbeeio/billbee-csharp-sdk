using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace Billbee.Api.Client
{
    public class FileParam
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
    
    public interface IBillbeeRestClient
    {
        // get
        HttpStatusCode Get(string resource);
        T Get<T>(
            string resource,
            NameValueCollection parameter = null) where T : new();

        // put
        string Put(string resource, NameValueCollection parameter = null);
        string Put(string resource, dynamic data, NameValueCollection parameter = null);
        T Put<T>(string resource, NameValueCollection parameter = null) where T : new();
        T Put<T>(string resource, dynamic data, NameValueCollection parameter = null) where T : new();
        Task<string> PutAsync(string resource, NameValueCollection parameter = null);
        Task<string> PutAsync(string resource, dynamic data);
        Task<T> PutAsync<T>(string resource, dynamic data) where T : new();
        
        // patch
        string Patch(string resource, NameValueCollection parameter = null, dynamic data = null);
        T Patch<T>(string resource, NameValueCollection parameter = null, dynamic data = null) where T : new();

        // post
        Task<string> PostAsync(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString,
            string acceptHeaderValue = "application/json");
        Task<string> PostAsync(string resource, dynamic data);
        Task<T> PostAsync<T>(
            string resource,
            dynamic data,
            NameValueCollection parameter = null,
            ParameterType paramType = ParameterType.QueryString) where T : new();
        T Post<T>(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString);
        string Post(string resource, dynamic data);
        T Post<T>(string resource, dynamic data, NameValueCollection parameters = null) where T : new();

        // delete
        void Delete(
            string resource,
            NameValueCollection parameter = null,
            ParameterType paramType = ParameterType.QueryString);
    }
}