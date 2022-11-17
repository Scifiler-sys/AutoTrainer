using AutoTrainer.DL;
using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class BatchRepositoryTest
    {
        private readonly BatchRepository repo;

        public BatchRepositoryTest()
        {
            repo = new BatchRepository();
        }

        [Fact]
        public void CanSaveBatchIntoJson()
        {
            //Arrange
            Associate addedAssociate = new Associate() { 
                email = "unit@gmail.com",
                gitUsername ="unitGithub",
                firstName = "UnitTest"
            };
            Batch expectedBatch = new Batch() { 
                data = new List<Associate>() { addedAssociate }
            };

            //Act
            repo.Save(expectedBatch);

            //Assert
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + "\\BatchRepository.json"))
            {
                Batch actualBatch = JsonSerializer.Deserialize<Batch>(reader.ReadToEnd());
                Assert.Equal(expectedBatch, actualBatch);
            }
        }

        [Fact]
        public void LoadBatchIntoObj()
        {
            Associate addedAssociate = new Associate()
            {
                email = "unit@gmail.com",
                gitUsername = "unitGithub",
                firstName = "UnitTest"
            };
            Batch expectedBatch = new Batch()
            {
                data = new List<Associate>() { addedAssociate }
            };

            Batch actualBatch = repo.Load();

            Assert.Equal(expectedBatch, actualBatch);
        }
    }
}
