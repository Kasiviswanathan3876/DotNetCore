﻿using IdentityModel.Client;
using System;
using System.Net.Http;

namespace PwdClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:10537").Result;
            if (diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }
            var tokenClient = new TokenClient(diso.TokenEndpoint, "pwdClient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("no8", "123456").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:55412/api/values").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            Console.ReadLine();
        }
    }
}
