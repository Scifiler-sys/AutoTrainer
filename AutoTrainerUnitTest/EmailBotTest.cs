using AutoTrainer.Models;
using AutoTrainer.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class EmailBotTest
    {
        private readonly EmailBot _emailBot;

        public EmailBotTest()
        {
            _emailBot = new EmailBot();
        }

        [Fact]
        public void SendWarningEmailShouldWork()
        {
            //Arrange
            Associate expectedAssociate = new Associate()
            {
                firstName = "Pablo",
                lastName = "Pagdilao",
                email = "pablo.delacruz@revature.com"
            };

            //Act
            _emailBot.SendEmail(expectedAssociate, EmailType.Warn);

            //Assert
            //No possible way to do assertion with a testing framework
        }
    }
}
