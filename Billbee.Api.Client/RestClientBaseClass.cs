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
  public abstract class RestClientBaseClass
  {
    protected ApiConfiguration config { get; set; }
    protected ILogger Logger { get; set; }

    string BaseUrl
    {
      get { return config.BaseUrl; }
    }

    protected virtual Dictionary<string, string> AdditionalHeaders { get; set; }

    protected DataFormat RequestFormat { get; set; } = DataFormat.Json;


    protected RestClientBaseClass(ILogger logger, ApiConfiguration config)
    {
      Logger = logger;
      this.config = config;
    }

    protected string put(string resource, NameValueCollection parameter = null)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      var response = c.Put(req);

      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected string put(string resource, dynamic data, NameValueCollection parameter = null)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      if (data != null)
        req.AddBody(data);
      var response = c.Put(req);

      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected T put<T>(string resource, NameValueCollection parameter = null) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      var response = c.Put<T>(req);

      throwWhenErrResponse(response, resource);
      return response.Data;
    }

    protected T put<T>(string resource, dynamic data, NameValueCollection parameter = null) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      if (data != null)
        req.AddBody(data);
      var response = c.Put<T>(req);

      throwWhenErrResponse(response, resource);
      return response.Data;
    }

    protected string patch(string resource, NameValueCollection parameter = null, dynamic data = null)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      if (data != null)
      {
        req.AddBody(data);
      }

      var response = c.Patch(req);

      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected T patch<T>(string resource, NameValueCollection parameter = null, dynamic data = null) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);

      if (data != null)
      {
        req.AddBody(data);
      }

      var response = c.Patch<T>(req);

      throwWhenErrResponse(response, resource);
      return response.Data;
    }

    protected virtual string parseError(IRestResponse response)
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

    protected virtual void throwWhenErrResponse(IRestResponse response, string resource)
    {
      if (response.StatusCode != HttpStatusCode.OK
          && response.StatusCode != HttpStatusCode.Created
          && response.StatusCode != HttpStatusCode.Accepted
          && response.StatusCode != HttpStatusCode.NoContent)
      {
        var errMsg = parseError(response);

        Log($"Request to {resource} failed: " + errMsg, LogSeverity.Error);
        Log($"Request to {resource} failed: " + response.Content, LogSeverity.Error);

        if (config.errorHandlingBehaviour == ErrorHandlingEnum.throwException)
          throw new Exception(errMsg);
      }
    }

    protected class FileParam
    {
      public string Name { get; set; }
      public string FileName { get; set; }
      public byte[] Data { get; set; }
      public string ContentType { get; set; }
    }

    protected async Task<string> postAsync(
        string resource,
        NameValueCollection parameter = null,
        List<FileParam> files = null,
        ParameterType paramType = ParameterType.QueryString,
        string acceptHeaderValue = "application/json")
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter, paramType, acceptHeaderValue);

      if (files != null)
      {
        foreach (var f in files)
        {
          req.AddFile(f.Name, f.Data, f.FileName, f.ContentType);
        }
      }

      var response = await c.ExecutePostAsync(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected void delete(
        string resource,
        NameValueCollection parameter = null,
        ParameterType paramType = ParameterType.QueryString)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter, paramType);
      var response = c.Delete(req);
      throwWhenErrResponse(response, resource);
    }

    protected string post(
        string resource,
        NameValueCollection parameter = null,
        List<FileParam> files = null,
        ParameterType paramType = ParameterType.QueryString)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter, paramType);

      if (files != null)
      {
        foreach (var f in files)
        {
          req.AddFile(f.Name, f.Data, f.FileName, f.ContentType);
        }
      }

      var response = c.Post(req);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected T post<T>(
        string resource,
        NameValueCollection parameter = null,
        List<FileParam> files = null,
        ParameterType paramType = ParameterType.QueryString)
    {
      var resStr = post(resource, parameter, files, paramType);
      return JsonConvert.DeserializeObject<T>(resStr);
    }

    protected async Task<string> postAsync(string resource, dynamic data)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, null);
      req.AddBody(data);
      var response = await c.ExecutePostAsync(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected async Task<T> postAsync<T>(
        string resource,
        dynamic data,
        NameValueCollection parameter = null,
        ParameterType paramType = ParameterType.QueryString) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      if (data != null)
        req.AddBody(data);
      var response = await c.ExecutePostAsync<T>(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Data;
    }

    protected string post(string resource, dynamic data)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, null);
      req.AddBody(data);
      var response = c.Post(req);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected T post<T>(string resource, dynamic data, NameValueCollection parameters = null) where T : new()
    {
      var c = createRestClient();
      var req = parameters != null
          ? createRestRequest(resource, parameters, ParameterType.QueryString)
          : createRestRequest(resource, null);
      req.AddBody(data);
      var response = c.Post<T>(req);
      throwWhenErrResponse(response, resource);


      return response.Data;
    }


    protected async Task<string> putAsync(string resource, NameValueCollection parameter = null)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);
      req.Method = Method.PUT;
      var response = await c.ExecuteAsync(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected async Task<string> putAsync(string resource, dynamic data)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, null);
      req.Method = Method.PUT;
      req.AddBody(data);
      var response = await c.ExecuteAsync(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Content;
    }

    protected async Task<T> putAsync<T>(string resource, dynamic data) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, null);
      req.Method = Method.PUT;
      req.AddBody(data);
      var response = await c.ExecuteAsync<T>(req).ConfigureAwait(false);
      throwWhenErrResponse(response, resource);
      return response.Data;
    }

    /// <summary>
    /// Retries a network request up to 4 times when throttled
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resource"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IRestResponse<T> retry<T>(string resource, Func<IRestResponse<T>> action, int sleepTimeMs)
    {
      IRestResponse<T> response = null;
      for (byte retryCounter = 5; retryCounter > 0; retryCounter--)
      {
        response = action();

        if ((int)response.StatusCode == 429)
        {
          // Throttling
          Thread.Sleep(sleepTimeMs);
        }
        else
        {
          throwWhenErrResponse(response, resource);
          return response;
        }
      }

      throwWhenErrResponse(response, resource);
      throw new Exception("Request is throttled");
    }

    protected HttpStatusCode get(string resource)
    {
      var c = createRestClient();
      var req = createRestRequest(resource, null);
      return c.Get(req).StatusCode;
    }

    protected T requestResource<T>(
        string resource,
        NameValueCollection parameter = null,
        Action<IList<Parameter>> headerProcessor = null,
        int sleepTimeMs = 1000, Action<IRestResponse<T>> preDeserializeHook = null,
        NameValueCollection headerParameter = null) where T : new()
    {
      var c = createRestClient();
      var req = createRestRequest(resource, parameter);

      if (headerParameter != null)
      {
        foreach (var param in headerParameter.AllKeys)
        {
          req.AddHeader(param, headerParameter[param]);
        }
      }

      var pStr = parameter == null ? "" : string.Join(",", parameter.AllKeys.Select(k => $"{k}={parameter[k]}"));

      Log($"Requesting {resource} with params {pStr}", LogSeverity.Info);

      IRestResponse<T> response = retry(resource, () => c.Get<T>(req), sleepTimeMs);
      headerProcessor?.Invoke(response.Headers);
      preDeserializeHook?.Invoke(response);
      var data = JsonConvert.DeserializeObject<T>(response.Content);
      return data;
    }

    protected RestRequest createRestRequest(
        string resource,
        NameValueCollection parameter,
        ParameterType paramType = ParameterType.QueryString,
        string acceptHeaderValue = "application/json")
    {
      RestRequest restreq = new RestRequest(resource);
      restreq.AddHeader("X-Billbee-Api-Key", config.ApiKey);
      restreq.AddHeader("Accept", acceptHeaderValue);
      restreq.RequestFormat = this.RequestFormat;

      if (parameter != null && parameter.Count > 0)
      {
        foreach (string key in parameter)
        {
          restreq.AddParameter(key, parameter[key], paramType);
        }
      }

      return restreq;
    }

    protected RestClient createRestClient()
    {
      RestClient rc = new RestClient(BaseUrl)
      {
        Authenticator = new HttpBasicAuthenticator(config.Username, config.Password),
        UserAgent = $"BillbeeApiClientDotNet/{typeof(ApiClient).Assembly.GetName().Version}"
      };

      if (AdditionalHeaders != null)
        foreach (var h in AdditionalHeaders)
        {
          rc.AddDefaultHeader(h.Key, h.Value);
        }

      return rc;
    }

    protected void Log(string msg, LogSeverity sev)
    {
      if (Logger != null)
        Logger.LogMsg(msg, sev);
    }
  }
}
