using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CameraDemo2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CameraDemo2.Services
{
    public static class ApiService
    {

        private static string _uri = "http://localhost:20598/";

        public static async Task<List<SavedImage>> GetImagesConverted()
        {
            List<SavedImage> images = null;

            HttpResponseMessage response = null;

            try
            {
                var client = new HttpClient();
                response = await client.GetAsync(_uri + "api/Images/Get");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                images = JsonConvert.DeserializeObject<List<SavedImage>>(content);

            }

            return images;
        }

        public static async Task<string> PostImagesConverted(SavedImage entry)
        {
            var client = new HttpClient();

            var parsedEntry = JsonConvert.SerializeObject(entry);

            var response = await client.PostAsync(_uri + "api/Images/Post", new StringContent(parsedEntry)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                });

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
