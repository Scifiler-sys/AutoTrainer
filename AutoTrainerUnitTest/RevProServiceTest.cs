using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Services;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class RevProServiceTest
    {
        private readonly MockHttpMessageHandler _mockHandler;
        private readonly Mock<BatchRepository> _mockBatchRepo;
        private readonly RevProService _batchServ;
        public RevProServiceTest()
        {
            this._mockHandler = new MockHttpMessageHandler();
            this._mockBatchRepo = new Mock<BatchRepository>();
            this._batchServ = new RevProService(_mockHandler.ToHttpClient(), _mockBatchRepo.Object);
        }

        [Fact]
        public async void ShouldLoadBatchFromRevPro()
        {
            //Arrange
            Batch expectedBatch = new Batch();
            string jsonBatch = JsonSerializer.Serialize(expectedBatch);
            //Hardcoded link
            _mockHandler.When("https://app-ms.revature.com/apigateway/batch/1352/18942/gradebook").Respond("application/json", jsonBatch);

            //Act
            Batch actualBatch = await _batchServ.SyncBatch();

            //Assert
            Assert.Equal(expectedBatch, actualBatch);

        }
    }
}
