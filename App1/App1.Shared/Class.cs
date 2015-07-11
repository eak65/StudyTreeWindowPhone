using App1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using RestSharp.Portable;

namespace App1
{

    public class result
    {

    }
        public class RequestHandler
        {  
            private static string mainUrl ="http://studytree2.azurewebsites.net/api/";
            public event EventHandler DataReceivedHandler = null;

            public void updateInformation()
            {
                var client = new RestClient("http://studytree2.azurewebsites.net");
                var request = new RestRequest("/api/profile/Getupdates",HttpMethod.Get);

                //IRestResponse response = client.Execute(request);


            }
            public async Task CreatePerson(App1.Model.Logic.CreatePerson person)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(mainUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   
                
                    HttpResponseMessage response;
                    // HTTP POST

                    response = await client.PostAsJsonAsync("Profile/CreateProfile", person);
                    if (response.IsSuccessStatusCode && DataReceivedHandler != null)
                    {
                        var responseBodyAsText = response.Content.ReadAsStringAsync().Result;

                        DataReceivedHandler(response, null);
                    }
                }
            }

            public async Task Login(string usernameOrEmail, string password)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    HttpResponseMessage response;
                    // HTTP POST
                    StringContent theContent = new StringContent("userName=" + usernameOrEmail + "&password=" + password, Encoding.UTF8, "application/x-www-form-urlencoded");
                    response = await client.PostAsync(new Uri("http://studytreebroker2.azurewebsites.net/hander/jwt"), theContent);
                    if (response.IsSuccessStatusCode && DataReceivedHandler != null)
                    {
                        var responseBodyAsText = response.Content.ReadAsStringAsync().Result;


                        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                        if (localSettings.Values.ContainsKey("token") ==false)
                        {
                            localSettings.Values.Add("token", responseBodyAsText);
                        }
                        else
                        {
                            localSettings.Values["token"] = responseBodyAsText;
                        }
                            DataManager.shared().token = responseBodyAsText;
                        DataReceivedHandler(response, null);
                    }
                }
            }

        }




}
