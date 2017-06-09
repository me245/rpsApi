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
        static private UserEntity currentUser { get; set; }
        static private SessionEntity currentSession { get; set; }

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://my.rpsins.com/resume/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            while(true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("Please make a selection");
                Console.WriteLine("1) Create User");
                Console.WriteLine("2) Create Session");
                Console.WriteLine("3) List Root Directory");
                Console.WriteLine("4) Create Directory Entity");
                Console.WriteLine("5) List Child Directory");
                Console.WriteLine("6) Update Directory Entity");
                Console.WriteLine("7) Get File Contents");
                Console.WriteLine("q) Quit");
                int responseCode;
                string response = Console.ReadLine();
                if(!int.TryParse(response, out responseCode))
                {
                    if (response.ToLower() == "q") return;
                    responseCode = -1;
                }
                switch (responseCode)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    default:
                        Console.WriteLine("Invalid Selection. Please Try Again");
                        break;
                }
            }
        }

        static async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            UserEntity responseUser = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("User", user);
            if (response.IsSuccessStatusCode)
            {
                responseUser = await response.Content.ReadAsAsync<UserEntity>();
            }
            return responseUser;
        }

        static async Task<SessionEntity> CreateSessionAsync(UserEntity user)
        {
            SessionEntity session = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("Session", user);
            if (response.IsSuccessStatusCode)
            {
                session = await response.Content.ReadAsAsync<SessionEntity>();
            }
            return session;
        }

        static async Task<IEnumerable<DirectoryEntryEntity>> GetDirectoryEntriesAsync()
        {
            IEnumerable<DirectoryEntryEntity> entries = null;
            HttpResponseMessage response = await client.GetAsync("DirectoryEntries");
            if (response.IsSuccessStatusCode)
            {
                entries = await response.Content.ReadAsAsync<IEnumerable<DirectoryEntryEntity>>();
            }
            return entries;
        }

        static async Task<DirectoryEntryEntity> CreateDirectoryEntryAsync(DirectoryEntryEntity entry)
        {
            DirectoryEntryEntity responseEntry = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("DirectoryEntries", entry);
            if (response.IsSuccessStatusCode)
            {
                responseEntry = await response.Content.ReadAsAsync<DirectoryEntryEntity>();
            }
            return responseEntry;
        }

        static async Task<DirectoryEntryEntity> GetDirectoryEntryAsync(DirectoryEntryEntity parent)
        {
            DirectoryEntryEntity entry = null;
            HttpResponseMessage response = await client.GetAsync("DirectoryEntries/{parent.Id}");
            if (response.IsSuccessStatusCode)
            {
                entry = await response.Content.ReadAsAsync<DirectoryEntryEntity>();
            }
            return entry;
        }

        static async Task<DirectoryEntryEntity> PutDirectoryEntryAsync(DirectoryEntryEntity entry)
        {
            DirectoryEntryEntity responseEntry = null;
            HttpResponseMessage response = await client.PutAsJsonAsync("DirectoryEntries", entry);
            if (response.IsSuccessStatusCode)
            {
                responseEntry = await response.Content.ReadAsAsync<DirectoryEntryEntity>();
            }
            return responseEntry;
        }

        static async Task<string> GetDirectoryEntryContentsAsync(DirectoryEntryEntity entry)
        {
            string content = null;
            HttpResponseMessage response = await client.GetAsync("DirectoryEntries/{entry.Id}");
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsAsync<string>();
            }
            return content;
        }

    }
}
