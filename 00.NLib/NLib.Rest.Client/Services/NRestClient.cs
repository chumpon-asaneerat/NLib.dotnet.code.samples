#region Using

using System;
using System.Net; // for http status code.
using System.Reflection;

using NLib.Models;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

#endregion

namespace NLib.Services.RestApi
{
    #region NRestClient

    /// <summary>
    /// The NRestClient (RestSharp) wrapper class.
    /// </summary>
    public class NRestClient
    {
        #region Enums

        /// <summary>
        /// The WebProtocol enum.
        /// </summary>
        public enum WebProtocol
        {
            /// <summary>
            /// http protocol.
            /// </summary>
            http,
            /// <summary>
            /// https protocol.
            /// </summary>
            https,
            /// <summary>
            /// Web Socket protocol (for future used).
            /// </summary>
            ws
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestClient() : this(WebProtocol.http, "localhost", 80) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="protocol">The web protocol (http, https).</param>
        /// <param name="host">The host name or IP address.</param>
        /// <param name="port">The port number.</param>
        public NRestClient(WebProtocol protocol = WebProtocol.http,
            string host = "localhost", int port = 9000) : base()
        {
            this.Protocol = protocol;
            this.Host = host;
            this.Port = port;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute (POST).
        /// </summary>
        /// <typeparam name="TReturn">The Returns object type.</typeparam>
        /// <param name="apiUrl">The action api url.</param>
        /// <param name="pObj">The parameter.</param>
        /// <param name="timeout">The timeout in ms.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Returns instance of NRestResult.
        /// </returns>
        public NRestResult<TReturn> Execute<TReturn>(string apiUrl,
            object pObj = null, 
            int timeout = 1000,
            string username = "", string password = "")
            where TReturn: new()
        {
            NRestResult<TReturn> ret = new NRestResult<TReturn>();

            string actionUrl = (!apiUrl.StartsWith("/")) ? @"/" + apiUrl : apiUrl;
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                med.Info("api: {0}", BaseUrl + actionUrl);
                if (null != pObj) med.Info("body: {0}", pObj.ToJson(true));

                var client = new RestClient(BaseUrl);
                //client.ReadWriteTimeout = timeout;
                client.Timeout = timeout;
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    client.Authenticator = new HttpBasicAuthenticator(username, password);
                }
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
                client.UseNewtonsoftJson(NJson.DefaultSettings);
                var request = new RestRequest(actionUrl, Method.POST);
                request.RequestFormat = DataFormat.Json;
                if (null != pObj)
                {
                    request.AddJsonBody(pObj);
                }

                var response = client.Execute(request);
                if (null != response)
                {
                    med.Info("result: {0}", response.Content);

                    if (response.IsSuccessful() && null != response.Content)
                    {
                        var obj = response.Content.FromJson<NDbResult<TReturn>>();
                        if (null != obj && obj.GetType() == typeof(NDbResult<TReturn>))
                        {
                            var dbRet = obj;
                            ret = dbRet.ToRest();
                        }
                        else
                        {
                            ret.data = (null != obj) ? obj.data : NDbResult<TReturn>.Default();
                        }
                    }
                    else
                    {
                        // Keep HTTP status code in result.
                        ret.RestResponseError((int)response.StatusCode, response.StatusDescription);
                        string msg = string.Format(
                            "Rest Client Content Error - Code: {0}, Content: {1}",
                            (int)response.StatusCode, response.Content);
                        Console.WriteLine(msg);
                        med.Err(msg);
                    }
                    // update http status.
                    ret.SetHttpStatus((int)response.StatusCode, response.StatusDescription);
                }
                else
                {
                    ret.RestConenctFailed();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
                ret.Error(ex);
            }

            return ret;
        }
        /// <summary>
        /// Execute (POST).
        /// </summary>
        /// <typeparam name="TReturn">The Returns object type.</typeparam>
        /// <typeparam name="TReturn">The Out object type.</typeparam>
        /// <param name="apiUrl">The action api url.</param>
        /// <param name="pObj">The parameter.</param>
        /// <param name="timeout">The timeout in ms.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Returns instance of NRestResult.
        /// </returns>
        public NRestResult<TReturn, TOut> Execute<TReturn, TOut>(string apiUrl,
            object pObj = null,
            int timeout = 1000,
            string username = "", string password = "")
            where TReturn : new()
            where TOut : new()
        {
            NRestResult<TReturn, TOut> ret = new NRestResult<TReturn, TOut>();

            string actionUrl = (!apiUrl.StartsWith("/")) ? @"/" + apiUrl : apiUrl;
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                med.Info("api: {0}", BaseUrl + actionUrl);
                if (null != pObj) med.Info("body: {0}", pObj.ToJson(true));

                var client = new RestClient(BaseUrl);
                //client.ReadWriteTimeout = timeout;
                client.Timeout = timeout;
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    client.Authenticator = new HttpBasicAuthenticator(username, password);
                }
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
                client.UseNewtonsoftJson(NJson.DefaultSettings);
                var request = new RestRequest(actionUrl, Method.POST);
                request.RequestFormat = DataFormat.Json;
                if (null != pObj)
                {
                    request.AddJsonBody(pObj);
                }

                var response = client.Execute(request);
                if (null != response)
                {
                    med.Info("result: {0}", response.Content);

                    if (response.IsSuccessful() && null != response.Content)
                    {
                        var obj = response.Content.FromJson<NDbResult<TReturn, TOut>>();
                        if (null != obj && obj.GetType() == typeof(NDbResult<TReturn, TOut>))
                        {
                            var dbRet = obj;
                            ret = dbRet.ToRest();
                        }
                        else
                        {
                            ret.data = (null != obj) ? obj.data : NDbResult<TReturn, TOut>.DefaultData();
                            ret.Output = (null != obj) ? obj.Output : NDbResult<TReturn, TOut>.DefaultOutput();
                        }
                    }
                    else
                    {
                        // Keep HTTP status code in result.
                        ret.RestResponseError((int)response.StatusCode, response.StatusDescription);
                        string msg = string.Format(
                            "Rest Client Content Error - Code: {0}, Content: {1}",
                            (int)response.StatusCode, response.Content);
                        Console.WriteLine(msg);
                        med.Err(msg);
                    }
                    // update http status.
                    ret.SetHttpStatus((int)response.StatusCode, response.StatusDescription);
                }
                else
                {
                    ret.RestConenctFailed();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
                ret.Error(ex);
            }

            return ret;
        }
        /// <summary>
        /// Execute (POST).
        /// </summary>
        /// <param name="apiUrl">The action api url.</param>
        /// <param name="pObj">The parameter.</param>
        /// <param name="timeout">The timeout in ms.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Returns instance of NRestResult object.
        /// </returns>
        public NRestResult Execute(string apiUrl,
            object pObj = null,
            int timeout = 1000,
            string username = "", string password = "")
        {
            NRestResult ret = new NRestResult();

            string actionUrl = (!apiUrl.StartsWith("/")) ? @"/" + apiUrl : apiUrl;
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                med.Info("api: {0}", BaseUrl + actionUrl);
                if (null != pObj) med.Info("body: {0}", pObj.ToJson(true));

                var client = new RestClient(BaseUrl);
                //client.ReadWriteTimeout = timeout;
                client.Timeout = timeout;
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    client.Authenticator = new HttpBasicAuthenticator(username, password);
                }
                //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
                client.UseNewtonsoftJson(NJson.DefaultSettings);
                var request = new RestRequest(actionUrl, Method.POST);
                request.RequestFormat = DataFormat.Json;
                if (null != pObj)
                {
                    request.AddJsonBody(pObj);
                }

                var response = client.Execute(request);
                if (null != response)
                {
                    med.Info("result: {0}", response.Content);

                    if (response.IsSuccessful() && null != response.Content)
                    {
                        var obj = response.Content.FromJson<NDbResult>();
                        if (null != obj)
                        {
                            ret = obj.ToRest();
                        }
                    }
                    else
                    {
                        // Keep HTTP status code in result.
                        ret.RestResponseError((int)response.StatusCode, response.StatusDescription);
                        string msg = string.Format(
                            "Rest Client Content Error - Code: {0}, Content: {1}",
                            (int)response.StatusCode,  response.Content);
                        Console.WriteLine(msg);
                        med.Err(msg);
                    }
                    // update http status.
                    ret.SetHttpStatus((int)response.StatusCode, response.StatusDescription);
                }
                else
                {
                    ret.RestConenctFailed();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
                ret.Error(ex);
            }

            return ret;
        }
        /// <summary>
        /// Basic HTTP Get without paraneter.
        /// </summary>
        /// <param name="apiUrl">The Url.</param>
        /// <param name="timeout">The timeout in ms.</param>
        /// <returns>Returns Response content.</returns>
        public object Get(string apiUrl, int timeout = 1000)
        {
            object ret = null;
            string actionUrl = (!apiUrl.StartsWith("/")) ? @"/" + apiUrl : apiUrl;
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                med.Info("api: {0}", BaseUrl + actionUrl);

                var client = new RestClient(BaseUrl);
                //client.ReadWriteTimeout = timeout;
                client.Timeout = timeout;
                var request = new RestRequest(actionUrl, Method.GET);

                var response = client.Execute(request);
                if (null != response)
                {
                    med.Info("result (GET): {0}", response.Content);
                    int code = (int)response.StatusCode;
                    string desc = response.StatusDescription;
                    HttpStatus status = (code >= 200 && code <= 399) ? HttpStatus.Success : HttpStatus.Failed;
                    if (response.IsSuccessful() && null != response.Content)
                    {
                        ret = response.Content;
                    }
                    else
                    {
                        ret = null;
                        string msg = string.Format(
                            "Rest Client Content Error (GET) - Code: {0}, Content: {1}",
                            (int)response.StatusCode, response.Content);
                        Console.WriteLine(msg);
                        med.Err(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return ret;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets web protocol (http, https).
        /// </summary>
        public WebProtocol Protocol { get; set; }
        /// <summary>
        /// Gets or sets host name or IP address.
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Gets or sets The port number.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Gets the host base(root) url.
        /// </summary>
        public string BaseUrl
        {
            get
            {
                string sProtocol = string.Empty;
                switch (Protocol)
                {
                    case WebProtocol.http:
                        sProtocol = "http";
                        break;
                    case WebProtocol.https:
                        sProtocol = "https";
                        break;
                    case WebProtocol.ws:
                        sProtocol = "ws";
                        break;
                    default:
                        sProtocol = "http";
                        break;
                }
                string sHost = (string.IsNullOrWhiteSpace(Host.Trim())) ? "localhost" : Host;
                if (Port <= 0 || Port > 65535)
                {
                    // no port.
                    return string.Format(@"{0}://{1}", sProtocol, sHost);
                }
                else
                {
                    // has port.
                    return string.Format(@"{0}://{1}:{2}", sProtocol, sHost, Port);
                }
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create new instance of NRestClient.
        /// </summary>
        /// <param name="protocol">The web protocol (http, https).</param>
        /// <param name="host">The host name or IP address.</param>
        /// <param name="port">The port number.</param>
        /// <returns>Returns new instance of NRestClient.</returns>
        public static NRestClient Create(WebProtocol protocol = WebProtocol.http,
            string host = "localhost", int port = 9000)
        {
            return new NRestClient(protocol, host, port);
        }

        #endregion
    }

    #endregion

    #region RestSharp Extension Methods

    /// <summary>
    /// The RestSharp Extension Methods.
    /// </summary>
    public static class RestSharpExtensionMethods
    {
        /// <summary>
        /// Checks is Successful.
        /// </summary>
        /// <param name="response">The IRestResponse instance response.</param>
        /// <returns>Returns true if success.</returns>
        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.StatusCode.IsSuccessful()
                && response.ResponseStatus == ResponseStatus.Completed;
        }
        /// <summary>
        /// Checks is Successful.
        /// </summary>
        /// <param name="responseCode">The http status code.</param>
        /// <returns>Returns true if success (code 200-399).</returns>
        public static bool IsSuccessful(this HttpStatusCode responseCode)
        {
            int numericResponse = (int)responseCode;
            return numericResponse >= 200
                && numericResponse <= 399;
        }
    }

    #endregion
}
