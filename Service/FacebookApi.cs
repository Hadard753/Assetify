using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assetify.Service
{
    public static class FacebookApi
    {
        private const string facebookToken = "EAAFoZBDCBr0cBAAWNKwmCiPfdBG9AOjZBqSYsF093unYOEZBkA4eYtdFyNlrJP6QDeRdGf8qWkAMB37ZCl1zSkFmEAFXFq3tijylM50gZCMeY2ZAtH58Xs7ol0u1QHFLm9e4uQhlkfTPEALhSX1Jfys1OCZCHCaULUQbe0eTSYqJ8VYIql9RU8Gzrlk04pXnUMkibhGMa5jZAwZDZD";
        public static async void PostToPage(string postContent)
        {
            var baseUrl = $"https://graph.facebook.com/105089301412103/feed?message={postContent}&access_token={facebookToken}";
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
