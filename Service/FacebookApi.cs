using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assetify.Service
{
    public static class FacebookApi
    {
        private const string facebookToken = "EAAJ73D4PmAIBAODU23mjeUyvmbjZB0DDUu2Bp8zOo69PhtHxWKRsTOIhjtMzHZBNB5SycSrZADGaZCALd5dZAkXYr1xK07STjOyVwTWxuHuomNPznD3B5lygj38xu5ZCY4MabyZB0R67fefrsqWXRd4NA70dBlEkBijWPdqIuLBlfIRu5EoxUXe";
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
