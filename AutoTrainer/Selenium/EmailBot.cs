using AutoTrainer.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTrainer.Selenium
{
    
    public enum EmailType
    {
        Warn,
        Welcome
    }

    public class EmailBot
    {
        public void SendEmail(Associate associate,EmailType type)
        {
            using (SeleniumDriver seleniumDriver = new SeleniumDriver())
            {
                IWebDriver webDriver = seleniumDriver.GetBot(60);

                webDriver.Navigate().GoToUrl("https://outlook.office.com/");

                ReDoClick(webDriver,"button[aria-label='New mail']");
                ReDoSendKeys(webDriver, "div[aria-label='To']", associate.email);

                IWebElement subject = webDriver.FindElement(By.CssSelector("input[aria-label='Add a subject']"));
                IWebElement body = webDriver.FindElement(By.CssSelector("div[aria-label='Message body, press Alt+F10 to exit']"));
                switch (type)
                {
                    case EmailType.Warn:
                        subject.SendKeys("Technical Warning");
                        body.SendKeys(@"
Hello "+ associate.firstName + @",

This email is a follow-up email for the warning we gave you today " + DateTime.Now.ToString("MM/dd/yyyy") + @". Because of recent performance, you are on performance-based probation for the remainder of the training. As we discussed, we need you to perform well on the upcoming evaluation as well overall examinations going forward, passing the majority of each week’s future evaluations, which include assigned projects, quizzes, one on ones, and QC. Some general suggestions to consider:

- Ensure that you are completing projects and reading the requirements thoroughly.

-  Be able to communicate the technology clearly during one on ones. Make sure you are taking the time to note the topics covered and can offer an overview of the topics and relevant details during our one on ones. Likewise, you should be able to explain any code you have written with an understanding of how and why you wrote the code the way you did.

- Make sure you are taking advantage of notes and demos provided. These will both help in passing your exams. Further, please speak up during training or after if you have any questions. Confusion in one place often spreads as we build on understanding going forward.

- Also, be careful of allowing your home atmosphere from overly distracting you. Though you may be in a non-work environment while working remotely, work will still require your full attention during such hours. Try to find someplace you can use free of said distractions to work and study.

We are always available on Teams or via Email for your questions, and please do reach out with any questions you have.

From,

                    " + Properties.Settings.Default.EmailSignature);
                        break;
                    case EmailType.Welcome:

                        break;
                    default:
                        throw new Exception("Email type does not exist");
                }

                //webDriver.FindElement(By.CssSelector("button[aria-label='Send']")).Click();

                //Not ideal, would like to change to Selenium actually checking that the request has finished
                Thread.Sleep(1000);
            }
        }

        private void ReDoClick(IWebDriver webDriver, string cssSelector)
        {
            try
            {
                webDriver.FindElement(By.CssSelector(cssSelector))
                    .Click();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException exc)
            {
                webDriver.FindElement(By.CssSelector(cssSelector))
                    .Click();
                Console.Error.WriteLine(exc.Message);
            }
        }

        private void ReDoSendKeys(IWebDriver webDriver, string cssSelector, string body)
        {
            try
            {
                webDriver.FindElement(By.CssSelector(cssSelector))
                    .SendKeys(body);
            }
            catch (OpenQA.Selenium.WebDriverException exc)
            {
                webDriver.FindElement(By.CssSelector(cssSelector))
                    .SendKeys(body);
                Console.Error.WriteLine(exc.Message);
            }
        }
    }
}
