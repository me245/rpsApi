using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace RpsResume
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static DataContractJsonSerializer userSerializer = new DataContractJsonSerializer(typeof(UserEntity));
        static DataContractJsonSerializer sessionSerializer = new DataContractJsonSerializer(typeof(SessionEntity));
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://my.rpsins.com/resume/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Console.ReadLine();
        }

        static async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("User", user);
            response.EnsureSuccessStatusCode();
            Stream streamResponse = await response.Content.ReadAsStreamAsync();
            UserEntity responseUser = (UserEntity)userSerializer.ReadObject(streamResponse);
            return responseUser;
        }

        static async Task<SessionEntity> CreateSessionAsync(UserEntity user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("Session", user);
            response.EnsureSuccessStatusCode();
            Stream streamResponse = await response.Content.ReadAsStreamAsync();
            SessionEntity session = (SessionEntity)sessionSerializer.ReadObject(streamResponse);
            return session;
        }
    }
}
