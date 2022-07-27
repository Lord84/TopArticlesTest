using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TopArticlesTest.Models;

namespace TopArticlesTest.ServiceProvider
{    
    public class WebRequestProvider
    {
        private readonly HttpClient Client;

        public WebRequestProvider(string baseAddress)
        {
            var addressUri = new Uri(baseAddress);
            Client = new HttpClient() { BaseAddress = addressUri };
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string requestUri, string data = null)
        {
            var request = new HttpRequestMessage(method, requestUri);
            if (!string.IsNullOrWhiteSpace(data))
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            return await Client.SendAsync(request);
        }

        private string GetErrorMessage(HttpStatusCode statusCode, string content)
        {
            string message;

            if (!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    var key = nameof(ReplyModel.Message).ToLower();
                    var contentData = JObject.Parse(content);
                    if (contentData.ContainsKey(key))
                    {
                        var keyValue = (string)contentData[key];
                        if (!string.IsNullOrWhiteSpace(keyValue))
                        {
                            message = $"{statusCode}: {keyValue}";
                        }
                        else
                        {
                            message = $"{statusCode}: Error message empty. Please contact the developer.";
                        }
                    }
                    else
                    {
                        message = $"{statusCode}: {content}";
                    }
                }
                catch (Exception ex)
                {
                    message = $"{statusCode}: {ex.InnerExceptionMessage() + Environment.NewLine}Error Message: {content}";
                }
            }
            else
            {
                message = $"{statusCode}: Response message empty. Please contact the developer.";
            }

            return message;
        }

        public ArticlePageModel GetArticles(int pageNumber)
        {

        }
    }
}
