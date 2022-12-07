using AutoTrainer.DL;
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
    public class StandupBot
    {
        private readonly StandupRepository _standupRepository;
        public StandupBot(StandupRepository standupRepository)
        {
            _standupRepository = standupRepository;
        }

        public void SendStandupNormal()
        {
            Standup currentStandup = _standupRepository.Load();

            using (SeleniumDriver driver = new SeleniumDriver())
            {
                IWebDriver webDriver = driver.GetBot(30, Properties.Settings.Default.Headless);

                webDriver.Navigate().GoToUrl(@"https://forms.office.com/Pages/ResponsePage.aspx?id=iuJja_motUeqQJfiMSFRZM7dmlOzrS1IsTudUsyGqRJUMzVIMEk1SUtYWjVCVDJWMUgxQllBVFk3QyQlQCN0PWcu");

                //=== First Page ===
                webDriver.FindElement(By.CssSelector($"input[value*='{currentStandup.CurrentTeam}']"))
                    .Click();
                //#form-main-content > div > div.office-form.office-form-theme-shadow > div.office-form-body > div.office-form-question-body > div:nth-child(2) > div > div.office-form-question-element > div > div:nth-child(2) > div > label > input[type=radio]

                webDriver.FindElement(By.CssSelector("#SelectId_0_placeholder"))
                    .Click();

                webDriver.FindElement(By.CssSelector($"div[aria-label='{WeekCalculation(currentStandup.SelectedDate)}']"))
                    .Click();

                webDriver.FindElement(By.CssSelector("button[title='Next']"))
                    .Click();


                //=== Second Page ===
                var questionElements = webDriver.FindElements(By.CssSelector("div[class='office-form-question-content']"));

                for (int i = 0; i < questionElements.Count-1; i++)
                {
                    var input = questionElements[i].FindElement(By.CssSelector("div:nth-child(3) > div > div > input"));

                    switch(i)
                    {
                        case 0:
                            input.SendKeys(currentStandup.HowManyAssociates.ToString());
                            break;
                        case 1:
                            input.SendKeys(currentStandup.NotMatchingSF);
                            break;
                        case 2:
                            input.SendKeys(currentStandup.HowManyWarnings.ToString());
                            break;
                        case 3:
                            input.SendKeys(currentStandup.HowManyAssociates.ToString());
                            break;
                        default:
                            break;
                    };
                }

                if (currentStandup.AwareOfBatch)
                {
                    webDriver.FindElement(By.CssSelector("input[value='Yes - aware']"))
                        .Click();
                }
                else
                {
                    webDriver.FindElement(By.CssSelector("input[value='No - need details']"))
                        .Click();
                }

                webDriver.FindElement(By.CssSelector("button[title='Next']"))
                    .Click();

                //=== Third Page ===
                questionElements = webDriver.FindElements(By.CssSelector("div[class='office-form-question-content']"));

                for (int i = 0; i < questionElements.Count-1; i++)
                {
                    var input = questionElements[i].FindElement(By.CssSelector("div:nth-child(3) > div > div > textarea"));

                    switch (i)
                    {
                        case 0:
                            input.SendKeys(currentStandup.GeneralNote);
                            break;
                        case 1:
                            input.SendKeys(currentStandup.ListOfInitiatives);
                            break;
                        default:
                            break;
                    }
                }

                webDriver.FindElement(By.CssSelector($"input[value*='{currentStandup.WorkLoad}']"))
                    .Click();

                webDriver.FindElement(By.CssSelector("button[title='Submit']"))
                    .Click();
            }
        }

        /// <summary>
        /// Will display what element the bot should select depending on what is your current week
        /// </summary>
        /// <param name="initialDate"></param>
        /// <returns></returns>
        private string WeekCalculation(DateTime initialDate)
        {
            TimeSpan diffDate = DateTime.Now - initialDate;
            int calculatedWeek = (int)Math.Ceiling(diffDate.Days / 7.0);

            int currentWeek = calculatedWeek;
            string convertedWeek;
            switch (currentWeek)
            {
                case 0:
                    convertedWeek = "0 - Week Before Batch";
                    break;
                case 12:
                    convertedWeek = "12+";
                    break;
                case > 12:
                    convertedWeek = "Staging";
                    break;
                default:
                    convertedWeek = currentWeek.ToString();
                    break;
            }

            return convertedWeek;
        }
    }
}
