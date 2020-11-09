using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assetify.Service
{
    public static class FacebookApi
    {
        private const string facebookToken = "EAAJZBfKZCPLKwBAJZALY0txJG3OZAxDWHjXSpeJ5mSFu3eSG1Xw2YUHf7M7vzZBjLZCGwFuBXrSjZBKGeViyjKhZBnqGPmI6ZACvdwnIU0CRHsn5k1VOZAMZAEpoOcV9LkZA9CybMoRu0YZAMR0ZAfQ5e1BhUpUJpAKlvDrljwxzkS3dvolwZDZD";
        public static async void PostToPage(string postContent)
        {
            var baseUrl = $"https://graph.facebook.com/102453711679698/feed?message={postContent}&access_token={facebookToken}";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.PostAsync(baseUrl, new StringContent("")))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    Console.WriteLine(data);
                }
            }
        }
    }
}
