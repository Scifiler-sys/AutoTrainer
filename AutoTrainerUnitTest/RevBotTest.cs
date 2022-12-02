using AutoTrainer.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class RevBotTest
    {
        [Fact]
        public void GetEncryptedTokenShouldWork()
        {
            //Arrange
            RevProBot bot = new RevProBot();

            //Act
            bot.SaveEncryptedKey(Properties.Resources.Username, Properties.Resources.Password);
        }
    }
}
