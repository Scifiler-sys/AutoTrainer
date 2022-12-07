using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V108.Network;
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
        private bool success;

        //needed to return a task just so exception handler can see the exception in this async method
        //not ideal maybe there is another solution but this has little to no impact in performance atm
        public async Task<bool> SaveEncryptedKey(string username, string password)
        {
            using (SeleniumDriver driver = new SeleniumDriver())
            {
                IWebDriver _webDriver = (ChromeDriver)driver.GetBot(5, Properties.Settings.Default.Headless);

                //Setting up interceptor to look for all network requests being sent out
                INetwork interceptor = _webDriver.Manage().Network;
                interceptor.NetworkRequestSent += OnNetworkRequestSent;
                interceptor.NetworkResponseReceived += OnNetworkResponseRecieved;

                //Starts monitoring the requests
                //BUGFIXED!! Running the monitor requires to put it on a separate thread
                //Found out after doing the manual way of starting the monitor and finding online
                await Task.Run(async () => { await interceptor.StartMonitoring(); });

                _webDriver.Navigate().GoToUrl("https://app.revature.com/core/");

                _webDriver.FindElement(By.CssSelector(@"#loginForm\:userName-input-id"))
                        .SendKeys(username);

                _webDriver.FindElement(By.CssSelector(@"#loginForm\:input-psw"))
                        .SendKeys(password);

                _webDriver.FindElement(By.CssSelector(@"#loginForm\:login-btn-id"))
                        .Click();
                try
                {
                    _webDriver.FindElement(By.CssSelector("#batDashBatBtnDpdwn"))
                            .Click();
                }
                catch (NoSuchElementException)
                {
                    return false;
                }

                _webDriver.FindElements(By.CssSelector("#batDashBatBtnDpdwnOpt"))[3]
                        .Click();

                _webDriver.FindElement(By.CssSelector("#batDashTbl"));

                await Task.Run(async () => { await interceptor.StopMonitoring(); });
            }

            return success;
        }

        [Obsolete("Deprecated, use SaveEncryptedKey instead for more detail associate information")]
        public void GrabAssociates(string username, string password)
        {
            using (SeleniumDriver driver = new SeleniumDriver())
            {
                IWebDriver webDriver = driver.GetBot(10, Properties.Settings.Default.Headless);

                webDriver.Navigate().GoToUrl("https://app.revature.com/core/");

                webDriver.FindElement(By.CssSelector(@"#loginForm\:userName-input-id"))
                        .SendKeys(username);

                webDriver.FindElement(By.CssSelector(@"#loginForm\:input-psw"))
                        .SendKeys(password);

                webDriver.FindElement(By.CssSelector(@"#loginForm\:login-btn-id"))
                        .Click();

                webDriver.FindElement(By.CssSelector("#batDashBatBtnDpdwn"))
                        .Click();

                webDriver.FindElements(By.CssSelector("#batDashBatBtnDpdwnOpt"))[3]
                        .Click();

                var table = webDriver.FindElement(By.CssSelector("tbody"));
                var rows = table.FindElements(By.CssSelector("tr"));

                foreach (var item in rows)
                {
                    var name = item.FindElement(By.CssSelector("th.text-left > div > div")).Text;
                    //#batDashTbl > tbody > tr:nth-child(1) > th.text-left > div > div.text-left.font-weight-400.first-col.text-truncate
                    var email = item.FindElement(By.CssSelector("th.text-left > div > div:nth-child(3)")).Text;
                    var github = item.FindElement(By.CssSelector("th:nth-child(2) > div")).Text;
                }

            }
        }

        ////Method will execute for every requests sent
        private void OnNetworkRequestSent(object sender, NetworkRequestSentEventArgs e)
        {
            if (e.RequestUrl.Contains("gradebook") && e.RequestHeaders.ContainsKey("encryptedToken"))
            {
                Properties.Settings.Default.Token = e.RequestHeaders["encryptedToken"];
                Properties.Settings.Default.BatchURL = e.RequestUrl;
                Properties.Settings.Default.Save();

                success = true;
            }
        }

        private void OnNetworkResponseRecieved(object receiver, NetworkResponseReceivedEventArgs e)
        {
            //if authentication fails, 13 headers
            //if authentication success, 11 headers
            if (e.ResponseUrl.Contains("login") && e.ResponseHeaders.Count > 11)
            {
                success = false;
            }
        }
    }
}
