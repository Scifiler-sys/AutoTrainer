using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Selenium
{
    public class SeleniumDriver : IDisposable
    {
        private IWebDriver _webDriver;

        public void Dispose()
        {
            if (_webDriver != null)
            {
                _webDriver.Quit();
                _webDriver = null;
            }
        }
        /// <summary>
        /// Initialization process to start the bot
        /// </summary>
        /// <param name="timer">How long for the bot to wait for the user</param>
        /// <param name="headless">Set True to start the process in the background</param>
        /// <returns></returns>
        public IWebDriver GetBot(int timer, bool headless)
        {
            if (_webDriver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument($"user-data-dir={Directory.GetCurrentDirectory()}/Chrome/UserData/Bot");

                if (headless)
                {
                    options.AddArgument("--headless");
                }

                _webDriver = new ChromeDriver(options);
                _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timer);
            }

            return _webDriver;
        }
    }
}
