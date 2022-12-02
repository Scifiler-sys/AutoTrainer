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

        /*
         * Initialization process to start the bot
         * Will return existing bot or create a new bot
         */
        public IWebDriver GetBot()
        {
            if (_webDriver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument($"user-data-dir={Directory.GetCurrentDirectory()}/Chrome/UserData/Bot");

                ChromeDriverService cService = ChromeDriverService.CreateDefaultService();
                cService.HideCommandPromptWindow = true;

                _webDriver = new ChromeDriver(cService,options);
                _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            }

            return _webDriver;
        }
    }
}
