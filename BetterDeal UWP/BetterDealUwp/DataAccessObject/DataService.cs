using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BetterDeal.DataAccessObject
{
    public class DataService
    {
        // public static bool _logged = false;
        public static HttpClient _client;
        public static Person _user;
        private static String tokenString;
        private readonly string _url = "http://webapplicationbetterdeal20180130015708.azurewebsites.net/";
        private bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();


        public DataService()
        {
            if (_client == null)
            {
                var handler = new HttpClientHandler { UseCookies = false };
               _client = new HttpClient(handler) { BaseAddress = new Uri(_url) };
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            }

            if (!isInternetConnected) throw new Exception("No network");

        }


        protected void SetContentJson(HttpRequestMessage req, Object o)
        {
            req.Content = new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
        }

      

        public async Task Login(string email, string password)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Post, "api/jwt");
            var login = new LoginForm() { Email = email, Password = password};

            
            SetContentJson(req, login);
            var res = await _client.SendAsync(req);
            var resBody = await res.Content.ReadAsStringAsync();

             if (res.IsSuccessStatusCode)
             {
                // _user = JsonConvert.DeserializeObject<Person>(await res.Content.ReadAsStringAsync());
                dynamic data = JObject.Parse(resBody);
                string tokenReceived = data.access_token;
                tokenString = tokenReceived;
                 

                _user = await GetUser(email);
                if (_user.Status != "admin") {
                    // _logged = true;
                }
             }

        }

        public async Task PostNewPublication(string title, string description)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Post, "api/publications");
            var publication = new Publication() { Title = title, Description = description, ApplicationUserId = _user.Id };


            SetContentJson(req, publication);
            var res = await _client.SendAsync(req);



            if (res.IsSuccessStatusCode)
            {
            }


        }

        public async Task<IEnumerable<Publication>> GetAllPublications()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Get, "api/publications");
            var res = await _client.SendAsync(req);

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Publication>>(json);

                return list;
            }

            return null;
   

        }

        public async Task<Person> GetUser(string email) {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Get, "api/ApplicationUsers/" + email);
            var res = await _client.SendAsync(req);
            if (res.IsSuccessStatusCode) {
                var json = await res.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<Person>(json);
                return person;
            }
            return null;
        }

        public async Task DeletePublication(int idPublication)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Delete, "api/publications/"+idPublication);

            var res = await _client.SendAsync(req);



            if (res.IsSuccessStatusCode)
            {
            }
        }

        public async Task ModifyPubliction(int idPublication, string title, string description)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            var req = new HttpRequestMessage(HttpMethod.Put, "api/publications/"+idPublication);
            var pubModified = new Publication() { Id = idPublication, Title = title, Description = description };

            SetContentJson(req, pubModified);
            var res = await _client.SendAsync(req);



            if (res.IsSuccessStatusCode)
            {
            }
        }

    }
}
