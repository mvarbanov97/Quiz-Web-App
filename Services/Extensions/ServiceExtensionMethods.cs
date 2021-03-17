using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using QuizWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizWebApp.Services.Extensions
{
    public static class ServiceExtensionMethods
    {
        public static async Task<JObject> GetJsonStreamFromUrlAsync(this IService service, string url)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(result);

            return json;
        }
    }
}
