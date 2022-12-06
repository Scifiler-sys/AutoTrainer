using AutoTrainer.DL;
using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoTrainer.Services
{
    public class RevProService
    {
        private readonly HttpClient client;
        private readonly BatchRepository repo;

        public RevProService(HttpClient client, BatchRepository repo)
        {
            this.client = client;
            this.repo = repo;
        }

        /// <summary>
        /// Will grab current Batch's associates
        /// </summary>
        /// <returns>Return a Batch object if successful.</returns>
        /// <exception cref="ValidationException">401 status code was returned</exception>
        public async Task<Batch> SyncBatch()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.BatchURL))
            {
                throw new ValidationException("Batch URL is empty");
            }
            try
            {
                //Grabing info from API
                StringContent stringContent = new StringContent("{\"active\":true,\"dropped\":true,\"isPaginationForLib\":false,\"subscribedContent\":false,\"publicContent\":false,\"ownContent\":false,\"attendance\":[],\"internTrainingStatus\":[\"Active\",\"Dropped\"],\"page\":1,\"size\":40,\"orderBy\":\"lastName\",\"sortOrder\":\"asc\"}", Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("encryptedToken", Properties.Settings.Default.Token);

                using HttpResponseMessage response = await client.PostAsync(Properties.Settings.Default.BatchURL, stringContent);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ValidationException("401 Unauthorized status code");
                }

                Batch content = JsonSerializer.Deserialize<Batch>(await response.Content.ReadAsStringAsync());

                //Saving to JSON
                repo.Save(content);

                return content;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
