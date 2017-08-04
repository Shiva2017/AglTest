using AGL.Models;
using AGL.Orchestration.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace AGL.Orchestration
{
    public class ProcessAglData : IProcessAglData
    {
        HttpClient client;
        AGLData aglData;
        const string type = "cat"; 
        //The URL of the WEB API Service
        string url = "http://agl-developer-test.azurewebsites.net/people.json";

        public ProcessAglData()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            aglData = new AGLData();
        }

        /// <summary>
        /// Process the Json Url data and converts to Object
        /// </summary>
        /// <returns></returns>
        public async Task<AGLData> Process()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var person = JsonConvert.DeserializeObject<List<Person>>(responseData);

                aglData.Persons = ProcessData(person, type);
            }
            return aglData;
        }

        /// <summary>
        /// Sort the object data
        /// </summary>
        /// <param name="person"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Person> ProcessData(List<Person> person, string type)
        {
            List<Person> persons = person.Where(p => p.Pets != null && p.Pets.Any(a => a.Type.ToLower() == type)).ToList();

            return persons.OrderBy(p => p.Name).ToList();
        }
    }
}