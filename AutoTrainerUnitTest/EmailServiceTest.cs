using AutoTrainer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoTrainerUnitTest
{
    public class EmailServiceTest
    {
        private readonly EmailService _emailService;

        public EmailServiceTest()
        {
            _emailService = new EmailService();
        }

        [Fact]
        public void CreateEmailShouldWork()
        {

        }
    }
}
