using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TopArticlesTest.Models;

namespace TopArticlesTest.ServiceProviders
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
                message = $"{statusCode}: {content}";
            }
            else
            {
                message = $"{statusCode}: Response message empty. Please contact the developer.";
            }

            return message;
        }

        public async Task<(bool, string, ArticlePageModel)> GetArticles(int pageNumber)
        {
            bool success;
            string message = null;
            ArticlePageModel results = null;

            var response = await SendRequest(HttpMethod.Get, $"?page={pageNumber}");
            var content = await response.Content.ReadAsStringAsync();

            success = response.StatusCode == HttpStatusCode.OK;
            if (success)
            {
                results = JsonConvert.DeserializeObject<ArticlePageModel>(content);
            }
            else
            {
                message = $"Failed getting articles on page {pageNumber}.{Environment.NewLine}Error Details:{Environment.NewLine}{GetErrorMessage(response.StatusCode, content)}";
            }

            return (success, message, results);
        }
    }
}
