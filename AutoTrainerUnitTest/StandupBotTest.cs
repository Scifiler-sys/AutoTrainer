using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Selenium;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class StandupBotTest
    {
        private readonly StandupBot _standupBot;
        private readonly Mock<StandupRepository> _mockStandupRepository;
        public StandupBotTest()
        {
            _mockStandupRepository = new Mock<StandupRepository>();
            _standupBot = new StandupBot(_mockStandupRepository.Object);
        }

        [Fact]
        public void ShouldCalculateHowManyWeeksProperly()
        {
            //Arrange
            DateTime initalDate = new DateTime(2022, 10, 10);
            MethodInfo privateMethod = typeof(StandupBot).GetMethod("WeekCalculation", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = {initalDate};

            //Act
            int result = (int)privateMethod.Invoke(_standupBot, parameters);

            //Assert
            Assert.Equal((int)Math.Ceiling((DateTime.Now - initalDate).Days / 7.0), result);
        }

        [Fact]
        public void SendStandupShouldWork()
        {
            Standup mockStandup = new Standup()
            {
                AwareOfBatch = true,
                CurrentTeam = "EJ",
                HowManyAssociates = 10,
                ListOfInitiatives = "List Initiatives Test",
                SelectedDate = new DateTime(2022, 9, 19),
                GeneralNote = "General Test",
                HowManyWarnings = 2,
                NotMatchingSF = "Not Match test",
                ProjectToStaging = 10,
                WorkLoad = 3
            };
            _mockStandupRepository.Setup(sr => sr.Load()).Returns(mockStandup);

            _standupBot.SendStandupNormal();

        }
    }
}
