using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Selenium
{
    public class RevProBot : SeleniumDriver
    {
        //needed to return a task just so exception handler can see the exception in this async method
        //not ideal maybe there is another solution but this has little to no impact in performance atm
        public async void SaveEncryptedKey(string username, string password)
        {
            using (SeleniumDriver driver = new SeleniumDriver())
            {
                ChromeDriver webDriver = (ChromeDriver)driver.GetBot();

                //Setting up interceptor to look for all network requests being sent out
                INetwork interceptor = webDriver.Manage().Network;
                interceptor.NetworkRequestSent += OnNetworkRequestSent;


                webDriver.Navigate().GoToUrl("https://app.revature.com/core/");

                webDriver.FindElement(By.CssSelector(@"#loginForm\:userName-input-id"))
                        .SendKeys(username);

                webDriver.FindElement(By.CssSelector(@"#loginForm\:input-psw"))
                        .SendKeys(password);

                webDriver.FindElement(By.CssSelector(@"#loginForm\:login-btn-id"))
                        .Click();

                webDriver.FindElement(By.CssSelector("#batDashBatBtnDpdwn"))
                        .Click();

                //Starts monitoring the requests
                //Seems like startmonitoring does not work with WPF in the background
                await interceptor.StartMonitoring();

                webDriver.FindElements(By.CssSelector("#batDashBatBtnDpdwnOpt"))[3]
                        .Click();

                webDriver.FindElement(By.CssSelector("#batDashTbl"));

                await interceptor.StopMonitoring();

            }
        }

        //Method will execute for every requests sent
        private void OnNetworkRequestSent(object sender, NetworkRequestSentEventArgs e)
        {
            if (e.RequestUrl.Contains("gradebook") && e.RequestHeaders.ContainsKey("encryptedToken"))
            {
                Properties.Settings.Default.Token = e.RequestHeaders["encryptedToken"];
                Properties.Settings.Default.BatchURL = e.RequestUrl;
            }
        }
    }
}
