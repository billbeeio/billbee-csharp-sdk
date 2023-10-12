using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Billbee.Api.Client.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Billbee.Api.Client
{
    internal class BillbeeRestClient : IBillbeeRestClient
    {
        private readonly ApiConfiguration _config;
        private readonly bool _allowRead;
        private readonly bool _allowWrite;
        private readonly ILogger _logger;
        private readonly DataFormat _requestFormat;
        private readonly Dictionary<string, string> _additionalHeaders;

        public BillbeeRestClient(ILogger logger, ApiConfiguration config, bool allowRead = true, bool allowWrite = true)
        {
            _logger = logger;
            _config = config;
            _allowRead = allowRead;
            _allowWrite = allowRead && allowWrite;
            _requestFormat = DataFormat.Json;
            _additionalHeaders = null;
        }
        
        #region get
        
        public HttpStatusCode Get(string resource)
        {
            _checkReadAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            return c.Get(req).StatusCode;
        }

        public T Get<T>(
            string resource,
            NameValueCollection parameter = null) where T : new()
        {
            _checkReadAccess();
            
            return _requestResourceInternal<T>(resource, parameter);
        }
        
        private T _requestResourceInternal<T>(
            string resource,
            NameValueCollection parameter = null,
#pragma warning disable 618
            Action<IList<HeaderParameter>> headerProcessor = null,
#pragma warning restore 618
            int sleepTimeMs = 1000, Action<RestResponse<T>> preDeserializeHook = null,
            NameValueCollection headerParameter = null) where T : new()
        {
            if (!_allowRead)
            {
                return default;
            }
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);

            if (headerParameter != null)
            {
                foreach (var param in headerParameter.AllKeys)
                {
                    req.AddHeader(param, headerParameter[param]);
                }
            }

            var pStr = parameter == null ? "" : string.Join(",", parameter.AllKeys.Select(k => $"{k}={parameter[k]}"));

            _log($"Requesting {resource} with params {pStr}", LogSeverity.Info);

            RestResponse<T> response = _retry<>(resource, () => c.Get<T>(req), sleepTimeMs);
            headerProcessor?.Invoke(response.Headers?.ToArray());
            preDeserializeHook?.Invoke(response);
            var data = response.Content != null ? JsonConvert.DeserializeObject<T>(response.Content) : default;
            return data;
        }
        
        #endregion
        
        #region put

        public string Put(string resource, NameValueCollection parameter = null)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public string Put(string resource, dynamic data, NameValueCollection parameter = null)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                RestRequestExtensions.AddBody(req, data);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Put<T>(string resource, NameValueCollection parameter = null) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content != null ? JsonConvert.DeserializeObject<T>(response.Content) : default;
        }

        public T Put<T>(string resource, dynamic data, NameValueCollection parameter = null) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                RestRequestExtensions.AddBody(req, data);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content != null ? JsonConvert.DeserializeObject<T>(response.Content) : default;
        }
        public async Task<string> PutAsync(string resource, NameValueCollection parameter = null)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            req.Method = Method.Put;
            var response = await c.ExecuteAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public async Task<string> PutAsync(string resource, dynamic data)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.Method = Method.Put;
            RestRequestExtensions.AddBody(req, data);
            var response = await c.ExecuteAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public async Task<T> PutAsync<T>(string resource, dynamic data) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.Method = Method.Put;
            RestRequestExtensions.AddBody(req, data);
            var response = await c.ExecuteAsync<T>(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Data;
        }
        
        #endregion

        #region patch
        
        public string Patch(string resource, NameValueCollection parameter = null, dynamic data = null)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
            {
                RestRequestExtensions.AddBody(req, data);
            }

            var response = c.Patch(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Patch<T>(string resource, NameValueCollection parameter = null, dynamic data = null) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);

            if (data != null)
            {
                RestRequestExtensions.AddBody(req, data);
            }

            var response = c.Patch(req);

            _throwWhenErrResponse(response, resource);
            return response.Content != null ? JsonConvert.DeserializeObject<T>(response.Content) : default;
        }
        
        #endregion

        #region post

        private string Post(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter, paramType);

            if (files != null)
            {
                foreach (var f in files)
                {
                    req.AddFile(f.Name, f.Data, f.FileName, f.ContentType);
                }
            }

            var response = c.Post(req);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Post<T>(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString)
        {
            _checkWriteAccess();
            
            var resStr = Post(resource, parameter, files, paramType);
            return JsonConvert.DeserializeObject<T>(resStr);
        }
        
        public string Post(string resource, dynamic data)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            RestRequestExtensions.AddBody(req, data);
            var response = c.Post(req);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Post<T>(string resource, dynamic data, NameValueCollection parameters = null) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = parameters != null
                ? _createRestRequest(resource, parameters)
                : _createRestRequest(resource, null);
            RestRequestExtensions.AddBody(req, data);
            var response = c.Post(req);
            _throwWhenErrResponse(response, resource);

            return response.Content != null ? JsonConvert.DeserializeObject<T>(response.Content) : default;
        }

        public async Task<string> PostAsync(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString,
            string acceptHeaderValue = "application/json")
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter, paramType, acceptHeaderValue);

            if (files != null)
            {
                foreach (var f in files)
                {
                    req.AddFile(f.Name, f.Data, f.FileName, f.ContentType);
                }
            }

            var response = await c.ExecutePostAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }
        
        public async Task<string> PostAsync(string resource, dynamic data)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            RestRequestExtensions.AddBody(req, data);
            var response = await c.ExecutePostAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public async Task<T> PostAsync<T>(
            string resource,
            dynamic data,
            NameValueCollection parameter = null,
            ParameterType paramType = ParameterType.QueryString) where T : new()
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                RestRequestExtensions.AddBody(req, data);
            var response = await c.ExecutePostAsync<T>(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Data;
        }

        #endregion

        #region delete
        
        public void Delete(
            string resource,
            NameValueCollection parameter = null,
            ParameterType paramType = ParameterType.QueryString)
        {
            _checkWriteAccess();
            
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter, paramType);
            var response = c.Delete(req);
            _throwWhenErrResponse(response, resource);
        }
        
        #endregion

        #region private methods

        private void _checkReadAccess()
        {
            if (!_allowRead)
            {
                throw new UnauthorizedAccessException(
                    "RestClient was configured without required read permission");
            }
        }
        
        private void _checkWriteAccess()
        {
            if (!_allowWrite)
            {
                throw new UnauthorizedAccessException(
                    "RestClient was configured without required write permission");
            }
        }
        
        /// <summary>
        /// Retries a network request up to 4 times when throttled
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <param name="action"></param>
        /// <param name="sleepTimeMs"></param>
        /// <returns></returns>
        private RestResponse<T> _retry<T>(string resource, Func<RestResponse<T>> action, int sleepTimeMs)
        {
            RestResponse<T> response = null;
            for (byte retryCounter = 5; retryCounter > 0; retryCounter--)
            {
                response = action();

                if ((int) response.StatusCode == 429)
                {
                    // Throttling
                    Thread.Sleep(sleepTimeMs);
                }
                else
                {
                    _throwWhenErrResponse(response, resource);
                    return response;
                }
            }

            _throwWhenErrResponse(response, resource);
            throw new Exception("Request is throttled");
        }

        private RestRequest _createRestRequest(
            string resource,
            NameValueCollection parameter,
            ParameterType paramType = ParameterType.QueryString,
            string acceptHeaderValue = "application/json")
        {
            var restRequest = new RestRequest(resource);
            restRequest.AddHeader("X-Billbee-Api-Key", _config.ApiKey);
            restRequest.AddHeader("Accept", acceptHeaderValue);
            restRequest.RequestFormat = _requestFormat;

            if (parameter == null || parameter.Count <= 0)
            {
                return restRequest;
            }

            foreach (string key in parameter)
            {
                restRequest.AddParameter(key, parameter[key], paramType);
            }

            return restRequest;
        }

        private RestClient _createRestClient()
        {
            var restClientOptions = new RestClientOptions
            {
                Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password),
                UserAgent = $"BillbeeApiClientDotNet/{typeof(ApiClient).Assembly.GetName().Version}",
                BaseUrl = new Uri(_config.BaseUrl)
            };

            var rc = new RestClient(restClientOptions);

            if (_additionalHeaders == null)
            {
                return rc;
            }

            foreach (var h in _additionalHeaders)
            {
                rc.AddDefaultHeader(h.Key, h.Value);
            }

            return rc;
        }

        private static string _parseError(RestResponseBase response)
        {
            // Default handling. Works for inventorum, Debitoor and some other
            string errMsg = null;
            try
            {
                var errResponse = response.Content == default ? default : JsonConvert.DeserializeObject<JObject>(response.Content);
                errMsg = "HTTP Response: " + response.StatusCode + ": " + errResponse?["ErrorCode"]?.Value<string>() +
                         " - " + errResponse?["ErrorMessage"]?.Value<string>() + errResponse?["Message"]?.Value<string>();

            }
            catch
            {
                // left intentionally blank
            }

            errMsg = errMsg ?? ($"Anfrage fehlgeschlagen: {response.StatusCode} {response.StatusDescription}");

            return errMsg;
        }

        private void _throwWhenErrResponse(RestResponseBase response, string resource)
        {
            if (response.StatusCode == HttpStatusCode.OK
                || response.StatusCode == HttpStatusCode.Created
                || response.StatusCode == HttpStatusCode.Accepted
                || response.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }

            var errMsg = _parseError(response);

            _log($"Request to {resource} failed: " + errMsg, LogSeverity.Error);
            _log($"Request to {resource} failed: " + response.Content, LogSeverity.Error);

            if (_config.ErrorHandlingBehaviour == ErrorHandlingEnum.ThrowException)
                throw new Exception(errMsg);
        }

        private void _log(string msg, LogSeverity sev)
        {
            _logger?.LogMsg(msg, sev);
        }
        
        #endregion
    }
}
