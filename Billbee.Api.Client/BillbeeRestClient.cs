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
        private readonly ILogger _logger;
        private readonly DataFormat _requestFormat;
        private readonly Dictionary<string, string> _additionalHeaders;

        public BillbeeRestClient(ILogger logger, ApiConfiguration config)
        {
            _logger = logger;
            _config = config;
            _requestFormat = DataFormat.Json;
            _additionalHeaders = null;
        }
        
        #region get
        
        public HttpStatusCode Get(string resource)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            return c.Get(req).StatusCode;
        }

        public T Get<T>(
            string resource,
            NameValueCollection parameter = null) where T : new()
        {
            return _requestResourceInternal<T>(resource, parameter);
        }
        
        private T _requestResourceInternal<T>(
            string resource,
            NameValueCollection parameter = null,
#pragma warning disable 618
            Action<IList<Parameter>> headerProcessor = null,
#pragma warning restore 618
            int sleepTimeMs = 1000, Action<IRestResponse<T>> preDeserializeHook = null,
            NameValueCollection headerParameter = null) where T : new()
        {
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

            IRestResponse<T> response = _retry(resource, () => c.Get<T>(req), sleepTimeMs);
            headerProcessor?.Invoke(response.Headers);
            preDeserializeHook?.Invoke(response);
            var data = JsonConvert.DeserializeObject<T>(response.Content);
            return data;
        }
        
        #endregion
        
        #region put

        public string Put(string resource, NameValueCollection parameter = null)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public string Put(string resource, dynamic data, NameValueCollection parameter = null)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                req.AddBody(data);
            var response = c.Put(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Put<T>(string resource, NameValueCollection parameter = null) where T : new()
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            var response = c.Put<T>(req);

            _throwWhenErrResponse(response, resource);
            return response.Data;
        }

        public T Put<T>(string resource, dynamic data, NameValueCollection parameter = null) where T : new()
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                req.AddBody(data);
            var response = c.Put<T>(req);

            _throwWhenErrResponse(response, resource);
            return response.Data;
        }
        public async Task<string> PutAsync(string resource, NameValueCollection parameter = null)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            req.Method = Method.PUT;
            var response = await c.ExecuteAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public async Task<string> PutAsync(string resource, dynamic data)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.Method = Method.PUT;
            req.AddBody(data);
            var response = await c.ExecuteAsync(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public async Task<T> PutAsync<T>(string resource, dynamic data) where T : new()
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.Method = Method.PUT;
            req.AddBody(data);
            var response = await c.ExecuteAsync<T>(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Data;
        }
        
        #endregion

        #region patch
        
        public string Patch(string resource, NameValueCollection parameter = null, dynamic data = null)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
            {
                req.AddBody(data);
            }

            var response = c.Patch(req);

            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Patch<T>(string resource, NameValueCollection parameter = null, dynamic data = null) where T : new()
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);

            if (data != null)
            {
                req.AddBody(data);
            }

            var response = c.Patch<T>(req);

            _throwWhenErrResponse(response, resource);
            return response.Data;
        }
        
        #endregion

        #region post
        
        public async Task<string> PostAsync(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString,
            string acceptHeaderValue = "application/json")
        {
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
        private string Post(
            string resource,
            NameValueCollection parameter = null,
            List<FileParam> files = null,
            ParameterType paramType = ParameterType.QueryString)
        {
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
            var resStr = Post(resource, parameter, files, paramType);
            return JsonConvert.DeserializeObject<T>(resStr);
        }

        public async Task<string> PostAsync(string resource, dynamic data)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.AddBody(data);
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
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter);
            if (data != null)
                req.AddBody(data);
            var response = await c.ExecutePostAsync<T>(req).ConfigureAwait(false);
            _throwWhenErrResponse(response, resource);
            return response.Data;
        }

        public string Post(string resource, dynamic data)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, null);
            req.AddBody(data);
            var response = c.Post(req);
            _throwWhenErrResponse(response, resource);
            return response.Content;
        }

        public T Post<T>(string resource, dynamic data, NameValueCollection parameters = null) where T : new()
        {
            var c = _createRestClient();
            var req = parameters != null
                ? _createRestRequest(resource, parameters)
                : _createRestRequest(resource, null);
            req.AddBody(data);
            var response = c.Post<T>(req);
            _throwWhenErrResponse(response, resource);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        
        #endregion

        #region delete
        
        public void Delete(
            string resource,
            NameValueCollection parameter = null,
            ParameterType paramType = ParameterType.QueryString)
        {
            var c = _createRestClient();
            var req = _createRestRequest(resource, parameter, paramType);
            var response = c.Delete(req);
            _throwWhenErrResponse(response, resource);
        }
        
        #endregion

        #region private methods
        
         /// <summary>
        /// Retries a network request up to 4 times when throttled
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <param name="action"></param>
        /// <param name="sleepTimeMs"></param>
        /// <returns></returns>
        private IRestResponse<T> _retry<T>(string resource, Func<IRestResponse<T>> action, int sleepTimeMs)
        {
            IRestResponse<T> response = null;
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
            RestRequest restRequest = new RestRequest(resource);
            restRequest.AddHeader("X-Billbee-Api-Key", _config.ApiKey);
            restRequest.AddHeader("Accept", acceptHeaderValue);
            restRequest.RequestFormat = _requestFormat;

            if (parameter != null && parameter.Count > 0)
            {
                foreach (string key in parameter)
                {
                    restRequest.AddParameter(key, parameter[key], paramType);
                }
            }

            return restRequest;
        }

        private RestClient _createRestClient()
        {
            RestClient rc = new RestClient(_config.BaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password),
                UserAgent = $"BillbeeApiClientDotNet/{typeof(ApiClient).Assembly.GetName().Version}"
            };

            if (_additionalHeaders != null)
                foreach (var h in _additionalHeaders)
                {
                    rc.AddDefaultHeader(h.Key, h.Value);
                }

            return rc;
        }

        private string _parseError(IRestResponse response)
        {
            // Default handling. Works for inventorum, Debitoor and some other
            string errMsg = null;
            try
            {
                var errResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
                errMsg = "HTTP Response: " + response.StatusCode + ": " + errResponse?["ErrorCode"]?.Value<string>() +
                         " - " + errResponse?["ErrorMessage"]?.Value<string>() + errResponse?["Message"]?.Value<string>();

            }
            catch
            {
            }

            errMsg = errMsg ?? ($"Anfrage fehlgeschlagen: {response.StatusCode} {response.StatusDescription}");

            return errMsg;
        }

        private void _throwWhenErrResponse(IRestResponse response, string resource)
        {
            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.Created
                && response.StatusCode != HttpStatusCode.Accepted
                && response.StatusCode != HttpStatusCode.NoContent)
            {
                var errMsg = _parseError(response);

                _log($"Request to {resource} failed: " + errMsg, LogSeverity.Error);
                _log($"Request to {resource} failed: " + response.Content, LogSeverity.Error);

                if (_config.ErrorHandlingBehaviour == ErrorHandlingEnum.ThrowException)
                    throw new Exception(errMsg);
            }
        }

        private void _log(string msg, LogSeverity sev)
        {
            if (_logger != null)
                _logger.LogMsg(msg, sev);
        }
        
        #endregion
    }
}
