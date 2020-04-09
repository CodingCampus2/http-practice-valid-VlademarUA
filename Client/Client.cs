using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Client
{
    class Client
    {
        public Client()
        {
            m_Client = new HttpClient();
            m_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            m_Client.DefaultRequestHeaders.Add("appId", "campus-task");
        }
        public async Task GetAll(bool isSorted = false)
        {
            string isSortedProperty = isSorted ? "?sorted=True" : "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5000/somedata{isSortedProperty}");
            var response = await m_Client.SendAsync(request);
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} {result}");
        }
        public async Task GetById(string id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5000/somedata/{id}");
            var response = await m_Client.SendAsync(request);
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} {result}");
        }

        public async Task PostData(SomeData data)
        {
            var json = JsonConvert.SerializeObject(data);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:5000/somedata";
            var response = await m_Client.PostAsync(url, requestData);
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} {result}");
        }

        public async Task PutById(string id, SomeData data)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5000/somedata/{id}");
            var json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await m_Client.SendAsync(request);
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} {result}");
        }

        public async Task DeleteById(string id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:5000/somedata/{id}");
            var response = await m_Client.SendAsync(request);
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} {result}");
        }

        private HttpClient m_Client;
    }
}
