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
        private readonly Standup _initialStandup;
        private string filepath;

        public StandupRepositoryTest()
        {
            repo = new StandupRepository();
            filepath = Directory.GetCurrentDirectory() + "\\StandupRepository.json";
            _initialStandup = new Standup()
            {
                SelectedDate = new DateTime(2019, 10, 20),
                AwareOfBatch = true,
                CurrentTeam = "EJ",
                HowManyAssociates = 10,
                ListOfInitiatives = "SomeInitiatives",
                GeneralNote = "Nothing",
                HowManyWarnings = 2,
                NotMatchingSF = "N/A",
                ProjectToStaging = 10,
                WorkLoad = 3
            };
        }

        [Fact]
        public void CanSaveBatchIntoJson()
        { 

            //Act
            repo.Save(_initialStandup);

            //Assert
            using (StreamReader reader = new StreamReader(filepath))
            {
                Standup actualStandup = JsonSerializer.Deserialize<Standup>(reader.ReadToEnd());
                Assert.Equal(_initialStandup, actualStandup);
            }
        }

        [Fact]
        public void LoadBatchIntoObj()
        {

            Standup actualBatch = repo.Load();

            Assert.Equal(_initialStandup, actualBatch);
        }

        [Fact]
        public void ShouldLoadBatchIfFileNotExist()
        {
            //Arrange
            if (File.Exists(filepath))
                File.Delete(filepath);
            Standup _initialStandup = new Standup();

            //Act
            Standup actualStandup = repo.Load();

            //Assert
            Assert.Equal(_initialStandup, actualStandup);
        }
    }
}
