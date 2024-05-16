using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure
{
    class HttpService : IHttpService
    {
        private static HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Function to do any async GET call
        public async Task<HttpResponseMessage> GetAsync(Uri url, Dictionary<string, string> headers = null, string token = null)
        {
            // Check if token exists and add it to the header dictionary
            if (token != null && !headers.ContainsKey("Authorisation")) headers.Add("Authorisation", token);
            return await HttpSendAsync(HttpMethod.Get, url, "", headers);
        }

        // Create a standard generic function to send api calls
        private static async Task<HttpResponseMessage> HttpSendAsync(HttpMethod method, Uri url, string payload, Dictionary<string, string> headers = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);
                // Check if there are headers given in the parameters
                if (headers != null)
                {
                    // Add all headers to the request
                    foreach (var header in headers)
                    {
                        // Check if key exists and remove if yes to refresh the value
                        if (request.Headers.Contains(header.Key))
                            request.Headers.Remove(header.Key);
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                // Check if a payload is provided and add it to the request
                if (!string.IsNullOrWhiteSpace(payload))
                    request.Content = new StringContent(payload);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                return response;
            }
            catch(Exception e)
            {
                // Return the error message as an httpResponse since the ui we're using is most likely expecting an http response instead of a string
                HttpResponseMessage httpResponse = new HttpResponseMessage
                {
                    Content = new StringContent(e.Message)
                };
                return httpResponse;
            }
        }
    }
}
