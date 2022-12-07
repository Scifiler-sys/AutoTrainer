using AutoTrainer.DL;
using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class StandupRepositoryTest
    {
        private readonly StandupRepository repo;
        private string filepath;

        public StandupRepositoryTest()
        {
            repo = new StandupRepository();
            filepath = Directory.GetCurrentDirectory() + "\\StandupRepository.json";
        }

        [Fact]
        public void CanSaveBatchIntoJson()
        {
            //Arrange
            Standup expectedStandup = new Standup()
            {
                StartDate = new DateTime(2019, 10, 20),
                AwareOfBatch = true,
                CurrentTeam = "EJ",
                HowManyAssociates = 10,
                ListOfInitiatives = "SomeInitiatives"
            };

            //Act
            repo.Save(expectedStandup);

            //Assert
            using (StreamReader reader = new StreamReader(filepath))
            {
                Standup actualStandup = JsonSerializer.Deserialize<Standup>(reader.ReadToEnd());
                Assert.Equal(expectedStandup, actualStandup);
            }
        }

        [Fact]
        public void LoadBatchIntoObj()
        {
            Standup expectedStandup = new Standup()
            {
                StartDate = new DateTime(2019, 10, 20),
                AwareOfBatch = true,
                CurrentTeam = "EJ",
                HowManyAssociates = 10,
                ListOfInitiatives = "SomeInitiatives"
            };

            Standup actualBatch = repo.Load();

            Assert.Equal(expectedStandup, actualBatch);
        }

        [Fact]
        public void ShouldLoadBatchIfFileNotExist()
        {
            //Arrange
            if (File.Exists(filepath))
                File.Delete(filepath);
            Standup expectedStandup = new Standup();

            //Act
            Standup actualStandup = repo.Load();

            //Assert
            Assert.Equal(expectedStandup, actualStandup);
        }
    }
}
