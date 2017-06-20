using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    class Crawler
    {
        public delegate bool Cycle(IWebElement root);

        private IWebDriver _driver;
        private Cycle _cycle;

        private const string User = "daliansunziyu";
        private const string Username = "17649818647";
        private const string Password = "qwerty123456.";
        private const string Url = "https://weibo.cn/";
        private const string LoginUrl = "https://passport.weibo.cn/signin/Login";


        private void Init()
        {
            _driver = new ChromeDriver();
        }

        private void Login()
        {
            _driver.Navigate().GoToUrl(LoginUrl);
            var userName = _driver.FindElement(By.Id("loginName"));
            userName.SendKeys(Username);
            var password = _driver.FindElement(By.Id("loginPassword"));
            password.SendKeys(Password);
            _driver.FindElement(By.Id("loginAction")).Click();
            Thread.Sleep(7500);
        }

        public void Start()
        {
            Init();
            Login();
            int page = 1;
            String rawUrl = Url + User + "?page=";
            int maxPage = 1;

            Console.WriteLine("Login complete, navigating to " + Url + User);
            _driver.Navigate().GoToUrl(Url + User);
            var rawMaxPage = _driver.FindElement(By.XPath("//*[@id=\"pagelist\"]/form/div/input[1]"));
            
            maxPage = int.Parse(rawMaxPage.GetAttribute("value"));
            while (page != maxPage)
            {
                _driver.Navigate().GoToUrl(rawUrl + page++);
                IWebElement root = _driver.FindElement(By.TagName("html"));
                if (!_cycle(root))
                {
                    page--;
                }
            }

        }

        public void Register(Cycle cycle)
        {
            _cycle += cycle;
        }

        public static void Main()
        {
            Parser parser = new Parser();
            Crawler crawler = new Crawler();
            crawler.Register(parser.Parse);
            crawler.Start();
        }

    }
}
