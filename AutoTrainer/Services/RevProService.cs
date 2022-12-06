using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Selenium;
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
        private readonly RevProBot _revProBot;

        public RevProService(HttpClient client, BatchRepository repo, RevProBot revProBot)
        {
            this.client = client;
            this.repo = repo;
            _revProBot = revProBot;
        }

        /// <summary>
        /// Will grab current Batch's associates. Will re-grab token if token expires
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

                //Will re-grab tokens if token is expired
                if (response.StatusCode == HttpStatusCode.Unauthorized && !(string.IsNullOrEmpty(Properties.Settings.Default.Username) && string.IsNullOrEmpty(Properties.Settings.Default.Password)))
                {
                    bool success = await _revProBot.SaveEncryptedKey(Properties.Settings.Default.Username, Properties.Settings.Default.Password);

                    if (success)
                    {
                        client.DefaultRequestHeaders.Clear();
                        await this.SyncBatch();
                        return null;
                    }
                }
                else if(response.StatusCode == HttpStatusCode.Unauthorized) //Will throw exception instead if user has not set username and password yet
                {
                    throw new ValidationException("401 Unauthorized Access");
                }

                Batch content = JsonSerializer.Deserialize<Batch>(await response.Content.ReadAsStringAsync());

                //Saving to JSON and clearing headers
                repo.Save(content);
                this.client.DefaultRequestHeaders.Clear();

                return content;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
