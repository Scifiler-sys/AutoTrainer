﻿using AutoTrainer.DL;
using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            client.DefaultRequestHeaders.Add("encryptedToken",Properties.Settings.Default.Token);

            this.repo = repo;
        }

        public async Task<Batch> SyncBatch()
        {
            try
            {
                //Grabing info from API
                StringContent stringContent = new StringContent("{\"active\":true,\"dropped\":true,\"isPaginationForLib\":false,\"subscribedContent\":false,\"publicContent\":false,\"ownContent\":false,\"attendance\":[],\"internTrainingStatus\":[\"Active\",\"Dropped\"],\"page\":1,\"size\":40,\"orderBy\":\"lastName\",\"sortOrder\":\"asc\"}", Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await client.PostAsync(@"https://app-ms.revature.com/apigateway/batch/1352/18942/gradebook", stringContent);

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