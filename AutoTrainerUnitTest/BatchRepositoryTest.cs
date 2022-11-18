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
        private string filepath;

        public BatchRepositoryTest()
        {
            repo = new BatchRepository();
            filepath = Directory.GetCurrentDirectory() + "\\BatchRepository.json";
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
            using (StreamReader reader = new StreamReader(filepath))
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

        [Fact]
        public void ShouldLoadBatchIfFileNotExist()
        {
            //Arrange
            if (File.Exists(filepath))
                File.Delete(filepath);
            Batch expectedBatch = new Batch();

            //Act
            Batch actualBatch = repo.Load();

            //Assert
            Assert.Equal(expectedBatch, actualBatch);
        }
    }
}
