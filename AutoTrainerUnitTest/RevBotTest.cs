using AutoTrainer.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class RevBotTest
    {
        private readonly RevProBot _revBot;

        public RevBotTest()
        {
            _revBot = new RevProBot();
        }

        [Fact]
        public async Task GetEncryptedTokenShouldWork()
        {
            bool result = await _revBot.SaveEncryptedKey(Properties.Settings.Default.Username, Properties.Settings.Default.Password);

            Assert.True(result);
        }

        [Fact]
        public async Task GetEncryptedTokenShouldFail()
        {
            bool result = await _revBot.SaveEncryptedKey("Testing", "Failure");

            Assert.False(result);
        }
    }
}
