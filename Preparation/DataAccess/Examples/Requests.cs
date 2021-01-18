using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Common;

namespace DataAccess.Examples
{
    public class RequestsExample : Example
    {
        private const string Url = "https://icanhazdadjoke.com/";

        public RequestsExample() : base("Requests example", "4.1")
        {
        }

        public override void Execute()
        {
            var wr = RequestWithWebRequest();
            var wc = RequestWithWebClient();
            var hc = RequestWithHttpClient();

            Console.WriteLine($"Got using WebRequest: {wr}");
            Console.WriteLine($"Got using WebClient: {wc}");
            Console.WriteLine($"Got using HttpClient: {hc}");
        }

        public string RequestWithWebRequest()
        {
            var request = WebRequest.Create(Url);

            request.Headers = new WebHeaderCollection {{"Accept", "text/plain"}};

            var response = request.GetResponse();

            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string RequestWithWebClient()
        {
            var client = new WebClient();

            client.Headers = new WebHeaderCollection { { "Accept", "text/plain" } };
            
            return client.DownloadString(Url);
        }

        public string RequestWithHttpClient()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, Url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            return client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
        }
    }
}